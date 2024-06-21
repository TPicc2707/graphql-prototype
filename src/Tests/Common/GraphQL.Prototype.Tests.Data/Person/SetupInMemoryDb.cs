using GraphQL.Person.Infrastructure.Persistence;

namespace GraphQL.Prototype.Tests.Data.Person
{
    public class SetupInMemoryDb
    {
        public static async Task SetupDb(PersonContext context)
        {
            var personTestData = PersonTestData.SeedPersonTestData();
            await context.AddRangeAsync(personTestData);

            await context.SaveChangesAsync();
        }

        public static async Task DeleteData(PersonContext context)
        {
            context.People.RemoveRange(context.People.ToList());

            await context.SaveChangesAsync();
        }
    }
}
