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


if object_id('tempdb.dbo.#tempTreatments') is not null drop table #tempTreatments

CREATE TABLE #tempTreatments(TemporaryTreatmentCacheID [int] IDENTITY(1,1) NOT NULL,
	[ProjectID] [int] NOT NULL,
    [TreatmentAreaFeature] [geometry] NOT NULL,
	[GrantAllocationAwardLandownerCostShareLineItemID] [int] NULL,
	[TreatmentStartDate] [datetime] NULL,
	[TreatmentEndDate] [datetime] NULL,
	[TreatmentFootprintAcres] [decimal](38, 10) NOT NULL,
	[TreatmentNotes] [varchar](2000) NULL,
	[TreatmentTypeID] [int] NOT NULL,
	[TreatmentAreaID] [int] NULL)


INSERT INTO #tempTreatments
           ([ProjectID]
           ,[TreatmentAreaFeature]
           ,[GrantAllocationAwardLandownerCostShareLineItemID]
           ,[TreatmentStartDate]
           ,[TreatmentEndDate]
           ,[TreatmentFootprintAcres]
           ,[TreatmentNotes]
           ,[TreatmentTypeID])



select p.ProjectID
, x.GisFeatureGeometry.MakeValid()
, null
, null
, null
, isnull(TRY_PARSE(x.OtherTreatmentAcres AS decimal(38,10)),0)  as [TreatmentOtherTreatmentAcres]
,null
, 13 -- other

 from dbo.Project p
join (
select gf.GisFeatureGeometry, gfma.GisFeatureMetadataAttributeValue, gfmaOther.GisFeatureMetadataAttributeValue as OtherTreatmentAcres from dbo.GisFeature gf
join dbo.GisFeatureMetadataAttribute gfma on gfma.GisFeatureID = gf.GisFeatureID
left join dbo.GisFeatureMetadataAttribute gfmaOther on gfmaOther.GisFeatureID = gf.GisFeatureID and gfmaOther.GisMetadataAttributeID = @otherTreatmentAcresMetadataAttributeID
where gfma.GisMetadataAttributeID = @projectIdentifierGisMetadataAttributeID 
and gf.GisUploadAttemptID = @piGisUploadAttemptID) x on x.GisFeatureMetadataAttributeValue = p.ProjectGisIdentifier
where p.CreateGisUploadAttemptID = @piGisUploadAttemptID
  






insert into dbo.TreatmentArea(TreatmentAreaFeature, TemporaryTreatmentCacheID)

select x.TreatmentAreaFeature, x.TemporaryTreatmentCacheID from #tempTreatments x


insert into dbo.Treatment ([ProjectID]
           ,[GrantAllocationAwardLandownerCostShareLineItemID]
           ,[TreatmentStartDate]
           ,[TreatmentEndDate]
           ,[TreatmentFootprintAcres]
           ,[TreatmentNotes]
           , TreatmentAreaID
           , TreatmentTypeID)

select 

            [ProjectID]
           ,[GrantAllocationAwardLandownerCostShareLineItemID]
           ,[TreatmentStartDate]
           ,[TreatmentEndDate]
           ,[TreatmentFootprintAcres]
           ,[TreatmentNotes]
           , ta.TreatmentAreaID
           , 13 -- other

from #tempTreatments x
join dbo.TreatmentArea ta on ta.TemporaryTreatmentCacheID = x.TemporaryTreatmentCacheID




/*

exec dbo.procImportTreatmentsFromGisUploadAttempt @piGisUploadAttemptID = 1, @projectIdentifierGisMetadataAttributeID = 39, 13

*/