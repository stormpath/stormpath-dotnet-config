namespace Stormpath.Configuration.Abstractions.Immutable
{
    public sealed class OktaConfiguration
    {
        public OktaConfiguration(
            string apiToken = null,
            string org = null,
            OktaApplicationConfiguration application = null)
        {
            ApiToken = apiToken ?? Default.Configuration.Okta.ApiToken;
            Org = org ?? Default.Configuration.Okta.Org;
            Application = application ?? Default.Configuration.Okta.Application;
        }

        public OktaConfiguration(OktaConfiguration existing)
            : this(apiToken: existing?.ApiToken, org: existing?.Org, application: existing?.Application)
        {
        }

        internal OktaConfiguration()
        {
        }

        public string ApiToken { get; internal set; }

        public string Org { get; internal set; }

        public OktaApplicationConfiguration Application { get; internal set; }
    }
}
