using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionRouting.Domain.Engine;

public class EvaluationResult
{
    public bool Matched { get; set; }
    public string ProductionPlant { get; set; }
    public string MatchedRuleset { get; set; }
    public string MatchedRule { get; set; }
    public string Reason { get; set; }

    public static EvaluationResult Success(
        string ruleset,
        string rule,
        string plant)
    {
        return new EvaluationResult
        {
            Matched = true,
            ProductionPlant = plant,
            MatchedRuleset = ruleset,
            MatchedRule = rule,
            Reason = "All conditions satisfied"
        };
    }

    public static EvaluationResult Failure()
    {
        return new EvaluationResult
        {
            Matched = false,
            MatchedRuleset = "None",
            MatchedRule = "None",
            ProductionPlant = "None",
            Reason = "No matching rules found"
        };
    }

}

