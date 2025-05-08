


drop table dbo.ClassificationPerformanceMeasure

drop table dbo.ProjectTypePerformanceMeasure

drop table dbo.PerformanceMeasureNote

drop table dbo.PerformanceMeasureActualSubcategoryOption

drop table dbo.PerformanceMeasureActualSubcategoryOptionUpdate

drop table dbo.PerformanceMeasureActualUpdate

drop table dbo.PerformanceMeasureExpectedSubcategoryOption

drop table dbo.PerformanceMeasureExpected

drop table dbo.PerformanceMeasureActual

drop table dbo.PerformanceMeasureSubcategoryOption

drop table dbo.PerformanceMeasureSubcategory

drop table dbo.PerformanceMeasureTargetValueType

drop table dbo.PerformanceMeasure

--drop table dbo.MeasurementUnitType

drop table dbo.PerformanceMeasureType

drop table dbo.PerformanceMeasureDataSourceType

delete from dbo.FirmaPage where FirmaPageTypeID in (9)

delete from dbo.FieldDefinitionData where FieldDefinitionID in (4,18,19,49,50,52,85,97,228,264)

alter table dbo.Project drop column PerformanceMeasureActualYearsExemptionExplanation;

alter table dbo.Project drop column PerformanceMeasureNotes;

alter table dbo.ProjectUpdateBatch drop column PerformanceMeasureDiffLog;

alter table dbo.ProjectUpdateBatch drop column PerformanceMeasuresComment;

alter table dbo.ProjectUpdateBatch drop column PerformanceMeasureActualYearsExemptionExplanation;