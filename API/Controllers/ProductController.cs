using API.CustomAttributes;
using Application.ProductCommandQuery.Command;
using Application.ProductCommandQuery.Query;
using Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    public class ProductController(IMediator mediator) : BaseController
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("id")]
        [SwaggerOperation(
            Summary = "Get a Product",
            Description = "Get a Product with Id",
            OperationId = "Product.Get",
            Tags = ["Product"])]
        [AccessControl(Permission = PermissionFlags.GetProduct)]
        public async Task<IActionResult> Get([FromQuery] GetProductQuery query)
        {
            GetProductQueryResponse result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Get All Products",
            Description = "Get All Products",
            OperationId = "Product.GetAll",
            Tags = ["Product"])]
        [AccessControl(Permission = PermissionFlags.GetAllProducts)]
        public async Task<IActionResult> GetAll([FromQuery] GetAllProductQuery query)
        {
            List<GetProductQueryResponse> result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Add a Product",
            Description = "Add a Product with SaveProductCommand",
            OperationId = "Product.Add",
            Tags = ["Product"])]
        [AccessControl(Permission = PermissionFlags.AddProduct)]
        public async Task<IActionResult> Add([FromForm] SaveProductCommand command)
        {
            SaveProductCommandResponse result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
