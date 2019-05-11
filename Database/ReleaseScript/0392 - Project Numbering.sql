--add column to table
alter table dbo.Project
	add FhtProjectNumber varchar(20);
go

--insert values into the new column
UPDATE dbo.Project
SET FhtProjectNumber = 'FHT-2019-' +  RIGHT('000'+ISNULL(cast(x.NewProjectNumber as varchar(10)),''),3)
FROM (
	  select
	  p.ProjectID,
      ROW_NUMBER() OVER (ORDER BY ProjectID) AS NewProjectNumber
      FROM dbo.Project AS p
      ) x
where x.ProjectID = dbo.Project.ProjectID


ALTER TABLE dbo.Project ALTER COLUMN FhtProjectNumber varchar(20) NOT NULL

ALTER TABLE dbo.Project ADD CONSTRAINT AK_Project_FhtProjectNumber UNIQUE (FhtProjectNumber);   
	
