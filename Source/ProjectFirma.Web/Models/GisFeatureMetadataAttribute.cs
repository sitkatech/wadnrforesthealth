namespace ProjectFirma.Web.Models
{
    public partial class GisFeatureMetadataAttribute : IAuditableEntity
    {
        public string AuditDescriptionString => $"GisFeatureID: {GisFeatureID}; GisMetadataAttributeID: {GisMetadataAttributeID}";

        public string MakeSqlInsertStatement()
        {

            if (string.IsNullOrEmpty(GisFeatureMetadataAttributeValue))
            {
                return $"exec sp_executesql N\'INSERT [dbo].[GisFeatureMetadataAttribute]([GisFeatureID], [GisMetadataAttributeID], [GisFeatureMetadataAttributeValue]) " +
                       $"VALUES (@0, @1, null)\', N\'@0 int,@1 int\',@0={GisFeatureID},@1={GisMetadataAttributeID}";
            }

            var value = GisFeatureMetadataAttributeValue.Replace("'", "''");
            return $"exec sp_executesql N\'INSERT [dbo].[GisFeatureMetadataAttribute]([GisFeatureID], [GisMetadataAttributeID], [GisFeatureMetadataAttributeValue]) " +
                $"VALUES (@0, @1, @2)\', N\'@0 int,@1 int,@2 varchar(max) \',@0={GisFeatureID},@1={GisMetadataAttributeID},@2=\'{value}\'";
          
        }

    }
}