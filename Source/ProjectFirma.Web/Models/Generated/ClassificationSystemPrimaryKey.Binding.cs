//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.ClassificationSystem
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class ClassificationSystemPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<ClassificationSystem>
    {
        public ClassificationSystemPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public ClassificationSystemPrimaryKey(ClassificationSystem classificationSystem) : base(classificationSystem){}

        public static implicit operator ClassificationSystemPrimaryKey(int primaryKeyValue)
        {
            return new ClassificationSystemPrimaryKey(primaryKeyValue);
        }

        public static implicit operator ClassificationSystemPrimaryKey(ClassificationSystem classificationSystem)
        {
            return new ClassificationSystemPrimaryKey(classificationSystem);
        }
    }
}