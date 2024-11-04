using Application.Commands.Property;
using Application.DTOs;
using Domain.Utils;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SmartRealEstateManagementSystem.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly IMediator mediator;

        public PropertiesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PropertyDTO>> GetPropertyById(Guid id)
        {
            // TODO
            // Finish this endpoint
            var temp = $"Property with {id}";
            return Ok(temp);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateProperty(CreatePropertyCommand command)
        {
            var result = await mediator.Send(command);
            if (result.IsSuccess)
            {
                return CreatedAtAction(nameof(GetPropertyById), new { Id = result.Data }, result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Guid>> UpdateProperty(Guid id, UpdatePropertyCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("Ids didn't match.");
            }
            var result = await mediator.Send(command);
            if (result == null)
            {
                return NotFound("Property not found.");
            }

            if (result.IsSuccess)
            {
                return NoContent();
            }
            return BadRequest(result.ErrorMessage);
        }

    }
}
