namespace GraphQL.Address.Domain.Entities
{
    public class Person
    {
        public Guid PersonId { get; set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
    }
}
