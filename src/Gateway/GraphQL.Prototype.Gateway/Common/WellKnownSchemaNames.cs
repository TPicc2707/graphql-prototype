namespace GraphQL.Prototype.Gateway.Common
{
    public static class WellKnownSchemaNames
    {
        public const string People = "person";
        public const string Addresses = "address";
    }

    public static class WellKnownSchemaUrls
    {
        // Use with Gateway locally
        //public const string PeopleUrl = "http://localhost:5011/graphql";
        //public const string AddressesUrl = "http://localhost:5012/graphql";

        // Use with Gateway within Docker container
        public const string PeopleUrl = "http://host.containers.internal:5011/graphql";
        public const string AddressesUrl = "http://host.containers.internal:5012/graphql";
    }
}
