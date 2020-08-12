IF EXISTS(SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.procImportTreatmentsFromGisUploadAttempt'))
    drop procedure dbo.procImportTreatmentsFromGisUploadAttempt
go
create procedure dbo.procImportTreatmentsFromGisUploadAttempt
(
    @piGisUploadAttemptID int,
    @projectIdentifierGisMetadataAttributeID int,
    @treatedAcresMetadataAttributeID int,
    @treatmentTypeMetadataAttributeID int,
    @treatmentDetailedActivityTypeMetadataAttributeID int
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
    [TreatmentDetailedActivityTypeID] [int] not null,
	[TreatmentAreaID] [int] NULL,
    TreatmentTypeImportedText varchar(200),
    [TreatmentDetailedActivityTypeImportedText] varchar(200))



INSERT INTO #tempTreatments
           ([ProjectID]
           ,[TreatmentAreaFeature]
           ,[GrantAllocationAwardLandownerCostShareLineItemID]
           ,[TreatmentStartDate]
           ,[TreatmentEndDate]
           ,[TreatmentFootprintAcres]
           ,[TreatmentNotes]
           ,[TreatmentTypeID]
           ,[TreatmentDetailedActivityTypeID]
           ,[TreatmentTypeImportedText]
           ,[TreatmentDetailedActivityTypeImportedText])



select p.ProjectID
, x.GisFeatureGeometry.MakeValid()
, null
, null
, null
, isnull(TRY_PARSE(x.OtherTreatmentAcres AS decimal(38,10)),0)  as [TreatmentOtherTreatmentAcres]
, null
, 4
, 13 -- other
, x.TreatmentTypeImportedText
, x.TreatmentDetailedActivityTypeImportedText

 from dbo.Project p
join (
        select gf.GisFeatureGeometry
        , gfma.GisFeatureMetadataAttributeValue
        , gfmaOther.GisFeatureMetadataAttributeValue as OtherTreatmentAcres 
        , gfmaTreatmentType.GisFeatureMetadataAttributeValue as TreatmentTypeImportedText
        , gfmaTreatmentDetailedActivityType.GisFeatureMetadataAttributeValue as TreatmentDetailedActivityTypeImportedText
        from dbo.GisFeature gf
        join dbo.GisFeatureMetadataAttribute gfma on gfma.GisFeatureID = gf.GisFeatureID
        left join dbo.GisFeatureMetadataAttribute gfmaOther on gfmaOther.GisFeatureID = gf.GisFeatureID and gfmaOther.GisMetadataAttributeID = @treatedAcresMetadataAttributeID
        left join dbo.GisFeatureMetadataAttribute gfmaTreatmentType on gfmaTreatmentType.GisFeatureID = gf.GisFeatureID and gfmaTreatmentType.GisMetadataAttributeID = @treatmentTypeMetadataAttributeID
        left join dbo.GisFeatureMetadataAttribute gfmaTreatmentDetailedActivityType on gfmaTreatmentDetailedActivityType.GisFeatureID = gf.GisFeatureID and gfmaTreatmentDetailedActivityType.GisMetadataAttributeID = @treatmentDetailedActivityTypeMetadataAttributeID
        where gfma.GisMetadataAttributeID = @projectIdentifierGisMetadataAttributeID 
        and gf.GisUploadAttemptID = @piGisUploadAttemptID)
   x on x.GisFeatureMetadataAttributeValue = p.ProjectGisIdentifier
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
           , TreatmentTypeID
           , TreatmentDetailedActivityTypeID
           , TreatmentTypeImportedText
           , TreatmentDetailedActivityTypeImportedText
           , CreateGisUploadAttemptID
           , TreatmentTreatedAcres)

select 

             x.ProjectID
           , x.GrantAllocationAwardLandownerCostShareLineItemID
           , x.TreatmentStartDate
           , x.TreatmentEndDate
           , x.TreatmentFootprintAcres
           , x.TreatmentNotes
           , ta.TreatmentAreaID
           , x.TreatmentTypeID
           , x.TreatmentDetailedActivityTypeID
           , x.TreatmentTypeImportedText
           , x.TreatmentDetailedActivityTypeImportedText
           , @piGisUploadAttemptID
           , x.TreatmentFootprintAcres

from #tempTreatments x
join dbo.TreatmentArea ta on ta.TemporaryTreatmentCacheID = x.TemporaryTreatmentCacheID



update dbo.TreatmentArea
set TemporaryTreatmentCacheID = null



update dbo.Treatment
set TreatmentTypeID = tt.TreatmentTypeID
from dbo.Treatment t
join dbo.GisUploadAttempt gua on t.CreateGisUploadAttemptID = gua.GisUploadAttemptID
join dbo.GisUploadSourceOrganization guso on guso.GisUploadSourceOrganizationID = gua.GisUploadSourceOrganizationID
join dbo.GisCrossWalkDefault gcwd on gcwd.GisUploadSourceOrganizationID = guso.GisUploadSourceOrganizationID and gcwd.GisCrossWalkSourceValue = t.TreatmentTypeImportedText
join dbo.TreatmentType tt on tt.TreatmentTypeDisplayName = gcwd.GisCrossWalkMappedValue
where gcwd.FieldDefinitionID = 468



update dbo.Treatment
set TreatmentDetailedActivityTypeID = tt.TreatmentDetailedActivityTypeID
from dbo.Treatment t
join dbo.GisUploadAttempt gua on t.CreateGisUploadAttemptID = gua.GisUploadAttemptID
join dbo.GisUploadSourceOrganization guso on guso.GisUploadSourceOrganizationID = gua.GisUploadSourceOrganizationID
join dbo.GisCrossWalkDefault gcwd on gcwd.GisUploadSourceOrganizationID = guso.GisUploadSourceOrganizationID and gcwd.GisCrossWalkSourceValue = t.TreatmentDetailedActivityTypeImportedText
join dbo.TreatmentDetailedActivityType tt on tt.TreatmentDetailedActivityTypeDisplayName = gcwd.GisCrossWalkMappedValue
where gcwd.FieldDefinitionID = 469

/*

exec dbo.procImportTreatmentsFromGisUploadAttempt @piGisUploadAttemptID = 1, @projectIdentifierGisMetadataAttributeID = 39, 13

*/