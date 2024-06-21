using FluentValidation;
using GraphQL.Person.Domain.Services.Validation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace GraphQL.Person.Domain
{
    public static class DomainServiceRegistration
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddScoped<IValidator<Entities.Person>, PersonValidator>();

            return services;
        }
    }
}
