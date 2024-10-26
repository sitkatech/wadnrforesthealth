
IF EXISTS(SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.fGetBoundingBoxForProjectIdList'))
    drop function dbo.fGetBoundingBoxForProjectIdList
go

create function dbo.fGetBoundingBoxForProjectIdList(@ProjectIDList dbo.IDList readonly)
returns @rtnTable TABLE 
(
    -- columns returned by the function
    SW geometry NOT NULL,
    NE geometry NOT NULL
)
begin
	declare @myBoundingBox varchar(1000);

	SELECT @myBoundingBox = geometry::EnvelopeAggregate(ProjectLocationPoint).ToString()
	FROM dbo.Project
	where ProjectID in (select ID from @ProjectIDList);

	declare @g geometry = geometry::STGeomFromText(@myBoundingBox, 0);
	insert into @rtnTable
	select @g.STPointN(1) as [SW], @g.STPointN(3) as [NE];

	return
end

go



/*

declare @g geometry = geometry::STGeomFromText('POLYGON ((-179.231086 51.175092, 179.859681 51.175092, 179.859681 71.441059, -179.231086 71.441059, -179.231086 51.175092))', 0);
select @g.STPointN(1) as [SW], @g.STPointN(3) as [NE]


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


