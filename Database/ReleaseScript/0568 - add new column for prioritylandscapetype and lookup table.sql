--create lookup for PriorityLandscapeCategory(Name, DisplayName)
create table PriorityLandscapeCategory(
PriorityLandscapeCategoryID int not null constraint PK_PriorityLandscapeCategory_PriorityLandscapeCategoryID primary key,
PriorityLandscapeCategoryName varchar(100) not null,
PriorityLandscapeCategoryDisplayName varchar(100) not null,
PriorityLandscapeCategoryMapLayerColor varchar(20) not null
)

insert dbo.PriorityLandscapeCategory (PriorityLandscapeCategoryID, PriorityLandscapeCategoryName, PriorityLandscapeCategoryDisplayName, PriorityLandscapeCategoryMapLayerColor) 
values 
(1,'East', '20-Year Forest Health Strategic Plan: Eastern Washington Priority Landscape', '#59ACFF'),
(2, 'West', 'Western WA Forest Health Priority Landscapes', '#0267e3');

--add column to PriorityLandscape FK to Category
ALTER TABLE dbo.PriorityLandscape
Add PriorityLandscapeCategoryID int constraint
FK_PriorityLandscape_PriorityLandscapeCategory_PriorityLandscapeCategoryID foreign key (PriorityLandscapeCategoryID) references dbo.PriorityLandscapeCategory(PriorityLandscapeCategoryID);
go

--set new FK to eastern PL for existing records
UPDATE dbo.PriorityLandscape
SET    PriorityLandscapeCategoryID = 1;

--import new western PLs
Declare @TempID int
set @TempID = 7561
INSERT INTO dbo.PriorityLandscape (PriorityLandscapeID, PriorityLandscapeName, PriorityLandscapeLocation, PriorityLandscapeCategoryID)  
SELECT @TempID+ROW_NUMBER() over (order by PilotName), PilotName, GEOM, 2
  FROM dbo.TmpWesternPriorityLandscapes

drop table dbo.TmpWesternPriorityLandscapes

--seed new western PLs with default above map text
update dbo.PriorityLandscape set PriorityLandscapeAboveMapText = 'This map displays the simple location of forest health projects in this priority landscape along with optional additional layers that users can select to view including detailed project and treatment locations.'
where PriorityLandscapeAboveMapText is null 
