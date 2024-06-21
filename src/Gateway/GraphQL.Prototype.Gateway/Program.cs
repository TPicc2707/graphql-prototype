using GraphQL.Prototype.Gateway.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient(WellKnownSchemaNames.People, c => c.BaseAddress = new Uri(WellKnownSchemaUrls.PeopleUrl));
builder.Services.AddHttpClient(WellKnownSchemaNames.Addresses, c => c.BaseAddress = new Uri(WellKnownSchemaUrls.AddressesUrl));

builder.Services
    .AddGraphQLServer()
    .AddRemoteSchema(WellKnownSchemaNames.People)
    .AddRemoteSchema(WellKnownSchemaNames.Addresses);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.MapGraphQL();
app.Run();
