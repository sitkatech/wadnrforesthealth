/*
select 
		tmp.UNIT_ID,
		tmp.OBJECTID,
		tmp.GlobalID,
		tmp.FHT_Project_Number,
		tmp.TRT_LOCAL_ID,
		tmp.PROJECT_NM,
		tmp.GEOM,
		p.FhtProjectNumber,
		p.ProjectID,
		p.ProjectName
from 
		dbo.tmpTreatmentsFromArcGis as tmp 
join	dbo.Project as p on p.FhtProjectNumber = tmp.FHT_Project_Number
*/


alter table dbo.ProjectLocation
add ArcGisObjectID int null,  ArcGisGlobalID varchar(50) null;
go


insert into dbo.ProjectLocation (ProjectID, ProjectLocationGeometry, ProjectLocationTypeID, ProjectLocationName, ArcGisObjectID, ArcGisGlobalID)
select 
		p.ProjectID,
		tmp.GEOM as ProjectLocationGeometry,
		2 as ProjectLocationTypeID, --Force typeID to TreatmentActivity because all of these are treatment activity areas.
		LTRIM(RTRIM(ISNULL(tmp.TRT_LOCAL_ID, '') + ' - ' + ISNULL(tmp.PROJECT_NM, '') + ' ' + cast(ROW_NUMBER() OVER(partition BY p.ProjectID order by p.ProjectID ASC) as varchar))) as ProjectLocationName,
		tmp.OBJECTID as ArcGisObjectID,
		tmp.GlobalID as ArcGisGlobalID
from 
		dbo.tmpTreatmentsFromArcGis as tmp 
join	dbo.Project as p on p.FhtProjectNumber = tmp.FHT_Project_Number