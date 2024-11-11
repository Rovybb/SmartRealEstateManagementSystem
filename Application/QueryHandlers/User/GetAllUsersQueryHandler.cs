using Application.Queries.User;
using AutoMapper;
using Domain.Repositories;
using MediatR;
using Domain.Utils;
using Application.DTOs;

namespace Application.QueryHandlers.User
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, Result<IEnumerable<UserDto>>>
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public GetAllUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task<Result<IEnumerable<UserDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var result = await userRepository.GetAllAsync();
            var users = result.Select(user => mapper.Map<UserDto>(user));
            return Result<IEnumerable<UserDto>>.Success(users);
        }
    }
}
