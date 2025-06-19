


alter table dbo.ProjectCreateSection
add IsSectionRequired bit;
go

update dbo.ProjectCreateSection
set IsSectionRequired = 0


update dbo.ProjectCreateSection
set IsSectionRequired = 1
where ProjectCreateSectionID in (2,3)

alter table dbo.ProjectCreateSection
alter column IsSectionRequired bit not null;
