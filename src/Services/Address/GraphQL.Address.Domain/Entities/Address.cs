namespace GraphQL.Address.Domain.Entities
{
    public class Address
    {
        public Guid AddressId { get; set; }
        public Guid PersonId { get; set; }
        public string Type { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public virtual Person Person { get; set; }
    }
}
