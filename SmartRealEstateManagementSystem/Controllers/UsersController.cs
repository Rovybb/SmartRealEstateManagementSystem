using Application.Commands.User;
using Application.DTOs;
using Application.Queries.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SmartRealEstateManagementSystem.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator mediator;

        public UsersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(Guid id)
        {
            var query = new GetUserByIdQuery { Id = id };
            var result = await mediator.Send(query);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return NotFound(result.ErrorMessage );
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            var query = new GetAllUsersQuery();
            var result = await mediator.Send(query);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage );
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateUser(CreateUserCommand command)
        {
            var validator = new CreateUserCommandValidator();
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var result = await mediator.Send(command);
            if (result.IsSuccess)
            {
                return CreatedAtAction(nameof(GetUserById), new { Id = result.Data }, result.Data);
            }
            return BadRequest(result.ErrorMessage );
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(Guid id, UpdateUserCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("Id mismatch");
            }

            var validator = new UpdateUserCommandValidator();
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var result = await mediator.Send(command);
            if (!result.IsSuccess)
            {
                if (result.ErrorMessage == "User not found")
                {
                    return NotFound(result.ErrorMessage );
                }
                return BadRequest(result.ErrorMessage );
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            var command = new DeleteUserCommand { Id = id };
            var result = await mediator.Send(command);
            if (result.IsSuccess)
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}
