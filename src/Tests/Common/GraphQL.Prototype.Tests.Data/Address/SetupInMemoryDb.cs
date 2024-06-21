using GraphQL.Address.Infrastructure.Persistence;

namespace GraphQL.Prototype.Tests.Data.Address
{
    public class SetupInMemoryDb
    {
        public static async Task SetupDb(PersonContext context)
        {
            var personTestData = PersonTestData.SeedPersonTestData();
            var addressTestData = AddressTestData.SeedPersonAddressTestData();
            await context.AddRangeAsync(personTestData);
            await context.AddRangeAsync(addressTestData);

            await context.SaveChangesAsync();
        }

        public static async Task DeleteData(PersonContext context)
        {
            context.Addresses.RemoveRange(context.Addresses.ToList());
            context.People.RemoveRange(context.People.ToList());

            await context.SaveChangesAsync();
        }
    }
}
