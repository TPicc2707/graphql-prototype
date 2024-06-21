using AutoMapper;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using GraphQL.Person.API.GraphQL.Mutations;
using GraphQL.Person.API.Mapping;
using GraphQL.Person.Domain.Services.Validation;
using GraphQL.Person.Infrastructure.Persistence;
using GraphQL.Prototype.Tests.Data.Person;
using GraphQL.Prototype.Tests.Data.TestingFramework;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GraphQL.Person.API.Tests.GraphQL.Mutations
{
    public class PersonMutationsTests
    {
        private readonly IMockNsubstituteMethods _mockingFramework;
        private readonly IMapper _mapper;
        private readonly PersonValidator _personValidator = new PersonValidator();
        private readonly HttpClient client;
        private readonly GraphQLHttpClient graphClient;


        public PersonMutationsTests()
        {
            _mockingFramework = Helper.GetRequiredService<IMockNsubstituteMethods>() ?? throw new ArgumentNullException(nameof(IMockNsubstituteMethods));
            var mapConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MapperProfile>();
            });
            _mapper = mapConfig.CreateMapper();
            var application = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Staging");
                builder.ConfigureTestServices(services =>
                {
                    var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<PersonContext>));
                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                    }

                    services.AddDbContext<PersonContext>(options =>
                    {
                        options.UseInMemoryDatabase("InMemoryTestDb");
                    });

                    var sp = services.BuildServiceProvider();

                    using (var scope = sp.CreateScope())
                    {
                        var scopedServices = scope.ServiceProvider;
                        var db = scopedServices.GetRequiredService<PersonContext>();

                        db.Database.EnsureDeleted();

                        db.Database.EnsureCreated();

                        try
                        {
                            var personTestData = PersonTestData.SeedPersonTestData();
                            db.People.RemoveRange(personTestData);
                            db.People.AddRange(personTestData);
                            db.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }
                    }
                });
            });
            client = application.CreateClient();
            graphClient = new GraphQLHttpClient(new GraphQLHttpClientOptions { EndPoint = new Uri("http://localhost:5237/graphql") }, new SystemTextJsonSerializer(), client);
        }


        #region Constructor

        [Fact]
        public void PersonMutations_Constructor_WhenInitiated_ShouldReturnCorrectType()
        {
            //Arrange/Act
            var mockMediatorObject = _mockingFramework.InitializeMockedClass<IMediator>(new object[] { });
            var mockPublishEndpointObject = _mockingFramework.InitializeMockedClass<IPublishEndpoint>(new object[] { });
            var mockHostEnvironmentObject = _mockingFramework.InitializeMockedClass<IHostEnvironment>(new object[] { });
            var mutation = new PersonMutations(mockMediatorObject, _mapper, mockPublishEndpointObject, mockHostEnvironmentObject);

            //Assert
            Assert.IsType<PersonMutations>(mutation);
        }


        #endregion

        #region CreatePerson

        [Fact]
        public async Task PersonMutations_CreatePerson_When_Called_Return_Expected_Result()
        {
            //Arrange
            var mutation = new GraphQLRequest
            {
                Query = @"mutation TestQuery($person:CreatePersonInputType!)
                        {
                            createPerson(input:$person)
                            {
                                personId,
                                firstName,
                                middleInitial,
                                lastName,
                                title
                            }
                        }",
                Variables =
                new {
                    person = new {
                        firstName = "Robert",
                        middleInitial = "J",
                        lastName = "Downery",
                        title = "Mr"
                    }
                }
            };
            
            //Act
            var response = await graphClient.SendMutationAsync<dynamic>(mutation);

            //Assert
            Assert.Null(response.Errors);
        }

        [Fact]
        public async Task PersonMutations_CreatePerson_When_Called_Return_Bad_Request()
        {
            //Arrange
            var mutation = new GraphQLRequest
            {
                Query = @"mutation TestQuery($person:CreatePersonInputType!)
                        {
                            createPerson(input:$person)
                            {
                                personId,
                                firstName,
                                middleInitial,
                                lastName,
                                title
                            }
                        }",
                Variables =
                new
                {
                    person  = "Bill"
                }
            };

            //Act
            var response = await graphClient.SendMutationAsync<dynamic>(mutation);

            //Assert
            var count = response.Errors.Count();
            Assert.Equal(1, count);
        }


        #endregion

        #region UpdatePerson

        [Fact]
        public async Task PersonMutations_UpdatePerson_When_Called_Return_Expected_Result()
        {
            //Arrange
            var mutation = new GraphQLRequest
            {
                Query = @"mutation TestQuery($person:UpdatePersonInputType!)
                        {
                            updatePerson(input:$person)
                        }",
                Variables =
                new
                {
                    person = new
                    {
                        personId = new Guid("bb531538-3bc3-4e88-bbfc-dac101abad2a"),
                        firstName = "Robert",
                        middleInitial = "J",
                        lastName = "Downery",
                        title = "Mr"
                    }
                }
            };

            //Act
            var response = await graphClient.SendMutationAsync<dynamic>(mutation);

            //Assert
            Assert.Null(response.Errors);
        }

        [Fact]
        public async Task PersonMutations_UpdatePerson_When_Called_Return_Bad_Request()
        {
            //Arrange
            var mutation = new GraphQLRequest
            {
                Query = @"mutation TestQuery($person:UpdatePersonInputType!)
                        {
                            updatePerson(input:$person)
                        }",
                Variables =
                new
                {
                    person = "Bill"
                }
            };

            //Act
            var response = await graphClient.SendMutationAsync<dynamic>(mutation);

            //Assert
            var count = response.Errors.Count();
            Assert.Equal(1, count);
        }


        #endregion

        #region DeletePerson

        [Fact]
        public async Task PersonMutations_DeletePerson_When_Called_Return_Expected_Result()
        {
            //Arrange
            var mutation = new GraphQLRequest
            {
                Query = @"mutation TestQuery($person:DeletePersonInputType!)
                        {
                            deletePerson(input:$person)
                        }",
                Variables =
                new
                {
                    person = new
                    {
                        personId = new Guid("bb531538-3bc3-4e88-bbfc-dac101abad2a")
                    }
                }
            };

            //Act
            var response = await graphClient.SendMutationAsync<dynamic>(mutation);

            //Assert
            Assert.Null(response.Errors);
        }

        [Fact]
        public async Task PersonMutations_DeletePerson_When_Called_Return_Bad_Request()
        {
            //Arrange
            var mutation = new GraphQLRequest
            {
                Query = @"mutation TestQuery($person:DeletePersonInputType!)
                        {
                            deletePerson(input:$person)
                        }",
                Variables =
                new
                {
                    person = "Bill"
                }
            };

            //Act
            var response = await graphClient.SendMutationAsync<dynamic>(mutation);

            //Assert
            var count = response.Errors.Count();
            Assert.Equal(1, count);
        }


        #endregion

    }
}
