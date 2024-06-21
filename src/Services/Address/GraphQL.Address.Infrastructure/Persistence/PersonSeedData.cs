using GraphQL.Address.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace GraphQL.Address.Infrastructure.Persistence
{
    public class PersonSeedData
    {
        public static async Task SeedAsync(PersonContext personContext,
                                    ILogger<PersonSeedData> logger, string populate)
        {
            try
            {
                if (populate == "true" && !personContext.People.Any())
                {
                    await personContext.People.AddRangeAsync(SeedPersonData());
                    await personContext.SaveChangesAsync();
                    logger.LogInformation("Seed database with person information.");
                }
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }

        private static IEnumerable<Person> SeedPersonData()
        {
            return new List<Person>
            {
                Seed_First_Person_data(),
                Seed_Second_Person_data(),
                Seed_Third_Person_data(),
                Seed_Fourth_Person_data()
            };
        }


        private static Person Seed_First_Person_data()
        {
            var data = new Person
            {
                PersonId = new Guid("ea70f055-9d1f-48da-a666-b01286f958cb"),
                FirstName = "Anthony",
                MiddleInitial = "N",
                LastName = "Piccirilli",
                Title = "Mr.",
                Addresses = new List<Domain.Entities.Address>
                {
                    new Domain.Entities.Address
                    {
                        AddressId = new Guid("50ca7492-6771-4e79-b7d8-59f996344e2e"),
                        Street = "123 Main Street",
                        City = "Louisville",
                        State = "KY",
                        Type = "Home",
                        ZipCode = "12345"
                    },
                    new Domain.Entities.Address
                    {
                        AddressId = new Guid("5e71e217-7e4a-4b7e-a72d-df26d4f6d301"),
                        Street = "1244 Red Station",
                        City = "Louisville",
                        State = "KY",
                        Type = "Work",
                        ZipCode = "12346"
                    }

                }
            };

            return data;
        }

        private static Person Seed_Second_Person_data()
        {
            var data = new Person
            {
                PersonId = new Guid("6981c898-25d5-4c3b-95e7-da766434c577"),
                FirstName = "Jake",
                MiddleInitial = "R",
                LastName = "Smith",
                Title = "Dr.",
                Addresses = new List<Domain.Entities.Address>
                {
                    new Domain.Entities.Address
                    {
                        AddressId = new Guid("37c5496d-2b52-41f0-a827-477aa26b8e74"),
                        Street = "7883 Secondary Drive",
                        City = "Pittsburgh",
                        State = "PA",
                        Type = "Home",
                        ZipCode = "03923"
                    }

                }
            };

            return data;
        }

        private static Person Seed_Third_Person_data()
        {
            var data = new Person
            {
                PersonId = new Guid("b3f8aeea-fb6b-4d45-bb52-28a0bd051c4e"),
                FirstName = "Patty",
                MiddleInitial = "L",
                LastName = "Jackson",
                Title = "Mrs.",
                Addresses = new List<Domain.Entities.Address>
                {
                    new Domain.Entities.Address
                    {
                        AddressId = new Guid("e4882f3f-d139-4b17-9124-9183a672ecc5"),
                        Street = "3423 Carter Street",
                        City = "Boston",
                        State = "MA",
                        Type = "Home",
                        ZipCode = "93232"
                    },
                    new Domain.Entities.Address
                    {
                        AddressId = new Guid("18a1c983-391f-4309-b06f-7b55cbe24e8c"),
                        Street = "2312 Light Boulevard",
                        City = "Boston",
                        State = "MA",
                        Type = "Work",
                        ZipCode = "93236"
                    }
                }
            };

            return data;
        }

        private static Person Seed_Fourth_Person_data()
        {
            var data = new Person
            {
                PersonId = new Guid("9e6b5924-3b82-4bf3-a015-22e245fc35ed"),
                FirstName = "Jerry",
                MiddleInitial = "B",
                LastName = "Johnson",
                Title = "Mrs.",
                Addresses = new List<Domain.Entities.Address>
                {
                    new Domain.Entities.Address
                    {
                        AddressId = new Guid("07c8d8f8-03b0-4261-963f-010d9e9c0a7d"),
                        Street = "3423 Carter Street",
                        City = "Albany",
                        State = "NY",
                        Type = "Home",
                        ZipCode = "3212"
                    }
                }
            };

            return data;
        }

    }
}
