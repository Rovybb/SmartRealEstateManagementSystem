using Application.Commands.User;
using AutoMapper;
using Domain.Repositories;
using Domain.Utils;
using MediatR;

namespace Application.CommandHandlers.User
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result>
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await userRepository.GetByIdAsync(request.Id);
            if (!existingUser.IsSuccess)
            {
                return Result.Failure("User not found.");
            }

            mapper.Map(request, existingUser.Data);

            var updateResult = await userRepository.UpdateAsync(existingUser.Data);
            if (updateResult.IsSuccess)
            {
                return Result.Success();
            }
            return Result.Failure(updateResult.ErrorMessage );
        }
    }
}
