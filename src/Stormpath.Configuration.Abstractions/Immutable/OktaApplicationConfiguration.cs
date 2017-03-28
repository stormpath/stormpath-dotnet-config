namespace Stormpath.Configuration.Abstractions.Immutable
{
    public sealed class OktaApplicationConfiguration
    {
        public OktaApplicationConfiguration(string id = null)
        {
            Id = id ?? Default.Configuration.Okta.Application.Id;
        }

        public OktaApplicationConfiguration(OktaApplicationConfiguration existing)
            : this(existing?.Id)
        {
        }

        internal OktaApplicationConfiguration()
        {
        }

        public string Id { get; internal set; }
    }
}
