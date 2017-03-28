namespace Stormpath.Configuration.Abstractions
{
    public class OktaConfiguration
    {
        public string ApiToken { get; set; }

        public string Org { get; set; }

        public OktaApplicationConfiguration Application { get; set; }
    }
}
