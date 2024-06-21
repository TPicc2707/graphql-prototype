namespace GraphQL.EventBus.Messages.Events
{
    public class CreatePersonEvent
    {
        public Guid PersonId { get; set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }

    }
}
