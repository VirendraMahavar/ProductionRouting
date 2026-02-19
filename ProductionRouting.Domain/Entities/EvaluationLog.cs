using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionRouting.Domain.Entities;

public class EvaluationLog
{
    public long Id { get; set; }

    public string OrderId { get; set; }

    public bool Matched { get; set; }

    public string? MatchedRuleset { get; set; }
    public string? MatchedRule { get; set; }
    public string? ProductionPlant { get; set; }


    public string Reason { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

