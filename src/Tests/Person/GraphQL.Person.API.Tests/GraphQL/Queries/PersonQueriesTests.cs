using AutoMapper;
using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using GraphQL.Person.API.GraphQL.Queries;
using GraphQL.Person.API.GraphQL.Types;
using GraphQL.Person.API.Mapping;
using GraphQL.Person.Domain.Services.Validation;
using GraphQL.Person.Infrastructure.Persistence;
using GraphQL.Prototype.Tests.Data.Person;
using GraphQL.Prototype.Tests.Data.TestingFramework;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Net.Http;
using System.Text;

namespace GraphQL.Person.API.Tests.GraphQL.Queries
{
    public class PersonQueriesTests
    {
        private readonly IMockNsubstituteMethods _mockingFramework;
        private readonly IMapper _mapper;
        private readonly PersonValidator _personValidator = new PersonValidator();
        private readonly HttpClient client;
        private readonly GraphQLHttpClient graphClient;


        public PersonQueriesTests()
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
        public void PersonQueries_Constructor_WhenInitiated_ShouldReturnCorrectType()
        {
            //Arrange/Act
            var mockMediatorObject = _mockingFramework.InitializeMockedClass<IMediator>(new object[] { });
            var queries = new PersonQueries(mockMediatorObject);

            //Assert
            Assert.IsType<PersonQueries>(queries);
        }


        #endregion

        #region People

        [Fact]
        public async Task PersonQueries_People_When_Called_Return_Expected_Result()
        {
            //Arrange
            var query = new GraphQLRequest
            {
                Query = @"query TestQuery
                        {
                            people
                            {
                                personId,
                                firstName,
                                middleInitial,
                                lastName,
                                title
                            }
                        }"
            };

            //Act
            var response = await graphClient.SendQueryAsync<dynamic>(query);

            //Assert
            Assert.Null(response.Errors);
        }

        #endregion

        #region Person

        [Fact]
        public async Task PersonQueries_Person_When_Called_Return_Expected_Result()
        {
            //Arrange
            var query = new GraphQLRequest
            {
                Query = @"query TestQuery($personId:ID!)
                        {
                            person(id:$personId)
                            {
                                personId
                                firstName,
                                middleInitial,
                                lastName,
                                title
                            }
                        }",
                Variables =
                new
                {
                    personId = new Guid("119d7fd2-d3ab-4ec5-a5a6-ad1b0a9ebaa9")
                }
            };

            //Act
            var response = await graphClient.SendQueryAsync<dynamic>(query);

            //Assert
            Assert.Null(response.Errors);
        }

        [Fact]
        public async Task PersonQueries_Person_When_Called_Return_Error()
        {
            //Arrange
            var query = new GraphQLRequest
            {
                Query = @"query TestQuery($personId:ID!)
                        {
                            person(id:$personId)
                            {
                                personId
                                firstName,
                                middleInitial,
                                lastName,
                                title
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

        #region PeopleByFirstName

        [Fact]
        public async Task PersonQueries_PeopleByFirstName_When_Called_Return_Expected_Result()
        {
            //Arrange
            var query = new GraphQLRequest
            {
                Query = @"query TestQuery($firstName:String!)
                        {
                            peopleByFirstName(firstname:$firstName)
                            {
                                personId
                                firstName,
                                middleInitial,
                                lastName,
                                title
                            }
                        }",
                Variables =
                new {
                    firstName = "Anthony"
                }
            };

            //Act
            var response = await graphClient.SendQueryAsync<dynamic>(query);

            //Assert
            Assert.Null(response.Errors);
        }

        [Fact]
        public async Task PersonQueries_PeopleByFirstName_When_Called_Return_Error()
        {
            //Arrange
            var query = new GraphQLRequest
            {
                Query = @"query TestQuery($firstName:String!)
                        {
                            peopleByFirstName(firstname:$firstName)
                            {
                                personId
                                firstName,
                                middleInitial,
                                lastName,
                                title
                            }
                        }",
                Variables =
                new
                {
                    firstName = 1
                }
            };

            //Act
            var response = await graphClient.SendQueryAsync<dynamic>(query);

            //Assert
            var count = response.Errors.Count();
            Assert.Equal(1, count);
        }


        #endregion

        #region PeopleByLastName

        [Fact]
        public async Task PersonQueries_PeopleByLastName_When_Called_Return_Expected_Result()
        {
            //Arrange
            var query = new GraphQLRequest
            {
                Query = @"query TestQuery($lastName:String!)
                        {
                            peopleByLastName(lastname:$lastName)
                            {
                                personId
                                firstName,
                                middleInitial,
                                lastName,
                                title
                            }
                        }",
                Variables =
                new
                {
                    lastName = "Piccirilli"
                }
            };

            //Act
            var response = await graphClient.SendQueryAsync<dynamic>(query);

            //Assert
            Assert.Null(response.Errors);
        }

        [Fact]
        public async Task PersonQueries_PeopleByLastName_When_Called_Return_Error()
        {
            //Arrange
            var query = new GraphQLRequest
            {
                Query = @"query TestQuery($lastName:String!)
                        {
                            peopleByLastName(lastname:$lastName)
                            {
                                personId
                                firstName,
                                middleInitial,
                                lastName,
                                title
                            }
                        }",
                Variables =
                new
                {
                    lastName = 1
                }
            };

            //Act
            var response = await graphClient.SendQueryAsync<dynamic>(query);

            //Assert
            var count = response.Errors.Count();
            Assert.Equal(1, count);
        }


        #endregion

        #region PeopleByMiddleInitial

        [Fact]
        public async Task PersonQueries_PeopleByMiddleInitial_When_Called_Return_Expected_Result()
        {
            //Arrange
            var query = new GraphQLRequest
            {
                Query = @"query TestQuery($middleInitial:String!)
                        {
                            peopleByMiddleInitial(middleinitial:$middleInitial)
                            {
                                personId
                                firstName,
                                middleInitial,
                                lastName,
                                title
                            }
                        }",
                Variables =
                new
                {
                    middleInitial = "N"
                }
            };

            //Act
            var response = await graphClient.SendQueryAsync<dynamic>(query);

            //Assert
            Assert.Null(response.Errors);
        }

        [Fact]
        public async Task PersonQueries_PeopleByMiddleInitial_When_Called_Return_Error()
        {
            //Arrange
            var query = new GraphQLRequest
            {
                Query = @"query TestQuery($middleInitial:String!)
                        {
                            peopleByMiddleInitial(middleinitial:$middleInitial)
                            {
                                personId
                                firstName,
                                middleInitial,
                                lastName,
                                title
                            }
                        }",
                Variables =
                new
                {
                    middleInitial = 1
                }
            };

            //Act
            var response = await graphClient.SendQueryAsync<dynamic>(query);

            //Assert
            var count = response.Errors.Count();
            Assert.Equal(1, count);
        }


        #endregion

        #region PeopleByTitle

        [Fact]
        public async Task PersonQueries_PeopleByTitle_When_Called_Return_Expected_Result()
        {
            //Arrange
            var query = new GraphQLRequest
            {
                Query = @"query TestQuery($title:String!)
                        {
                            peopleByTitle(title:$title)
                            {
                                personId
                                firstName,
                                middleInitial,
                                lastName,
                                title
                            }
                        }",
                Variables =
                new
                {
                    title = "Mr"
                }
            };

            //Act
            var response = await graphClient.SendQueryAsync<dynamic>(query);

            //Assert
            Assert.Null(response.Errors);
        }

        [Fact]
        public async Task PersonQueries_PeopleByTitle_When_Called_Return_Error()
        {
            //Arrange
            var query = new GraphQLRequest
            {
                Query = @"query TestQuery($title:String!)
                        {
                            peopleByTitle(title:$title)
                            {
                                personId
                                firstName,
                                middleInitial,
                                lastName,
                                title
                            }
                        }",
                Variables =
                new
                {
                    title = 1
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
