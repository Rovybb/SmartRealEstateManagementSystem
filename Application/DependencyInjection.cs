using Application.Commands.Payment;
using Application.Commands.Property;
using Application.Utils;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddValidatorsFromAssemblyContaining<CreatePropertyCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdatePropertyCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<CreatePaymentCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdatePaymentCommandValidator>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            return services;
        }
    }
}
