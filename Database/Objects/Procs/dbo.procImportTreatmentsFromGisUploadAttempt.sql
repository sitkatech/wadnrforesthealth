IF EXISTS(SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.procImportTreatmentsFromGisUploadAttempt'))
    drop procedure dbo.procImportTreatmentsFromGisUploadAttempt
go
create procedure dbo.procImportTreatmentsFromGisUploadAttempt
(
    @piGisUploadAttemptID int,
    @projectIdentifierGisMetadataAttributeID int,
    @otherTreatmentAcresMetadataAttributeID int
)
as


INSERT INTO [dbo].[Treatment]
           ([ProjectID]
           ,[TreatmentFeature]
           ,[GrantAllocationAwardLandownerCostShareLineItemID]
           ,[TreatmentStartDate]
           ,[TreatmentEndDate]
           ,[TreatmentFootprintAcres]
           ,[TreatmentChippingAcres]
           ,[TreatmentPruningAcres]
           ,[TreatmentThinningAcres]
           ,[TreatmentMasticationAcres]
           ,[TreatmentGrazingAcres]
           ,[TreatmentLopAndScatterAcres]
           ,[TreatmentBiomassRemovalAcres]
           ,[TreatmentHandPileAcres]
           ,[TreatmentBroadcastBurnAcres]
           ,[TreatmentHandPileBurnAcres]
           ,[TreatmentMachinePileBurnAcres]
           ,[TreatmentOtherTreatmentAcres]
           ,[TreatmentSlashAcres]
           ,[TreatmentNotes])



select p.ProjectID
, x.GisFeatureGeometry.MakeValid()
, null
, null
, null
, 0
,0
,0
,0
,0
,0
,0
,0
,0
,0
,0
,0
,isnull(TRY_PARSE(x.OtherTreatmentAcres AS decimal(38,10)),0)  as [TreatmentOtherTreatmentAcres]
,0
,null

 from dbo.Project p
join (
select gf.GisFeatureGeometry, gfma.GisFeatureMetadataAttributeValue, gfmaOther.GisFeatureMetadataAttributeValue as OtherTreatmentAcres from dbo.GisFeature gf
join dbo.GisFeatureMetadataAttribute gfma on gfma.GisFeatureID = gf.GisFeatureID
left join dbo.GisFeatureMetadataAttribute gfmaOther on gfmaOther.GisFeatureID = gf.GisFeatureID and gfmaOther.GisMetadataAttributeID = @otherTreatmentAcresMetadataAttributeID
where gfma.GisMetadataAttributeID = @projectIdentifierGisMetadataAttributeID 
and gf.GisUploadAttemptID = @piGisUploadAttemptID) x on x.GisFeatureMetadataAttributeValue = p.ProjectGisIdentifier
where p.CreateGisUploadAttemptID = @piGisUploadAttemptID
  


/*

exec dbo.procImportTreatmentsFromGisUploadAttempt @piGisUploadAttemptID = 1, @projectIdentifierGisMetadataAttributeID = 39, 13

*/