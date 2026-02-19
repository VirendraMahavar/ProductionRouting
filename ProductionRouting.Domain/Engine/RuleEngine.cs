using ProductionRouting.Domain.Entities;
using ProductionRouting.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionRouting.Domain.Engine;

public class RuleEngine
{
    private readonly IEnumerable<Ruleset> _rulesets;

    public RuleEngine(IEnumerable<Ruleset> rulesets)
    {
        _rulesets = rulesets.OrderBy(r => r.Priority);
    }

    public EvaluationResult Evaluate(Order order)
    {
        var facts = OrderFactExtractor.Extract(order);

        foreach (var ruleset in _rulesets)
        {
            if (!ruleset.IsActive)
                continue;

            if (!ruleset.Conditions.All(c => c.Evaluate(facts[c.Field])))
                continue;

            foreach (var rule in ruleset.Rules.OrderBy(r => r.Priority))
            {
                if (rule.Conditions.All(c => c.Evaluate(facts[c.Field])))
                {
                    return EvaluationResult.Success(
                        ruleset.Name,
                        rule.Name,
                        rule.ProductionPlant);
                }
            }
        }

        return EvaluationResult.Failure();
    }
}

