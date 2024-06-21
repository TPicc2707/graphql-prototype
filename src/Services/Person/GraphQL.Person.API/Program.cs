using GraphQL;
using GraphQL.Types;
using GraphQL.Person.Application;
using GraphQL.Person.Infrastructure;
using GraphQL.Person.API;
using GraphQL.Person.Infrastructure.Persistence;
using GraphQL.Person.API.Extensions;
using MassTransit;
using GraphQL.Person.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAPIServices();
builder.Services.AddApplicationServices();
builder.Services.AddDomainServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddMassTransit(config =>
{
    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
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

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseGraphQLAltair("/");
app.UseGraphQL<ISchema>();

app.UseAuthorization();

app.MapControllers();

app.Run();

