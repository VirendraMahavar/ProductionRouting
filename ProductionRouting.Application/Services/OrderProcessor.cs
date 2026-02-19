using ProductionRouting.Application.Interfaces;
using ProductionRouting.Domain.Engine;
using ProductionRouting.Domain.Models;

namespace ProductionRouting.Application.Services;

public class OrderProcessor : IOrderProcessor
{
    private readonly IEvaluationService _evaluationService;

    public OrderProcessor(IEvaluationService evaluationService)
    {
        _evaluationService = evaluationService;
    }

    public async Task<EvaluationResult> ProcessAsync(Order order)
    {
        if (order == null)
            throw new ArgumentNullException(nameof(order));

        if (string.IsNullOrWhiteSpace(order.PublisherNumber))
            throw new ArgumentException("PublisherNumber is required");

        return await _evaluationService.EvaluateAsync(order);
    }
}

