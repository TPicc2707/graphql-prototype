using AutoMapper;
using GraphQL.EventBus.Messages.Events;
using GraphQL.Person.API.GraphQL.InputTypes.Person;
using GraphQL.Person.API.GraphQL.Types;
using GraphQL.Person.Application.Dtos.Person;
using GraphQL.Types;
using MassTransit;
using MediatR;

namespace GraphQL.Person.API.GraphQL.Mutations
{
    public class PersonMutations : ObjectGraphType<object>
    {
        public PersonMutations(IMediator mediator, IMapper mapper, IPublishEndpoint publishEndpoint, IHostEnvironment env)
        {
            this.Name = "PersonMutations";
            this.Description = "This is the base mutation for all the people with their information in our object graph";

            #region Person

            _ = this.Field<PersonType, PersonDto>("createPerson")
                .Arguments(new QueryArguments(new QueryArgument<NonNullGraphType<CreatePersonInputType>> { Name = "input" }))
                .ResolveAsync(async (context) =>
                {
                    try
                    {
                        var input = context.GetArgument<CreatePersonDto>("input");
                        ArgumentNullException.ThrowIfNull(input);

                        var person = await mediator.Send(input);

                        if (!env.IsStaging())
                        {
                            var eventMessage = mapper.Map<CreatePersonEvent>(person);
                            await publishEndpoint.Publish(eventMessage);
                        }

                        return person;
                    }
                    catch (Exception ex)
                    {
                        context.Errors.Add(new ExecutionError("An error occured while attempting to create the person.", ex));
                        throw;
                    }
                });

            _ = this.Field<BooleanGraphType>("updatePerson")
                .Arguments(new QueryArguments(new QueryArgument<NonNullGraphType<UpdatePersonInputType>> { Name = "input" }))
                .ResolveAsync(async (context) =>
                {
                    try
                    {
                        var input = context.GetArgument<UpdatePersonDto>("input");

                        ArgumentNullException.ThrowIfNull(input);

                        var isUpdated = await mediator.Send(input);

                        if (isUpdated && !env.IsStaging())
                        {
                            var eventMessage = mapper.Map<UpdatePersonEvent>(input);
                            await publishEndpoint.Publish(eventMessage);

                            return isUpdated;
                        }
                        else
                        {
                            return isUpdated;
                        }
                    }
                    catch (Exception ex)
                    {
                        context.Errors.Add(new ExecutionError("An error occured while attempting to update the person.", ex));
                        throw;
                    }
                });

            _ = this.Field<BooleanGraphType>("deletePerson")
                .Arguments(new QueryArguments(new QueryArgument<NonNullGraphType<DeletePersonInputType>> { Name = "input" }))
                .ResolveAsync(async (context) =>
                {
                    try
                    {
                        var input = context.GetArgument<DeletePersonDto>("input");

                        ArgumentNullException.ThrowIfNull(input);

                        var isDeleted = await mediator.Send(input);

                        if (isDeleted && !env.IsStaging())
                        {
                            var eventMessage = mapper.Map<DeletePersonEvent>(input);
                            await publishEndpoint.Publish(eventMessage);

                            return isDeleted;
                        }
                        else
                        {
                            return isDeleted;
                        }
                    }
                    catch (Exception ex)
                    {
                        context.Errors.Add(new ExecutionError("An error occured while attempting to delete the person.", ex));
                        throw;
                    }
                });


            #endregion

        }
    }
}
