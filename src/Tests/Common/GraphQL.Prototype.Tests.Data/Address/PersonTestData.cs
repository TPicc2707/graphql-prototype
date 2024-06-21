namespace GraphQL.Prototype.Tests.Data.Address
{
    public class PersonTestData
    {
        public static List<GraphQL.Address.Domain.Entities.Person> SeedPersonTestData()
        {
            var people = new List<GraphQL.Address.Domain.Entities.Person>();

            people.Add(new GraphQL.Address.Domain.Entities.Person()
            {
                PersonId = new Guid("bb531538-3bc3-4e88-bbfc-dac101abad2a"),
                FirstName = "Anthony",
                MiddleInitial = "N",
                LastName = "Piccirilli",
                Title = "Mr",
            });

            people.Add(new GraphQL.Address.Domain.Entities.Person()
            {
                PersonId = new Guid("1a4e32e0-479d-4110-ae8d-292cd63c8bdf"),
                FirstName = "Jake",
                MiddleInitial = "R",
                LastName = "Smith",
                Title = "Dr",
            });

            people.Add(new GraphQL.Address.Domain.Entities.Person()
            {
                PersonId = new Guid("f60e17e3-c781-466d-9e6a-511fb3b3d2a3"),
                FirstName = "Patty",
                MiddleInitial = "L",
                LastName = "Jackson",
                Title = "Mrs",
            });

            people.Add(new GraphQL.Address.Domain.Entities.Person()
            {
                PersonId = new Guid("5311f4ad-3a9c-485f-bdab-dc6ed066c960"),
                FirstName = "Jerry",
                MiddleInitial = "B",
                LastName = "Johnson",
                Title = "Mrs",
            });

            return people;
        }

    }
}
