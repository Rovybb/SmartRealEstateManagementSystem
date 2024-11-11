﻿using Application.DTOs;
using Domain.Utils;
using MediatR;

namespace Application.Queries.User
{
    public class GetUserByIdQuery : IRequest<Result<UserDTO>>
    {
        public Guid Id { get; set; }
    }
}
