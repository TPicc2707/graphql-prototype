using GraphQL;
using GraphQL.Types;
using GraphQL.Address.Application;
using GraphQL.Address.Infrastructure;
using GraphQL.Address.API;
using GraphQL.Address.Domain;
using GraphQL.Address.Infrastructure.Persistence;
using GraphQL.Address.API.Extensions;
using MassTransit;
using GraphQL.EventBus.Messages.Common;
using GraphQL.Address.API.EventBusConsumer;
using GraphQL.Address.Application.Features.Address.Queries.GetAllPersonAddress;
using MediatR;
using GraphQL.Address.Application.Features.Address.Queries.GetPersonAddressById;
using GraphQL.Address.Application.Features.Address.Queries.GetAllPersonAddressByPersonId;
using GraphQL.Address.Application.Features.Address.Queries.GetAllPersonAddressByCity;
using GraphQL.Address.Application.Features.Address.Queries.GetAllPersonAddressByState;
using GraphQL.Address.Application.Features.Address.Queries.GetAllPersonAddressByStreet;
using GraphQL.Address.Application.Features.Address.Queries.GetAllPersonAddressByType;
using GraphQL.Address.Application.Features.Address.Queries.GetAllPersonAddressByZipCode;
using GraphQL.Address.Application.Dtos.Address;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add services to the container.
builder.Services.AddAPIServices();
builder.Services.AddApplicationServices();
builder.Services.AddDomainServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<CreatePersonConsumer>();
    config.AddConsumer<UpdatePersonConsumer>();
    config.AddConsumer<DeletePersonConsumer>();

    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
        cfg.ReceiveEndpoint(EventBusConstants.CreatePersonQueue, c =>
        {
            c.ConfigureConsumer<CreatePersonConsumer>(ctx);
        });
        cfg.ReceiveEndpoint(EventBusConstants.UpdatePersonQueue, c =>
        {
            c.ConfigureConsumer<UpdatePersonConsumer>(ctx);
        });
        cfg.ReceiveEndpoint(EventBusConstants.DeletePersonQueue, c =>
        {
            c.ConfigureConsumer<DeletePersonConsumer>(ctx);
        });

    });
});

builder.Services.AddControllers();

builder.Services.AddGraphQL(b => b.AddAutoSchema<ISchema>().AddSystemTextJson());

var app = builder.Build();

if (!app.Environment.IsStaging())
{
    var populate = new ConfigurationBuilder().AddJsonFile("appsettings.json")
                    .Build()
                    .GetSection("SeedDatabase")["PopulateData"];

    app.MigrateDatabase<PersonContext>((context, services) =>
    {
        var logger = services.GetService<ILogger<PersonSeedData>>();
        PersonSeedData.SeedAsync(context, logger, populate).Wait(10000);
    });
}

app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

var addressGroup = app.MapGroup("/address")
    .WithTags("Address").WithOpenApi(operation => new(operation)
    {
        Summary = "Provides the ability to manage the Person Address records."
    });

addressGroup.MapPost("/address", async (IMediator mediator, [FromBody] CreateAddressDto address) =>
{
    var createdAddress = await mediator.Send(address);
    return Results.CreatedAtRoute(routeName: "GetAddressById", routeValues: new { id = createdAddress.AddressId });
}).WithOpenApi(operation => new(operation)
{
    Summary = "Creates the Person Address record."
})
.Produces<AddressDto>(StatusCodes.Status201Created)
.Produces<ProblemDetails>(StatusCodes.Status400BadRequest);

addressGroup.MapPut("/address", async (IMediator mediator, [FromBody] UpdateAddressDto address) =>
{
    await mediator.Send(address);
    return Results.NoContent();
}).WithOpenApi(operation => new(operation)
{
    Summary = "Updates the Person Address record."
})
.Produces(StatusCodes.Status204NoContent)
.Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
.Produces<ProblemDetails>(StatusCodes.Status404NotFound);

addressGroup.MapDelete("/address", async (IMediator mediator, [FromBody] DeleteAddressDto address) =>
{
    await mediator.Send(address);
    return Results.NoContent();
}).WithOpenApi(operation => new(operation)
{
    Summary = "Deletes the Person Address record."
})
.Produces(StatusCodes.Status204NoContent)
.Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
.Produces<ProblemDetails>(StatusCodes.Status404NotFound);

addressGroup.MapGet("/addresses", async (IMediator mediator) => Results.Ok(await mediator.Send(new GetAllPersonAddressQuery())))
    .WithName("GetAllAddresses").WithOpenApi(operation => new(operation)
    {
        Summary = "Retrieves all Person Address records",
        RequestBody = null
    })
    .Produces<List<AddressDto>>(StatusCodes.Status200OK);

addressGroup.MapGet("/address/{id}", async (IMediator mediator, string id) =>
{
    Guid.TryParse(id, out Guid validAddressId);
    var result = await mediator.Send(new GetPersonAddressByIdQuery(validAddressId));
    return Results.Ok(result);
}).WithName("GetAddressById").WithOpenApi(operation => new(operation)
{
    Summary = "Retrieves a Person Address record by Id.",
    RequestBody = null
})
.Produces<AddressDto>(StatusCodes.Status200OK)
.Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
.Produces<ProblemDetails>(StatusCodes.Status404NotFound);

addressGroup.MapGet("/personaddresses/{personId}", async (IMediator mediator, string personId) =>
{
    Guid.TryParse(personId, out Guid validPersonId);
    var result = await mediator.Send(new GetAllPersonAddressByPersonIdQuery(validPersonId));
    return Results.Ok(result);

}).WithName("GetAllAddressesByPersonId").WithOpenApi(operation => new(operation)
{
    Summary = "Retrieves all Person Address records by Person Id.",
    RequestBody = null
})
.Produces<List<AddressDto>>(StatusCodes.Status200OK)
.Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
.Produces<ProblemDetails>(StatusCodes.Status404NotFound);

addressGroup.MapGet("/addressesbycity/{city}", async (IMediator mediator, string city) => Results.Ok(await mediator.Send(new GetAllPersonAddressByCityQuery(city))))
    .WithName("GetAllAddressesByCity").WithOpenApi(operation => new(operation)
    {
        Summary = "Retrieves all Person Address records by City.",
        RequestBody = null
    })
    .Produces<List<AddressDto>>(StatusCodes.Status200OK)
    .Produces<ProblemDetails>(StatusCodes.Status400BadRequest);

addressGroup.MapGet("/addressesbystate/{state}", async (IMediator mediator, string state) => Results.Ok(await mediator.Send(new GetAllPersonAddressByStateQuery(state))))
    .WithName("GetAllAddressesByState").WithOpenApi(operation => new(operation)
    {
        Summary = "Retrieves all Person Address records by State.",
        RequestBody = null
    })
    .Produces<List<AddressDto>>(StatusCodes.Status200OK)
    .Produces<ProblemDetails>(StatusCodes.Status400BadRequest);

addressGroup.MapGet("/addressesbystreet/{street}", async (IMediator mediator, string street) => Results.Ok(await mediator.Send(new GetAllPersonAddressByStreetQuery(street))))
    .WithName("GetAllAddressesByStreet").WithOpenApi(operation => new(operation)
    {
        Summary = "Retrieves all Person Address records by Street.",
        RequestBody = null
    })
    .Produces<List<AddressDto>>(StatusCodes.Status200OK)
    .Produces<ProblemDetails>(StatusCodes.Status400BadRequest);

addressGroup.MapGet("/addressesbytype/{type}", async (IMediator mediator, string type) => Results.Ok(await mediator.Send(new GetAllPersonAddressByTypeQuery(type))))
    .WithName("GetAllAddressesByType").WithOpenApi(operation => new(operation)
    {
        Summary = "Retrieves all Person Address records by Type.",
        RequestBody = null
    })
    .Produces<List<AddressDto>>(StatusCodes.Status200OK)
    .Produces<ProblemDetails>(StatusCodes.Status400BadRequest);

addressGroup.MapGet("/addressesbyzipcode/{zipCode}", async (IMediator mediator, string zipCode) => Results.Ok(await mediator.Send(new GetAllPersonAddressByZipCodeQuery(zipCode))))
    .WithName("GetAllAddressesByZipCode").WithOpenApi(operation => new(operation)
    {
        Summary = "Retrieves all Person Address records by Zip Code.",
        RequestBody = null
    })
    .Produces<List<AddressDto>>(StatusCodes.Status200OK)
    .Produces<ProblemDetails>(StatusCodes.Status400BadRequest);

app.UseGraphQLAltair("/");
app.UseGraphQL<ISchema>();

app.UseAuthorization();

app.MapControllers();

app.Run();
