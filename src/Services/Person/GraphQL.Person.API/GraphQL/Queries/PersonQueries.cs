using GraphQL.Person.API.GraphQL.Types;
using GraphQL.Person.Application.Features.Person.Queries.GetAllPeople;
using GraphQL.Person.Application.Features.Person.Queries.GetAllPeopleByFirstName;
using GraphQL.Person.Application.Features.Person.Queries.GetAllPeopleByLastName;
using GraphQL.Person.Application.Features.Person.Queries.GetAllPeoplebyMiddleInitial;
using GraphQL.Person.Application.Features.Person.Queries.GetAllPeoplebyTitle;
using GraphQL.Person.Application.Features.Person.Queries.GetPersonById;
using GraphQL.Types;
using MediatR;

namespace GraphQL.Person.API.GraphQL.Queries
{
    public class PersonQueries : ObjectGraphType<object>
    {
        public PersonQueries(IMediator mediator)
        {
            this.Name = "PersonQueries";
            this.Description = "This is the base query for all the people with their information in our object graph";

            #region Person

            _ = this.Field<ListGraphType<PersonType>>("people")
                .Description("Gets a list of all people")
                .ResolveAsync(async context => await mediator.Send(new GetAllPeopleQuery()));

            _ = this.Field<PersonType>("person")
                .Description("Gets a person by unique identifier")
                .Arguments(new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>>
                    {
                        Name = "id",
                        Description = "The unique GUID of the person."
                    }))
                .ResolveAsync(async context => await mediator.Send(new GetPersonByIdQuery(context.GetArgument("id", Guid.Empty))));

            _ = this.Field<ListGraphType<PersonType>>("peopleByFirstName")
                .Description("Gets a list of people by first name")
                .Arguments(new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>>
                    {
                        Name = "firstname",
                        Description = "The first name of people."
                    }))
                .ResolveAsync(async context => await mediator.Send(new GetAllPeopleByFirstNameQuery(context.GetArgument("firstname", string.Empty))));

            _ = this.Field<ListGraphType<PersonType>>("peopleByLastName")
                .Description("Gets a list of people by last name")
                .Arguments(new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>>
                    {
                        Name = "lastname",
                        Description = "The last name of people."
                    }))
                .ResolveAsync(async context => await mediator.Send(new GetAllPeopleByLastNameQuery(context.GetArgument("lastname", string.Empty))));

            _ = this.Field<ListGraphType<PersonType>>("peopleByMiddleInitial")
                .Description("Gets a list of people by middle initial")
                .Arguments(new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>>
                    {
                        Name = "middleinitial",
                        Description = "The middle initial of people."
                    }))
                .ResolveAsync(async context => await mediator.Send(new GetAllPeopleByMiddleInitialQuery(context.GetArgument("middleinitial", string.Empty))));

            _ = this.Field<ListGraphType<PersonType>>("peopleByTitle")
                .Description("Gets a list of people by title")
                .Arguments(new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>>
                    {
                        Name = "title",
                        Description = "The title of people."
                    }))
                .ResolveAsync(async context => await mediator.Send(new GetAllPeopleByTitleQuery(context.GetArgument("title", string.Empty))));

            #endregion

        }
    }
}
