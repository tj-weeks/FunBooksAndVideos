using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FunBooksAndVideos.PurchaseOrder
{
    [ApiController]
    [Route("[controller]")]
    public class PurchaseOrderController : ControllerBase
    {
        private readonly ILogger<PurchaseOrderController> _logger;
        private readonly IMediator _mediator;

        public PurchaseOrderController(ILogger<PurchaseOrderController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost(Name = "PurchaseOrder")]
        public async Task<IActionResult> PostAsync([FromBody] OrderItems.PurchaseOrderItem purchaseOrder)
        {
            _logger.LogInformation("Processing purchase order");

            var command = new PurchaseOrderCommand(purchaseOrder);

            var response = await _mediator.Send(command, new CancellationToken());

            if (response.IsSuccessStatusCode)
            {
                return Ok(response.Content);
            }
            else
            {
                return StatusCode(500, response.Content);
            }
        }
    }
}
