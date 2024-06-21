using AutoMapper;
using GraphQL.Address.API.GraphQL.Mutations;
using GraphQL.Address.API.Mapping;
using GraphQL.Address.Domain.Services.Validation;
using GraphQL.Address.Infrastructure.Persistence;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using GraphQL.Prototype.Tests.Data.Address;
using GraphQL.Prototype.Tests.Data.TestingFramework;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GraphQL.Address.API.Tests.GraphQL.Mutations
{
    public class PersonAddressMutationsTests
    {
        private readonly IMockNsubstituteMethods _mockingFramework;
        private readonly IMapper _mapper;
        private readonly AddressValidator _addressValidator = new AddressValidator();
        private readonly HttpClient client;
        private readonly GraphQLHttpClient graphClient;


        public PersonAddressMutationsTests()
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
                            var addressTestData = AddressTestData.SeedPersonAddressTestData();
                            db.Addresses.RemoveRange(addressTestData);
                            db.People.RemoveRange(personTestData);
                            db.People.AddRange(personTestData);
                            db.Addresses.AddRange(addressTestData);
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
            graphClient = new GraphQLHttpClient(new GraphQLHttpClientOptions { EndPoint = new Uri("http://localhost:5157/graphql") }, new SystemTextJsonSerializer(), client);
        }

        #region Constructor

        [Fact]
        public void PersonAddressMutations_Constructor_WhenInitiated_ShouldReturnCorrectType()
        {
            //Arrange/Act
            var mockMediatorObject = _mockingFramework.InitializeMockedClass<IMediator>(new object[] { });
            var mutation = new PersonAddressMutations(mockMediatorObject);

            //Assert
            Assert.IsType<PersonAddressMutations>(mutation);
        }


        #endregion

        #region CreatePersonAddress

        [Fact]
        public async Task PersonAddressMutations_CreatePersonAddress_When_Called_Return_Expected_Result()
        {
            //Arrange
            var mutation = new GraphQLRequest
            {
                Query = @"mutation TestQuery($address:CreatePersonAddressInputType!)
                        {
                            createPersonAddress(input:$address)
                            {
                                personId,
                                type,
                                street,
                                city,
                                state,
                                zipCode
                            }
                        }",
                Variables =
                new
                {
                    address = new
                    {
                        personId = new Guid("bb531538-3bc3-4e88-bbfc-dac101abad2a"),
                        type = "Work",
                        street = "1236 Blue Avenue",
                        city = "Atlanta",
                        state = "GA",
                        zipCode = "72912"
                    }
                }
            };

            //Act
            var response = await graphClient.SendMutationAsync<dynamic>(mutation);

            //Assert
            Assert.Null(response.Errors);
        }

        [Fact]
        public async Task PersonAddressMutations_CreatePersonAddress_When_Called_Return_Bad_Request()
        {
            //Arrange
            var mutation = new GraphQLRequest
            {
                Query = @"mutation TestQuery($address:CreatePersonAddressInputType!)
                        {
                            createPersonAddress(input:$address)
                            {
                                personId,
                                street,
                                city,
                                state,
                                type,
                                zipCode
                            }
                        }",
                Variables =
                new
                {
                    address = "Bill"
                }
            };

            //Act
            var response = await graphClient.SendMutationAsync<dynamic>(mutation);

            //Assert
            var count = response.Errors.Count();
            Assert.Equal(1, count);
        }

        #endregion

        #region UpdatePersonAddress

        [Fact]
        public async Task PersonAddressMutations_UpdatePersonAddress_When_Called_Return_Expected_Result()
        {
            //Arrange
            var mutation = new GraphQLRequest
            {
                Query = @"mutation TestQuery($address:UpdatePersonAddressInputType!)
                        {
                            updatePersonAddress(input:$address)
                        }",
                Variables =
                new
                {
                    address = new
                    {
                        addressId = new Guid("50ca7492-6771-4e79-b7d8-59f996344e2e"),
                        personId = new Guid("bb531538-3bc3-4e88-bbfc-dac101abad2a"),
                        street = "1236 Blue Avenue",
                        city = "Atlanta",
                        state = "GA",
                        type = "Work",
                        zipCode = "72912"
                    }
                }
            };

            //Act
            var response = await graphClient.SendMutationAsync<dynamic>(mutation);

            //Assert
            Assert.Null(response.Errors);
        }

        [Fact]
        public async Task PersonAddressMutations_UpdatePersonAddress_When_Called_Return_Bad_Request()
        {
            //Arrange
            var mutation = new GraphQLRequest
            {
                Query = @"mutation TestQuery($address:UpdatePersonAddressInputType!)
                        {
                            updatePersonAddress(input:$address)
                        }",
                Variables =
                new
                {
                    address = "Bill"
                }
            };

            //Act
            var response = await graphClient.SendMutationAsync<dynamic>(mutation);

            //Assert
            var count = response.Errors.Count();
            Assert.Equal(1, count);
        }


        #endregion

        #region DeletePersonAddress

        [Fact]
        public async Task PersonAddressMutations_DeletePersonAddress_When_Called_Return_Expected_Result()
        {
            //Arrange
            var mutation = new GraphQLRequest
            {
                Query = @"mutation TestQuery($address:DeletePersonAddressInputType!)
                        {
                            deletePersonAddress(input:$address)
                        }",
                Variables =
                new
                {
                    address = new
                    {
                        addressId = new Guid("50ca7492-6771-4e79-b7d8-59f996344e2e")
                    }
                }
            };

            //Act
            var response = await graphClient.SendMutationAsync<dynamic>(mutation);

            //Assert
            Assert.Null(response.Errors);
        }

        [Fact]
        public async Task PersonAddressMutations_DeletePersonAddress_When_Called_Return_Bad_Request()
        {
            //Arrange
            var mutation = new GraphQLRequest
            {
                Query = @"mutation TestQuery($address:DeletePersonAddressInputType!)
                        {
                            deletePersonAddress(input:$address)
                        }",
                Variables =
                new
                {
                    address = "Bill"
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
