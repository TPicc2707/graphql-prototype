using Microsoft.Extensions.Logging;

namespace GraphQL.Person.Infrastructure.Persistence
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

        private static IEnumerable<Domain.Entities.Person> SeedPersonData()
        {
            return new List<Domain.Entities.Person>
            {
                Seed_First_Person_data(),
                Seed_Second_Person_data(),
                Seed_Third_Person_data(),
                Seed_Fourth_Person_data()
            };
        }


        private static Domain.Entities.Person Seed_First_Person_data()
        {
            var data = new Domain.Entities.Person
            {
                PersonId = new Guid("ea70f055-9d1f-48da-a666-b01286f958cb"),
                FirstName = "Anthony",
                MiddleInitial = "N",
                LastName = "Piccirilli",
                Title = "Mr.",
            };

            return data;
        }

        private static Domain.Entities.Person Seed_Second_Person_data()
        {
            var data = new Domain.Entities.Person
            {
                PersonId = new Guid("6981c898-25d5-4c3b-95e7-da766434c577"),
                FirstName = "Jake",
                MiddleInitial = "R",
                LastName = "Smith",
                Title = "Dr.",
            };

            return data;
        }

        private static Domain.Entities.Person Seed_Third_Person_data()
        {
            var data = new Domain.Entities.Person
            {
                PersonId = new Guid("b3f8aeea-fb6b-4d45-bb52-28a0bd051c4e"),
                FirstName = "Patty",
                MiddleInitial = "L",
                LastName = "Jackson",
                Title = "Mrs.",
            };

            return data;
        }

        private static Domain.Entities.Person Seed_Fourth_Person_data()
        {
            var data = new Domain.Entities.Person
            {
                PersonId = new Guid("9e6b5924-3b82-4bf3-a015-22e245fc35ed"),
                FirstName = "Jerry",
                MiddleInitial = "B",
                LastName = "Johnson",
                Title = "Mrs.",
            };

            return data;
        }

    }
}
