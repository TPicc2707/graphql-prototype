using GraphQL.Address.API.GraphQL.Mutations;
using GraphQL.Address.API.GraphQL.Queries;
using GraphQL.Types;

namespace GraphQL.Address.API.GraphQL
{
    public class PersonAddressSchema : Schema
    {
        public PersonAddressSchema(PersonAddressQueries query, PersonAddressMutations mutations, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            this.Query = query;
            this.Mutation = mutations;
        }
    }
}
