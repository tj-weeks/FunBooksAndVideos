using FunBooksAndVideos.Classes;
using FunBooksAndVideos.PurchaseOrders;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FunBooksAndVideos.Controllers
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

        [HttpPost(Name = "Test")]
        public async Task<IActionResult> Post()
        {
            var purchaseOrder = new Classes.PurchaseOrder
            {
                Id = 1,
                CustomerId = 1,
                TotalPrice = 100,
                PurchaseItems = new List<IPurchaseItem>
                {
                    new ProductItem
                    {
                        Id = 1,
                        Name = "Product 1",
                        Price = 50
                    },
                    new MembershipItem
                    {
                        Id = 2,
                        Name = "Membership 1",
                        Price = 50
                    }
                }
            };

            var command = new PurchaseOrderCommand(purchaseOrder);

            var response = await _mediator.Send(command, new CancellationToken());

            return Ok(response);
        }

        ////[HttpPost(Name = "PurchaseOrder")]
        ////public IActionResult Post(
        ////    [FromBody] Classes.PurchaseOrder purchaseOrder)
        ////{
        ////    _logger.LogDebug("Processing purchase order");

        ////    return Ok();
        ////}
    }
}
