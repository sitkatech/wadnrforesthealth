
IF EXISTS(SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.fGetBoundingBoxForProjectIdList'))
    drop function dbo.fGetBoundingBoxForProjectIdList
go

create function dbo.fGetBoundingBoxForProjectIdList(@ProjectIDList dbo.IDList readonly)
returns varchar(max)
begin
	declare @myBoundingBox varchar(max)
	

	SELECT @myBoundingBox = geometry::EnvelopeAggregate(ProjectLocationPoint).ToString() 
	FROM dbo.Project
	where ProjectID in (select ID from @ProjectIDList);

	return @myBoundingBox
end

go



/*


declare @projectIDList dbo.IDList 
insert into @projectIDList(id) select top 100 ProjectID from dbo.Project
select * from dbo.fGetBoundingBoxForProjectIdList(@projectIDList) 




	declare @myBoundingBox varchar(max)
	declare @projectIDList dbo.IDList 
	insert into @projectIDList(id) select top 100 ProjectID from dbo.Project

	SELECT geometry::EnvelopeAggregate(ProjectLocationPoint).ToString() 
	FROM dbo.Project
	where ProjectID in (select ID from @projectIDList);

	return @myBoundingBox


*/


