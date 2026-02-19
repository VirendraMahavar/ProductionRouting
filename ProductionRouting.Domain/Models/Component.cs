using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionRouting.Domain.Models;

public class Component
{
    public string Code { get; set; }

    public ComponentAttributes Attributes { get; set; }
}
