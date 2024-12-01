using Application.Commands.Inquiry;
using Application.Commands.Payment;
using Application.Commands.Property;
using Application.Commands.User;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Utils;

namespace Application.Utils
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Property, PropertyDto>().ReverseMap();
            CreateMap<Property, CreatePropertyCommand>().ReverseMap();
            CreateMap<Property, UpdatePropertyCommand>().ReverseMap();
            CreateMap(typeof(PaginatedList<>), typeof(PaginatedList<>))
            .ConvertUsing(typeof(PaginatedListConverter<,>));

            CreateMap<Payment, PaymentDto>().ReverseMap();
            CreateMap<Payment, CreatePaymentCommand>().ReverseMap();
            CreateMap<Payment, UpdatePaymentCommand>().ReverseMap();

            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, CreateUserCommand>().ReverseMap();
            CreateMap<User, UpdateUserCommand>().ReverseMap();

            CreateMap<Inquiry, InquiryDto>().ReverseMap();
            CreateMap<Inquiry, CreateInquiryCommand>().ReverseMap();
            CreateMap<Inquiry, UpdateInquiryCommand>().ReverseMap();
        }
    }

    public class PaginatedListConverter<TSource, TDestination> : ITypeConverter<PaginatedList<TSource>, PaginatedList<TDestination>>
    {
        public PaginatedList<TDestination> Convert(PaginatedList<TSource> source, PaginatedList<TDestination> destination, ResolutionContext context)
        {
            var items = context.Mapper.Map<List<TDestination>>(source.Items);
            return new PaginatedList<TDestination>(items, source.TotalCount, source.PageNumber, source.PageSize);
        }
    }
}
