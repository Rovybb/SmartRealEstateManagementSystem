using Application.Commands.Property;
using Application.DTOs;
using Application.Queries.Property;
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
        public async Task<ActionResult<PropertyDto>> GetPropertyById(Guid id)
        {
            var query = new GetPropertyByIdQuery { Id = id };
            var result = await mediator.Send(query);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return NotFound(result.ErrorMessage );
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PropertyDto>>> GetAllProperties()
        {
            var query = new GetAllPropertiesQuery();
            var result = await mediator.Send(query);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage );
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateProperty(CreatePropertyCommand command)
        {
            var validator = new CreatePropertyCommandValidator();
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var result = await mediator.Send(command);
            if (result.IsSuccess)
            {
                return CreatedAtAction(nameof(GetPropertyById), new { Id = result.Data }, result.Data);
            }
            return BadRequest(result.ErrorMessage );
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Guid>> UpdateProperty(Guid id, UpdatePropertyCommand command)
        {
            var validator = new UpdatePropertyCommandValidator();
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

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
            return BadRequest(result.ErrorMessage ); // here is optional to send the error message
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProperty(Guid id)
        {
            var command = new DeletePropertyCommand { Id = id };
            var result = await mediator.Send(command);
            if (result.IsSuccess)
            {
                return NoContent();
            }
            return NotFound(result.ErrorMessage );
        }
    }
}
