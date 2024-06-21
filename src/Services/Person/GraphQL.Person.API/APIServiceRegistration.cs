using GraphQL.Person.API.GraphQL;
using GraphQL.Person.API.GraphQL.InputTypes.Person;
using GraphQL.Person.API.GraphQL.Mutations;
using GraphQL.Person.API.GraphQL.Queries;
using GraphQL.Person.API.GraphQL.Types;
using GraphQL.Types;

namespace GraphQL.Person.API
{
    public static class APIServiceRegistration
    {
        public static IServiceCollection AddAPIServices(this IServiceCollection services)
        {
            //Person
            services.AddScoped<PersonType>();
            services.AddScoped<CreatePersonInputType>();
            services.AddScoped<UpdatePersonInputType>();
            services.AddScoped<DeletePersonInputType>();

            //Queries
            services.AddScoped<PersonQueries>();

            //Mutations
            services.AddScoped<PersonMutations>();

            //Schema
            services.AddScoped<ISchema, PersonSchema>();

            return services;

        }
    }
}
