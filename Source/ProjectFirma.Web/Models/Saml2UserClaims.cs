using Newtonsoft.Json;

namespace ProjectFirma.Web.Models
{
    public class Saml2UserClaims
    {
        // core
        [JsonProperty("uniqueIdentifiier")]
        public string UniqueIdentifier { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        public string Username { get; set; }
    }
}