using Microsoft.AspNetCore.Mvc;
using ProductionRouting.Application.Interfaces;
using ProductionRouting.Application.Services;
using ProductionRouting.Domain.Models;

namespace ProductionRouting.Api.Controllers
{
    [ApiController]
    [Route("api/evaluate")]
    public class EvaluationController : ControllerBase
    {
        private readonly IEvaluationService _service;

        public EvaluationController(IEvaluationService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Evaluate(Order order)
        {
            var result = await _service.EvaluateAsync(order);
            return Ok(result);
        }
    }

}
