namespace Stormpath.Configuration.Abstractions.Immutable
{
    public sealed class OktaApplicationConfiguration
    {
        public OktaApplicationConfiguration(string id = null)
        {
            Id = id;
        }

        internal OktaApplicationConfiguration()
        {
        }

        public OktaApplicationConfiguration DeepClone()
            => new OktaApplicationConfiguration(Id);

        public string Id { get; internal set; }
    }
}
