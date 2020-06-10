
CREATE TABLE [dbo].[CustomPageNavigationSection](
	[CustomPageNavigationSectionID] [int] NOT NULL,
	[CustomPageNavigationSectionName] [varchar](100) NOT NULL
 CONSTRAINT [PK_CustomPageNavigationSection_CustomPageNavigationSectionID] PRIMARY KEY CLUSTERED 
(
	[CustomPageNavigationSectionID] ASC
)
)
GO



insert into dbo.[CustomPageNavigationSection]([CustomPageNavigationSectionID], [CustomPageNavigationSectionName]) 
values (1, 'About')
insert into dbo.[CustomPageNavigationSection]([CustomPageNavigationSectionID], [CustomPageNavigationSectionName]) 
values (2, 'Projects')
insert into dbo.[CustomPageNavigationSection]([CustomPageNavigationSectionID], [CustomPageNavigationSectionName]) 
values (3, 'Financials')
insert into dbo.[CustomPageNavigationSection]([CustomPageNavigationSectionID], [CustomPageNavigationSectionName]) 
values (4, 'ProgramInfo')



alter table dbo.CustomPage
add CustomPageNavigationSectionID int constraint FK_CustomPage_CustomPageNavigationSection_CustomPageNavigationSectionID foreign key references dbo.CustomPageNavigationSection(CustomPageNavigationSectionID);
go

update dbo.CustomPage
set CustomPageNavigationSectionID = 1;
go

alter table dbo.CustomPage
alter column CustomPageNavigationSectionID int not null;
go

insert into dbo.CustomPage(CustomPageDisplayName, CustomPageVanityUrl, CustomPageDisplayTypeID, CustomPageContent, CustomPageNavigationSectionID)
values ('Additional Resources', 'AdditionalResources', 3, '', 4);
insert into dbo.CustomPage(CustomPageDisplayName, CustomPageVanityUrl, CustomPageDisplayTypeID, CustomPageContent, CustomPageNavigationSectionID)
values ('Project Spotlight', 'ProjectSpotlight', 3, '', 2);




/*
    A new custom page under the Program Info menu called Additional Resources exists and editor works

    A new custom page under the Projects menu called Project Spotlight exists and editor works

*/