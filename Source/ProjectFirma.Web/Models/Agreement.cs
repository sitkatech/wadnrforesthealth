using LtInfo.Common.Models;

namespace ProjectFirma.Web.Models
{
    public partial class Agreement : IHavePrimaryKey
    {
        public int FakeAgreementID { get; set; }
        public int PrimaryKey { get { return FakeAgreementID; } set { FakeAgreementID = value; } }


       
    }


    //TODO delete when this is actually generated
    public class AgreementPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<Agreement>
    {
        public AgreementPrimaryKey(int primaryKeyValue) : base(primaryKeyValue) { }
        public AgreementPrimaryKey(Agreement agreement) : base(agreement) { }

        public static implicit operator AgreementPrimaryKey(int primaryKeyValue)
        {
            return new AgreementPrimaryKey(primaryKeyValue);
        }

        public static implicit operator AgreementPrimaryKey(Agreement agreement)
        {
            return new AgreementPrimaryKey(agreement);
        }
    }
}