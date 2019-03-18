--select * from dbo.ProjectCode
--select distinct ProjectCodeAbbrev from dbo.ProjectCode

--select * from dbo.GrantAllocationProjectCode
--where ProjectCodeID = 105

--Remove "SFA" project code because this is an abbreviation not an actual project code
--Is this OKAY!? should I do a like on the ProjectCodeAbbrev column instead!?
delete from dbo.GrantAllocationProjectCode
where ProjectCodeID = 105

delete from dbo.ProjectCode 
where ProjectCodeID = 105


--"GKB, C,D,E,G" 
--update 'C', 'D', 'E', 'G' codes to prepend 'GK'
UPDATE dbo.ProjectCode
SET ProjectCodeAbbrev = 'GKC'
where ProjectCodeAbbrev = 'C'

UPDATE dbo.ProjectCode
SET ProjectCodeAbbrev = 'GKD'
where ProjectCodeAbbrev = 'D'

UPDATE dbo.ProjectCode
SET ProjectCodeAbbrev = 'GKE'
where ProjectCodeAbbrev = 'E'

UPDATE dbo.ProjectCode
SET ProjectCodeAbbrev = 'GKG'
where ProjectCodeAbbrev = 'G'


--"GVL,M,N,O,P"
--update 'M', 'N', 'O', 'P' codes to prepend 'GV'
UPDATE dbo.ProjectCode
SET ProjectCodeAbbrev = 'GVM'
where ProjectCodeAbbrev = 'M'

UPDATE dbo.ProjectCode
SET ProjectCodeAbbrev = 'GVN'
where ProjectCodeAbbrev = 'N'

UPDATE dbo.ProjectCode
SET ProjectCodeAbbrev = 'GVO'
where ProjectCodeAbbrev = 'O'

UPDATE dbo.ProjectCode
SET ProjectCodeAbbrev = 'GVP'
where ProjectCodeAbbrev = 'P'


--select * from dbo.ProjectCode where ProjectCodeAbbrev in ('RGP','RGR','RGS','RGU')
--select * from dbo.ProjectCode where ProjectCodeAbbrev LIKE 'FEPP%'

--update 'FEPP...' record to use 4 separate project codes and like to grant allocation

--first add new project codes from record
INSERT INTO dbo.ProjectCode
(ProjectCodeAbbrev)
VALUES
('RGP'),
('RGR'),
('RGS'),
('RGU')

--insert links to new project codes and grant allocation tied to FEPP record
--Project codes to link 107,108,108,110
--GrantAllocationID to link 197
insert into dbo.GrantAllocationProjectCode
(GrantAllocationID, ProjectCodeID) VALUES

(197, (select ProjectCodeID from dbo.ProjectCode where ProjectCodeAbbrev = 'RGP')),
(197, (select ProjectCodeID from dbo.ProjectCode where ProjectCodeAbbrev = 'RGR')),
(197, (select ProjectCodeID from dbo.ProjectCode where ProjectCodeAbbrev = 'RGS')),
(197, (select ProjectCodeID from dbo.ProjectCode where ProjectCodeAbbrev = 'RGU'))


--remove bad project code
delete from dbo.GrantAllocationProjectCode
where ProjectCodeID = 4 and GrantAllocationProjectCodeID = 95

delete from dbo.ProjectCode
where ProjectCodeAbbrev LIKE 'FEPP%'

