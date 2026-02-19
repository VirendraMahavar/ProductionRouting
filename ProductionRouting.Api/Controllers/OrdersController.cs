using Microsoft.AspNetCore.Mvc;
using ProductionRouting.Application.Interfaces;
using ProductionRouting.Domain.Models;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly IOrderProcessor _processor;

    public OrdersController(IOrderProcessor processor)
    {
        _processor = processor;
    }

    [HttpPost]
    public async Task<IActionResult> Submit(Order order)
    {
        var result = await _processor.ProcessAsync(order);
        return Ok(result);
    }
}
