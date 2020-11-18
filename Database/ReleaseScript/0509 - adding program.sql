
CREATE TABLE [dbo].[Program](
	[ProgramID] [int] IDENTITY(1,1) NOT NULL,
    [OrganizationID] int not null,
	[ProgramName] [varchar](200) NOT NULL,
	[ProgramShortName] [varchar](50) NOT NULL,
	[ProgramPrimaryContactPersonID] [int] NULL,
	[ProgramIsActive] [bit] NOT NULL,
    [ProgramCreateDate] datetime not null,
    [ProgramCreatePersonID] int not null,
    [ProgramLastUpdatedDate] datetime null,
    [ProgramLastUpdatedByPersonID] int null
 CONSTRAINT [PK_Program_ProgramID] PRIMARY KEY CLUSTERED 
(
	[ProgramID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_Program_ProgramName_OrganizationID] UNIQUE NONCLUSTERED 
(
	[ProgramName], OrganizationID ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_Program_ProgramShortName_OrganizationID] UNIQUE NONCLUSTERED 
(
	[ProgramShortName], OrganizationID ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Program]  WITH CHECK ADD  CONSTRAINT [FK_Program_Organization_OrganizationID] FOREIGN KEY([OrganizationID])
REFERENCES [dbo].[Organization] ([OrganizationID])
GO


ALTER TABLE [dbo].[Program]  WITH CHECK ADD  CONSTRAINT [FK_Program_Person_ProgramPrimaryContactPersonID_PersonIDID] FOREIGN KEY([ProgramPrimaryContactPersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO


ALTER TABLE [dbo].[Program]  WITH CHECK ADD  CONSTRAINT [FK_Program_Person_ProgramCreatePersonID_PersonIDID] FOREIGN KEY([ProgramCreatePersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO



ALTER TABLE [dbo].[Program]  WITH CHECK ADD  CONSTRAINT [FK_Program_Person_ProgramLastUpdatedByPersonID_PersonIDID] FOREIGN KEY([ProgramLastUpdatedByPersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO


go

ALTER TABLE dbo.GisUploadSourceOrganization ADD ProgramID int

ALTER TABLE [dbo].[GisUploadSourceOrganizationID]  WITH CHECK ADD  CONSTRAINT [FK_GisUploadSourceOrganization_Program_ProgramID] FOREIGN KEY([ProgramID])
REFERENCES [dbo].[Program] ([ProgramID])
GO

insert into dbo.Program (OrganizationID, ProgramName, ProgramShortName, ProgramIsActive, ProgramCreateDate, ProgramCreatePersonID)
select x.DefaultLeadImplementerOrganizationID, x.GisUploadSourceOrganizationName, x.GisUploadSourceOrganizationName, 1, GetDate(), 5285
 from dbo.GisUploadSourceOrganization x


 go

 update dbo.GisUploadSourceOrganization
 set ProgramID = p.ProgramID
 from dbo.GisUploadSourceOrganization x
 join dbo.Program p on x.GisUploadSourceOrganizationName = p.ProgramName and x.DefaultLeadImplementerOrganizationID = p.OrganizationID


 ALTER TABLE dbo.GisUploadSourceOrganization ALTER COLUMN ProgramID int not null
