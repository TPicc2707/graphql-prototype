using GraphQL.Address.API.GraphQL.Types;
using GraphQL.Address.Application.Features.Address.Queries.GetAllPersonAddress;
using GraphQL.Address.Application.Features.Address.Queries.GetAllPersonAddressByCity;
using GraphQL.Address.Application.Features.Address.Queries.GetAllPersonAddressByPersonId;
using GraphQL.Address.Application.Features.Address.Queries.GetAllPersonAddressByState;
using GraphQL.Address.Application.Features.Address.Queries.GetAllPersonAddressByStreet;
using GraphQL.Address.Application.Features.Address.Queries.GetAllPersonAddressByType;
using GraphQL.Address.Application.Features.Address.Queries.GetAllPersonAddressByZipCode;
using GraphQL.Address.Application.Features.Address.Queries.GetPersonAddressById;
using GraphQL.Types;
using MediatR;

namespace GraphQL.Address.API.GraphQL.Queries
{
    public class PersonAddressQueries : ObjectGraphType<object>
    {
        public PersonAddressQueries(IMediator mediator)
        {
            this.Name = "PersonAddressQueries";
            this.Description = "This is the base query for all the people with their information in our object graph";

            #region Address

            _ = this.Field<ListGraphType<AddressType>>("addresses")
                .Description("Gets a list of all addresses")
                .ResolveAsync(async context => await mediator.Send(new GetAllPersonAddressQuery()));

            _ = this.Field<AddressType>("address")
                .Description("Gets a address by unique identifier")
                .Arguments(new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>>
                    {
                        Name = "id",
                        Description = "The unique GUID of the address."
                    }))
                .ResolveAsync(async context => await mediator.Send(new GetPersonAddressByIdQuery(context.GetArgument("id", Guid.Empty))));

            _ = this.Field<ListGraphType<AddressType>>("personAddresses")
                .Description("Gets all person addresses by unique identifier")
                .Arguments(new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>>
                    {
                        Name = "id",
                        Description = "The unique GUID of the person."
                    }))
                .ResolveAsync(async context => await mediator.Send(new GetAllPersonAddressByPersonIdQuery(context.GetArgument("id", Guid.Empty))));

            _ = this.Field<ListGraphType<AddressType>>("addressesByCity")
                .Description("Gets all addresses by city")
                .Arguments(new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>>
                    {
                        Name = "city",
                        Description = "The city of the addresses."
                    }))
                .ResolveAsync(async context => await mediator.Send(new GetAllPersonAddressByCityQuery(context.GetArgument("city", string.Empty))));

            _ = this.Field<ListGraphType<AddressType>>("addressesByState")
                .Description("Gets all addresses by state")
                .Arguments(new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>>
                    {
                        Name = "state",
                        Description = "The state of the addresses."
                    }))
                .ResolveAsync(async context => await mediator.Send(new GetAllPersonAddressByStateQuery(context.GetArgument("state", string.Empty))));

            _ = this.Field<ListGraphType<AddressType>>("addressesByStreet")
                .Description("Gets all addresses by street name")
                .Arguments(new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>>
                    {
                        Name = "street",
                        Description = "The street name of the addresses."
                    }))
                .ResolveAsync(async context => await mediator.Send(new GetAllPersonAddressByStreetQuery(context.GetArgument("street", string.Empty))));

            _ = this.Field<ListGraphType<AddressType>>("addressesByType")
                .Description("Gets all addresses by type")
                .Arguments(new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>>
                    {
                        Name = "type",
                        Description = "The type of the addresses."
                    }))
                .ResolveAsync(async context => await mediator.Send(new GetAllPersonAddressByTypeQuery(context.GetArgument("type", string.Empty))));

            _ = this.Field<ListGraphType<AddressType>>("addressesByZipCode")
                .Description("Gets all addresses by zip code")
                .Arguments(new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>>
                    {
                        Name = "zipcode",
                        Description = "The zip code of the addresses."
                    }))
                .ResolveAsync(async context => await mediator.Send(new GetAllPersonAddressByZipCodeQuery(context.GetArgument("zipcode", string.Empty))));

            #endregion 
        }
    }
}
