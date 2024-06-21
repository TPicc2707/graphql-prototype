namespace GraphQL.Prototype.Tests.Data.Address
{
    public static class AddressTestData
    {
        public static List<GraphQL.Address.Domain.Entities.Address> SeedPersonAddressTestData()
        {
            var addresses = new List<GraphQL.Address.Domain.Entities.Address>();

            addresses.Add(new GraphQL.Address.Domain.Entities.Address()
            {
                AddressId = new Guid("50ca7492-6771-4e79-b7d8-59f996344e2e"),
                PersonId = new Guid("bb531538-3bc3-4e88-bbfc-dac101abad2a"),
                Street = "123 Main Street",
                City = "Louisville",
                State = "KY",
                Type = "Home",
                ZipCode = "12345"
            });
            addresses.Add(new GraphQL.Address.Domain.Entities.Address()
            {
                AddressId = new Guid("5e71e217-7e4a-4b7e-a72d-df26d4f6d301"),
                PersonId = new Guid("bb531538-3bc3-4e88-bbfc-dac101abad2a"),
                Street = "1244 Red Station",
                City = "Louisville",
                State = "KY",
                Type = "Work",
                ZipCode = "12346"
            });
            addresses.Add(new GraphQL.Address.Domain.Entities.Address()
            {
                AddressId = new Guid("37c5496d-2b52-41f0-a827-477aa26b8e74"),
                PersonId = new Guid("1a4e32e0-479d-4110-ae8d-292cd63c8bdf"),
                Street = "7883 Secondary Drive",
                City = "Pittsburgh",
                State = "PA",
                Type = "Home",
                ZipCode = "03923"
            });
            addresses.Add(new GraphQL.Address.Domain.Entities.Address()
            {
                AddressId = new Guid("e4882f3f-d139-4b17-9124-9183a672ecc5"),
                PersonId = new Guid("f60e17e3-c781-466d-9e6a-511fb3b3d2a3"),
                Street = "3423 Carter Street",
                City = "Boston",
                State = "MA",
                Type = "Home",
                ZipCode = "93232"
            });
            addresses.Add(new GraphQL.Address.Domain.Entities.Address()
            {
                AddressId = new Guid("18a1c983-391f-4309-b06f-7b55cbe24e8c"),
                PersonId = new Guid("f60e17e3-c781-466d-9e6a-511fb3b3d2a3"),
                Street = "2312 Light Boulevard",
                City = "Boston",
                State = "MA",
                Type = "Work",
                ZipCode = "93236"
            });
            addresses.Add(new GraphQL.Address.Domain.Entities.Address()
            {
                AddressId = new Guid("07c8d8f8-03b0-4261-963f-010d9e9c0a7d"),
                PersonId = new Guid("5311f4ad-3a9c-485f-bdab-dc6ed066c960"),
                Street = "3423 Carter Street",
                City = "Albany",
                State = "NY",
                Type = "Home",
                ZipCode = "83212"
            });

            return addresses;
        }
    }
}
