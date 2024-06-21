using GraphQL.Address.API.GraphQL;
using GraphQL.Address.API.GraphQL.InputTypes.Address;
using GraphQL.Address.API.GraphQL.Mutations;
using GraphQL.Address.API.GraphQL.Queries;
using GraphQL.Address.API.GraphQL.Types;
using GraphQL.Types;

namespace GraphQL.Address.API
{
    public static class APIServiceRegistration
    {
        public static IServiceCollection AddAPIServices(this IServiceCollection services)
        {
            //Person
            services.AddScoped<PersonType>();

            //Address
            services.AddScoped<AddressType>();
            services.AddScoped<CreateAddressInputType>();
            services.AddScoped<UpdateAddressInputType>();
            services.AddScoped<DeleteAddressInputType>();

            //Queries
            services.AddScoped<PersonAddressQueries>();

            //Mutations
            services.AddScoped<PersonAddressMutations>();

            //Schema
            services.AddScoped<ISchema, PersonAddressSchema>();

            return services;

        }
    }
}
