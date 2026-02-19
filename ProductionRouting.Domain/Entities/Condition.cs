using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProductionRouting.Domain.Enums;

namespace ProductionRouting.Domain.Entities;

public class Condition
{
    public int Id { get; set; }

    public string Field { get; set; }
    public OperatorType Operator { get; set; }
    public string Value { get; set; }

    public bool Evaluate(object actualValue)
    {
        var actual = actualValue?.ToString();

        return Operator switch
        {
            OperatorType.Equals =>
                actual == Value,

            OperatorType.LessThanOrEqual =>
                Convert.ToDecimal(actual) <= Convert.ToDecimal(Value),

            OperatorType.GreaterThanOrEqual =>
                Convert.ToDecimal(actual) >= Convert.ToDecimal(Value),

            _ => false
        };
    }
}

