namespace ProjectFirma.Web.Models
{
    public partial class PersonEnvironmentCredential
    {
        public PersonEnvironmentCredential(Person person, DeploymentEnvironment deploymentEnvironment, Authenticator authenticator, string personUniqueIdentifier): this(person, deploymentEnvironment, authenticator)
        {
            this.PersonUniqueIdentifier = personUniqueIdentifier;
        }
    }
}