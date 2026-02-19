using ProductionRouting.Domain.Engine;
using ProductionRouting.Domain.Models;

namespace ProductionRouting.Application.Interfaces;

public interface IOrderProcessor
{
    Task<EvaluationResult> ProcessAsync(Order order);
}
