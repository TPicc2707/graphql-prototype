using FluentValidation;
using GraphQL.Address.Domain.Services.Validation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace GraphQL.Address.Domain
{
    public static class DomainServiceRegistration
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddScoped<IValidator<Entities.Person>, PersonValidator>();
            services.AddScoped<IValidator<Entities.Address>, AddressValidator>();

            return services;
        }

    }
}
