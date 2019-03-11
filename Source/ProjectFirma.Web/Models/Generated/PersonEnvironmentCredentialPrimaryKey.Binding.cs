//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.PersonEnvironmentCredential
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class PersonEnvironmentCredentialPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<PersonEnvironmentCredential>
    {
        public PersonEnvironmentCredentialPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public PersonEnvironmentCredentialPrimaryKey(PersonEnvironmentCredential personEnvironmentCredential) : base(personEnvironmentCredential){}

        public static implicit operator PersonEnvironmentCredentialPrimaryKey(int primaryKeyValue)
        {
            return new PersonEnvironmentCredentialPrimaryKey(primaryKeyValue);
        }

        public static implicit operator PersonEnvironmentCredentialPrimaryKey(PersonEnvironmentCredential personEnvironmentCredential)
        {
            return new PersonEnvironmentCredentialPrimaryKey(personEnvironmentCredential);
        }
    }
}