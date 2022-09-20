alter TABLE [dbo].[County] add countyIDNew [int] identity(1, 1) NOT NULL
go
alter TABLE [dbo].[County] drop constraint PK_County_CountyID
go
alter TABLE [dbo].[County] drop column countyID
go
Exec sp_rename 'County.countyIDNew', 'CountyID', 'Column'
go

ALTER TABLE [dbo].[County]  WITH CHECK ADD  CONSTRAINT [PK_County_CountyID] PRIMARY KEY CLUSTERED ([CountyID])

insert into County(StateProvinceID, CountyName, CountyFeature)
select 47, ForesterWorkUnitName, ForesterworkUnitLocation
from dbo.vGeoServerFindYourForester where ForesterRoleID = 6

go

CREATE TABLE [dbo].[ProjectCounty](
	[ProjectCountyID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectID] [int] NOT NULL,
	[CountyID] [int] NOT NULL,
	CONSTRAINT [PK_ProjectCounty_ProjectCountyID] PRIMARY KEY CLUSTERED ([ProjectCountyID] ASC),
	CONSTRAINT [AK_ProjectCounty_ProjectID_CountyID] UNIQUE NONCLUSTERED ([ProjectID] ASC, [CountyID] ASC)
) 
GO

ALTER TABLE [dbo].[ProjectCounty]  WITH CHECK ADD  CONSTRAINT [FK_ProjectCounty_County_CountyID] FOREIGN KEY([CountyID])
REFERENCES [dbo].[County] ([CountyID])
GO

ALTER TABLE [dbo].[ProjectCounty] CHECK CONSTRAINT [FK_ProjectCounty_County_CountyID]
GO

ALTER TABLE [dbo].[ProjectCounty]  WITH CHECK ADD  CONSTRAINT [FK_ProjectCounty_Project_ProjectID] FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Project] ([ProjectID])
GO

ALTER TABLE [dbo].[ProjectCounty] CHECK CONSTRAINT [FK_ProjectCounty_Project_ProjectID]
GO

insert into [dbo].[FirmaPageType] values (70, 'County', 'Counties', 1)

go

insert into [dbo].[FirmaPage] values(70,'<p>Click on a county to see a map and list of forest health projects within the area.</p>')

go
