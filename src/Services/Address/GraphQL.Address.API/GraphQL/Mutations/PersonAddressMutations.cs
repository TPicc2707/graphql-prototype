using GraphQL.Address.API.GraphQL.InputTypes.Address;
using GraphQL.Address.API.GraphQL.Types;
using GraphQL.Address.Application.Dtos.Address;
using GraphQL.Types;
using MediatR;

namespace GraphQL.Address.API.GraphQL.Mutations
{
    public class PersonAddressMutations : ObjectGraphType<object>
    {
        public PersonAddressMutations(IMediator mediator)
        {
            this.Name = "PersonAddressMutations";
            this.Description = "This is the base mutation for all the people with their information in our object graph";
 
            #region Address

            _ = this.Field<AddressType, AddressDto>("createPersonAddress")
                .Arguments(new QueryArguments(new QueryArgument<NonNullGraphType<CreateAddressInputType>> { Name = "input" }))
                .ResolveAsync(async (context) =>
                {
                    try
                    {
                        var input = context.GetArgument<CreateAddressDto>("input");
                        ArgumentNullException.ThrowIfNull(input);

                        return await mediator.Send(input);
                    }
                    catch (Exception ex)
                    {
                        context.Errors.Add(new ExecutionError("An error occured while attempting to create the person's address.", ex));
                        throw;
                    }
                });

            _ = this.Field<BooleanGraphType>("updatePersonAddress")
                .Arguments(new QueryArguments(new QueryArgument<NonNullGraphType<UpdateAddressInputType>> { Name = "input" }))
                .ResolveAsync(async (context) =>
                {
                    try
                    {
                        var input = context.GetArgument<UpdateAddressDto>("input");

                        ArgumentNullException.ThrowIfNull(input);

                        return await mediator.Send(input);
                    }
                    catch (Exception ex)
                    {
                        context.Errors.Add(new ExecutionError("An error occured while attempting to update the person's address.", ex));
                        throw;
                    }
                });

            _ = this.Field<BooleanGraphType>("deletePersonAddress")
                .Arguments(new QueryArguments(new QueryArgument<NonNullGraphType<DeleteAddressInputType>> { Name = "input" }))
                .ResolveAsync(async (context) =>
                {
                    try
                    {
                        var input = context.GetArgument<DeleteAddressDto>("input");

                        ArgumentNullException.ThrowIfNull(input);

                        return await mediator.Send(input);
                    }
                    catch (Exception ex)
                    {
                        context.Errors.Add(new ExecutionError("An error occured while attempting to delete the person's address.", ex));
                        throw;
                    }
                });


            #endregion
        }
    }
}
