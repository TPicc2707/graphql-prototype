using AutoMapper;
using GraphQL.Address.API.GraphQL.Queries;
using GraphQL.Address.API.Mapping;
using GraphQL.Address.Domain.Services.Validation;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using GraphQL.Prototype.Tests.Data.TestingFramework;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace GraphQL.Address.API.Tests.GraphQL.Queries
{
    public class PersonAddressQueriesTest
    {
        private readonly IMockNsubstituteMethods _mockingFramework;
        private readonly IMapper _mapper;
        private readonly PersonValidator _personValidator = new PersonValidator();
        private readonly AddressValidator _addressValidator = new AddressValidator();
        private readonly HttpClient client;
        private readonly GraphQLHttpClient graphClient;


        public PersonAddressQueriesTest()
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
            });
            client = application.CreateClient();
            graphClient = new GraphQLHttpClient(new GraphQLHttpClientOptions { EndPoint = new Uri("http://localhost:5237/graphql") }, new SystemTextJsonSerializer(), client);
        }

        #region Constructor

        [Fact]
        public void PersonAddressQueries_Constructor_WhenInitiated_ShouldReturnCorrectType()
        {
            //Arrange/Act
            var mockMediatorObject = _mockingFramework.InitializeMockedClass<IMediator>(new object[] { });
            var queries = new PersonAddressQueries(mockMediatorObject);

            //Assert
            Assert.IsType<PersonAddressQueries>(queries);
        }


        #endregion

        #region Addresses

        [Fact]
        public async Task PersonAddressQueries_Addresses_When_Called_Return_Expected_Result()
        {
            //Arrange
            var query = new GraphQLRequest
            {
                Query = @"query TestQuery
                        {
                            addresses
                            {
                                addressId,
                                personId,
                                type,
                                street,
                                city,
                                state,
                                zipCode
                            }
                        }"
            };

            //Act
            var response = await graphClient.SendQueryAsync<dynamic>(query);

            //Assert
            Assert.Null(response.Errors);
        }

        #endregion

        #region Address

        [Fact]
        public async Task PersonAddressQueries_Address_When_Called_Return_Expected_Result()
        {
            //Arrange
            var query = new GraphQLRequest
            {
                Query = @"query TestQuery($addressId:ID!)
                        {
                            address(id:$addressId)
                            {
                                addressId,
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
                    addressId = new Guid("50ca7492-6771-4e79-b7d8-59f996344e2e")
                }
            };

            //Act
            var response = await graphClient.SendQueryAsync<dynamic>(query);

            //Assert
            Assert.Null(response.Errors);
        }

        [Fact]
        public async Task PersonAddressQueries_Address_When_Called_Return_Error()
        {
            //Arrange
            var query = new GraphQLRequest
            {
                Query = @"query TestQuery($addressId:ID!)
                        {
                            address(id:$addressId)
                            {
                                addressId,
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
                    addressId = "Bill"
                }
            };

            //Act
            var response = await graphClient.SendQueryAsync<dynamic>(query);

            //Assert
            var count = response.Errors.Count();
            Assert.Equal(1, count);
        }


        #endregion

        #region PersonAddresses

        [Fact]
        public async Task PersonAddressQueries_PersonAddresses_When_Called_Return_Expected_Result()
        {
            //Arrange
            var query = new GraphQLRequest
            {
                Query = @"query TestQuery($personId:ID!)
                        {
                            personAddresses(id:$personId)
                            {
                                addressId,
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
                    personId = new Guid("bb531538-3bc3-4e88-bbfc-dac101abad2a")
                }
            };

            //Act
            var response = await graphClient.SendQueryAsync<dynamic>(query);

            //Assert
            Assert.Null(response.Errors);
        }

        [Fact]
        public async Task PersonAddressQueries_PersonAddresses_When_Called_Return_Error()
        {
            //Arrange
            var query = new GraphQLRequest
            {
                Query = @"query TestQuery($personId:ID!)
                        {
                            personAddresses(id:$personId)
                            {
                                addressId,
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
                    personId = "Bill"
                }
            };

            //Act
            var response = await graphClient.SendQueryAsync<dynamic>(query);

            //Assert
            var count = response.Errors.Count();
            Assert.Equal(1, count);
        }


        #endregion

        #region AddressesByCity

        [Fact]
        public async Task PersonAddressQueries_AddressesByCity_When_Called_Return_Expected_Result()
        {
            //Arrange
            var query = new GraphQLRequest
            {
                Query = @"query TestQuery($city:String!)
                        {
                            addressesByCity(city:$city)
                            {
                                addressId,
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
                    city = "Louisville"
                }
            };

            //Act
            var response = await graphClient.SendQueryAsync<dynamic>(query);

            //Assert
            Assert.Null(response.Errors);
        }

        [Fact]
        public async Task PersonAddressQueries_AddressesByCity_When_Called_Return_Error()
        {
            //Arrange
            var query = new GraphQLRequest
            {
                Query = @"query TestQuery($city:String!)
                        {
                            addressesByCity(city:$city)
                            {
                                addressId,
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
                    city = 1
                }
            };

            //Act
            var response = await graphClient.SendQueryAsync<dynamic>(query);

            //Assert
            var count = response.Errors.Count();
            Assert.Equal(1, count);
        }


        #endregion

        #region AddressesByState

        [Fact]
        public async Task PersonAddressQueries_AddressesByState_When_Called_Return_Expected_Result()
        {
            //Arrange
            var query = new GraphQLRequest
            {
                Query = @"query TestQuery($state:String!)
                        {
                            addressesByState(state:$state)
                            {
                                addressId,
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
                    state = "KY"
                }
            };

            //Act
            var response = await graphClient.SendQueryAsync<dynamic>(query);

            //Assert
            Assert.Null(response.Errors);
        }

        [Fact]
        public async Task PersonAddressQueries_AddressesByState_When_Called_Return_Error()
        {
            //Arrange
            var query = new GraphQLRequest
            {
                Query = @"query TestQuery($state:String!)
                        {
                            addressesByState(state:$state)
                            {
                                addressId,
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
                    state = 1
                }
            };

            //Act
            var response = await graphClient.SendQueryAsync<dynamic>(query);

            //Assert
            var count = response.Errors.Count();
            Assert.Equal(1, count);
        }


        #endregion

        #region AddressesByStreet

        [Fact]
        public async Task PersonAddressQueries_AddressesByStreet_When_Called_Return_Expected_Result()
        {
            //Arrange
            var query = new GraphQLRequest
            {
                Query = @"query TestQuery($street:String!)
                        {
                            addressesByStreet(street:$street)
                            {
                                addressId,
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
                    street = "Main Street"
                }
            };

            //Act
            var response = await graphClient.SendQueryAsync<dynamic>(query);

            //Assert
            Assert.Null(response.Errors);
        }

        [Fact]
        public async Task PersonAddressQueries_AddressesByStreet_When_Called_Return_Error()
        {
            //Arrange
            var query = new GraphQLRequest
            {
                Query = @"query TestQuery($street:String!)
                        {
                            addressesByStreet(street:$street)
                            {
                                addressId,
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
                    street = 1
                }
            };

            //Act
            var response = await graphClient.SendQueryAsync<dynamic>(query);

            //Assert
            var count = response.Errors.Count();
            Assert.Equal(1, count);
        }


        #endregion

        #region AddressesByType

        [Fact]
        public async Task PersonAddressQueries_AddressesByType_When_Called_Return_Expected_Result()
        {
            //Arrange
            var query = new GraphQLRequest
            {
                Query = @"query TestQuery($type:String!)
                        {
                            addressesByType(type:$type)
                            {
                                addressId,
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
                    type = "Home"
                }
            };

            //Act
            var response = await graphClient.SendQueryAsync<dynamic>(query);

            //Assert
            Assert.Null(response.Errors);
        }

        [Fact]
        public async Task PersonAddressQueries_AddressesByType_When_Called_Return_Error()
        {
            //Arrange
            var query = new GraphQLRequest
            {
                Query = @"query TestQuery($type:String!)
                        {
                            addressesByType(type:$type)
                            {
                                addressId,
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
                    type = 1
                }
            };

            //Act
            var response = await graphClient.SendQueryAsync<dynamic>(query);

            //Assert
            var count = response.Errors.Count();
            Assert.Equal(1, count);
        }


        #endregion

        #region AddressesByZipCode

        [Fact]
        public async Task PersonAddressQueries_AddressesByZipCode_When_Called_Return_Expected_Result()
        {
            //Arrange
            var query = new GraphQLRequest
            {
                Query = @"query TestQuery($zipCode:String!)
                        {
                            addressesByZipCode(zipcode:$zipCode)
                            {
                                addressId,
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
                    zipCode = "Home"
                }
            };

            //Act
            var response = await graphClient.SendQueryAsync<dynamic>(query);

            //Assert
            Assert.Null(response.Errors);
        }

        [Fact]
        public async Task PersonAddressQueries_AddressesByZipCode_When_Called_Return_Error()
        {
            //Arrange
            var query = new GraphQLRequest
            {
                Query = @"query TestQuery($zipCode:String!)
                        {
                            addressesByZipCode(zipcode:$zipCode)
                            {
                                addressId,
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
                    zipCode = 1
                }
            };

            //Act
            var response = await graphClient.SendQueryAsync<dynamic>(query);

            //Assert
            var count = response.Errors.Count();
            Assert.Equal(1, count);
        }


        #endregion

    }
}
