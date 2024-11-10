﻿using Application.DTOs;
using Application.Queries.Property;
using AutoMapper;
using Domain.Repositories;
using Domain.Utils;
using MediatR;

namespace Application.QueryHandlers.Property
{
    public class GetPropertyByIdQueryHandler : IRequestHandler<GetPropertyByIdQuery, Result<PropertyDTO>>
    {
        private readonly IPropertyRepository propertyRepository;
        private readonly IMapper mapper;


        public GetPropertyByIdQueryHandler(IPropertyRepository propertyRepository, IMapper mapper)
        {
            this.propertyRepository = propertyRepository;
            this.mapper = mapper;
        }

        public async Task<Result<PropertyDTO>> Handle(GetPropertyByIdQuery request, CancellationToken cancellationToken)
        {
            var propertyResult = await propertyRepository.GetByIdAsync(request.Id);
            if (!propertyResult.IsSuccess)
            {
                return Result<PropertyDTO>.Failure(propertyResult.ErrorMessage);
            }

            var propertyDTO = mapper.Map<PropertyDTO>(propertyResult.Data);
            return Result<PropertyDTO>.Success(propertyDTO);
        }
    }
}
