/*
create lookup for PriorityLandscapeType(Name, DisplayName)
*/
create table PriorityLandscapeType(PriorityLandscapeTypeID int not null primary key, PriorityLandscapeTypeName varchar(100) not null, PriorityLandscapeTypeDisplayName varchar(100) not null)

delete from dbo.PriorityLandscapeType;
insert dbo.PriorityLandscapeType (PriorityLandscapeTypeID, PriorityLandscapeTypeName, PriorityLandscapeTypeDisplayName) 
values 
(1,'East', '20-Year Forest Health Strategic Plan: Eastern Washington Priority Landscape'),
(2, 'West', 'West');

/*
add column to PriorityLandscape FK to Type
*/
ALTER TABLE dbo.PriorityLandscape
Add PriorityLandscapeTypeID int constraint
FK_PriorityLandscape_PriorityLandscapeType_PriorityLandscapeTypeID foreign key (PriorityLandscapeTypeID) references dbo.PriorityLandscapeType(PriorityLandscapeTypeID);
go
/*
set new FK to eastern PL for existing records
*/
UPDATE dbo.PriorityLandscape
SET    PriorityLandscapeTypeID = 1;

/*
import new western PLs
*/
--select * from dbo.PriorityLandscape

Declare @TempID int
set @TempID = 7561
INSERT INTO dbo.PriorityLandscape (PriorityLandscapeID, PriorityLandscapeName, PriorityLandscapeLocation, PriorityLandscapeTypeID)  
SELECT @TempID+ROW_NUMBER() over (order by PilotName), PilotName, GEOM, 2
  FROM dbo.TmpWesternPriorityLandscapes

--select * from PriorityLandscape
drop table dbo.TmpWesternPriorityLandscapes