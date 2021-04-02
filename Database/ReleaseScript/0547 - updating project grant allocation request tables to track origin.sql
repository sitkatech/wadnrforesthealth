
ALTER TABLE dbo.ProjectGrantAllocationRequest ADD CreateDate DateTime 

ALTER TABLE dbo.ProjectGrantAllocationRequest ADD UpdateDate DateTime

ALTER TABLE dbo.ProjectGrantAllocationRequest ADD ImportedFromTabularData bit


ALTER TABLE dbo.ProjectGrantAllocationRequestUpdate ADD CreateDate DateTime 

ALTER TABLE dbo.ProjectGrantAllocationRequestUpdate ADD UpdateDate DateTime

ALTER TABLE dbo.ProjectGrantAllocationRequestUpdate ADD ImportedFromTabularData bit

go

update dbo.ProjectGrantAllocationRequest set CreateDate = getDate()

update dbo.ProjectGrantAllocationRequest set ImportedFromTabularData = 0

update dbo.ProjectGrantAllocationRequestUpdate set CreateDate = getDate()

update dbo.ProjectGrantAllocationRequestUpdate set ImportedFromTabularData = 0

go

ALTER TABLE dbo.ProjectGrantAllocationRequest ALTER COLUMN CreateDate DateTime not null

ALTER TABLE dbo.ProjectGrantAllocationRequest ALTER COLUMN ImportedFromTabularData bit not null

ALTER TABLE dbo.ProjectGrantAllocationRequestUpdate ALTER COLUMN CreateDate DateTime not null

ALTER TABLE dbo.ProjectGrantAllocationRequestUpdate ALTER COLUMN ImportedFromTabularData bit not null

