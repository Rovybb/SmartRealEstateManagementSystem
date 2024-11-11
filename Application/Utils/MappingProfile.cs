using Application.Commands.Inquiry;
using Application.Commands.Payment;
using Application.Commands.Property;
using Application.Commands.User;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Utils
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Property, PropertyDto>().ReverseMap();
            CreateMap<Property, CreatePropertyCommand>().ReverseMap();
            CreateMap<Property, UpdatePropertyCommand>().ReverseMap();

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
}
