using Application.Queries.User;
using AutoMapper;
using Domain.Repositories;
using MediatR;
using Domain.Utils;
using Application.DTOs;

namespace Application.QueryHandlers.User
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, Result<IEnumerable<UserDTO>>>
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public GetAllUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task<Result<IEnumerable<UserDTO>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var result = await userRepository.GetAllAsync();
            var users = result.Select(user => mapper.Map<UserDTO>(user));
            return Result<IEnumerable<UserDTO>>.Success(users);
        }
    }
}
