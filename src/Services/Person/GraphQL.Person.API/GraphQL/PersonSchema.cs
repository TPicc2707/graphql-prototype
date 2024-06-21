using GraphQL.Person.API.GraphQL.Mutations;
using GraphQL.Person.API.GraphQL.Queries;
using GraphQL.Types;

namespace GraphQL.Person.API.GraphQL
{
    public class PersonSchema : Schema
    {
        public PersonSchema(PersonQueries query, PersonMutations mutations, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            this.Query = query;
            this.Mutation = mutations;
        }
    }
}
