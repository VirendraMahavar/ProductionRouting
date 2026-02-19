using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductionRouting.Application.Interfaces;
using ProductionRouting.Domain.Engine;
using ProductionRouting.Domain.Models;
using ProductionRouting.Infrastructure.Persistence;

namespace ProductionRouting.Infrastructure.Services
{    
    public class EvaluationService : IEvaluationService
    {
        private readonly ProductionRoutingDbContext _context;

        public EvaluationService(ProductionRoutingDbContext context)
        {
            _context = context;
        }

        public async Task<EvaluationResult> EvaluateAsync(Order order)
        {
            var rulesets = await _context.Rulesets
                .Include(r => r.Conditions)
                .Include(r => r.Rules)
                    .ThenInclude(r => r.Conditions)
                .ToListAsync();

            var engine = new RuleEngine(rulesets);

            var result = engine.Evaluate(order);

            await LogEvaluation(order, result);

            return result;
        }

        private async Task LogEvaluation(Order order, EvaluationResult result)
        {
            var log = new Domain.Entities.EvaluationLog
            {
                OrderId = order.OrderId,
                Matched = result.Matched,
                MatchedRuleset = result.MatchedRuleset,
                MatchedRule = result.MatchedRule,
                ProductionPlant = result.ProductionPlant,
                Reason = result.Reason
            };

            _context.EvaluationLogs.Add(log);
            await _context.SaveChangesAsync();
        }
    }

}
