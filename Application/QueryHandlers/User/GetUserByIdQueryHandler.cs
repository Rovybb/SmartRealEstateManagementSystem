using Application.Queries.User;
using AutoMapper;
using Domain.Repositories;
using Domain.Utils;
using MediatR;
using Application.DTOs;

namespace Application.QueryHandlers.User
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<UserDTO>>
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public GetUserByIdQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task<Result<UserDTO>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await userRepository.GetByIdAsync(request.Id);
            if (result.IsSuccess)
            {
                return Result<UserDTO>.Success(mapper.Map<UserDTO>(result.Data));
            }
            return Result<UserDTO>.Failure(result.ErrorMessage);
        }
    }
}
