using Application.Commands.Property;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Utils
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Property, PropertyDTO>().ReverseMap();
            CreateMap<Property, CreatePropertyCommand>().ReverseMap();
            CreateMap<Property, UpdatePropertyCommand>().ReverseMap();
        }
    }
}
