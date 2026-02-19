using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProductionRouting.Domain.Models;
using ProductionRouting.Domain.Engine;

namespace ProductionRouting.Application.Interfaces;

public interface IEvaluationService
{
    Task<EvaluationResult> EvaluateAsync(Order order);
}
