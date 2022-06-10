--create lookup for PriorityLandscapeType(Name, DisplayName)
create table PriorityLandscapeType(
PriorityLandscapeTypeID int not null constraint PK_PriorityLandscapeType_PriorityLandscapeTypeID primary key,
PriorityLandscapeTypeName varchar(100) not null,
PriorityLandscapeTypeDisplayName varchar(100) not null,
PriorityLandscapeTypeMapLayerColor varchar(20) not null
)

insert dbo.PriorityLandscapeType (PriorityLandscapeTypeID, PriorityLandscapeTypeName, PriorityLandscapeTypeDisplayName, PriorityLandscapeTypeMapLayerColor) 
values 
(1,'East', '20-Year Forest Health Strategic Plan: Eastern Washington Priority Landscape', '#59ACFF'),
(2, 'West', 'West', '#0267e3');

--add column to PriorityLandscape FK to Type
ALTER TABLE dbo.PriorityLandscape
Add PriorityLandscapeTypeID int constraint
FK_PriorityLandscape_PriorityLandscapeType_PriorityLandscapeTypeID foreign key (PriorityLandscapeTypeID) references dbo.PriorityLandscapeType(PriorityLandscapeTypeID);
go

--set new FK to eastern PL for existing records
UPDATE dbo.PriorityLandscape
SET    PriorityLandscapeTypeID = 1;

--import new western PLs
Declare @TempID int
set @TempID = 7561
INSERT INTO dbo.PriorityLandscape (PriorityLandscapeID, PriorityLandscapeName, PriorityLandscapeLocation, PriorityLandscapeTypeID)  
SELECT @TempID+ROW_NUMBER() over (order by PilotName), PilotName, GEOM, 2
  FROM dbo.TmpWesternPriorityLandscapes

drop table dbo.TmpWesternPriorityLandscapes

--seed new western PLs with default above map text
update dbo.PriorityLandscape set PriorityLandscapeAboveMapText = 'This map displays the simple location of forest health projects in this priority landscape along with optional additional layers that users can select to view including detailed project and treatment locations.'
where PriorityLandscapeAboveMapText is null 
