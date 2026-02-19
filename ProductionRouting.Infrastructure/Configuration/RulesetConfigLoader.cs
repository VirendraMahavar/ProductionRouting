using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionRouting.Domain.Entities;
using ProductionRouting.Domain.Enums;
using ProductionRouting.Infrastructure.Persistence;
using System.Text.Json;

namespace ProductionRouting.Infrastructure.Configuration
{  
    public class RulesetConfigLoader
    {
        private readonly ProductionRoutingDbContext _context;

        public RulesetConfigLoader(ProductionRoutingDbContext context)
        {
            _context = context;
        }

        public void LoadFromFile(string filePath)
        {
            if (_context.Rulesets.Any())
                return;

            var json = File.ReadAllText(filePath);

            var config = JsonSerializer.Deserialize<RulesetConfigRoot>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            foreach (var rsConfig in config.Rulesets)
            {
                var ruleset = new Ruleset
                {
                    Name = rsConfig.Name,
                    Priority = 1,
                    IsActive = true,
                    Conditions = rsConfig.Conditions.Select(c => new Condition
                    {
                        Field = c.Field,
                        Operator = ParseOperator(c.Operator),
                        Value = c.Value.ToString()
                    }).ToList(),
                    Rules = rsConfig.Rules.Select(r => new Rule
                    {
                        Name = r.Name,
                        ProductionPlant = r.Result.ProductionPlant,
                        Priority = 1,
                        Conditions = r.Conditions.Select(c => new Condition
                        {
                            Field = c.Field,
                            Operator = ParseOperator(c.Operator),
                            Value = c.Value.ToString()
                        }).ToList()
                    }).ToList()
                };

                _context.Rulesets.Add(ruleset);
            }

            _context.SaveChanges();
        }

        private OperatorType ParseOperator(string op)
        {
            return op switch
            {
                "Equals" => OperatorType.Equals,
                "LessThanOrEqual" => OperatorType.LessThanOrEqual,
                "GreaterThanOrEqual" => OperatorType.GreaterThanOrEqual,
                _ => throw new Exception($"Unknown operator {op}")
            };
        }
    }

}
