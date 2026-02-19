using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionRouting.Domain.Entities;

public class Ruleset
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Priority { get; set; }
    public bool IsActive { get; set; }

    public List<Condition> Conditions { get; set; } = new();
    public List<Rule> Rules { get; set; } = new();
}
