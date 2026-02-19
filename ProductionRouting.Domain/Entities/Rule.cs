using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionRouting.Domain.Entities;

public class Rule
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ProductionPlant { get; set; }
    public int Priority { get; set; }

    public List<Condition> Conditions { get; set; } = new();
}
