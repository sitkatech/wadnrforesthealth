ALTER TABLE dbo.Treatment add ProgramID int
ALTER TABLE dbo.Treatment add ImportedFromGis bit



ALTER TABLE [dbo].[Treatment]  WITH CHECK ADD  CONSTRAINT [FK_Treatment_Program_ProgramID] FOREIGN KEY([ProgramID])
REFERENCES [dbo].[Program] ([ProgramID])
GO


ALTER TABLE dbo.ProjectLocation ADD ProgramID int
ALTER TABLE dbo.ProjectLocation ADD ImportedFromGisUpload bit


ALTER TABLE [dbo].[ProjectLocation]  WITH CHECK ADD  CONSTRAINT [FK_ProjectLocation_Program_ProgramID] FOREIGN KEY([ProgramID])
REFERENCES [dbo].[Program] ([ProgramID])
GO




CREATE TABLE [dbo].[ProjectProgram](
	[ProjectProgramID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectID] [int] NOT NULL,
	[ProgramID] [int] NOT NULL,
 CONSTRAINT [PK_ProjectProgram_ProjectProgramID] PRIMARY KEY CLUSTERED 
(
	[ProjectProgramID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_ProjectProgram_ProjectID_ProgramID] UNIQUE NONCLUSTERED 
(
	[ProjectID] ASC,
	[ProgramID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ProjectProgram]  WITH CHECK ADD  CONSTRAINT [FK_ProjectProgram_Program_ProgramID] FOREIGN KEY([ProgramID])
REFERENCES [dbo].[Program] ([ProgramID])
GO



ALTER TABLE [dbo].[ProjectProgram]  WITH CHECK ADD  CONSTRAINT [FK_ProjectProgram_Project_ProjectID] FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Project] ([ProjectID])
GO

CREATE TABLE [dbo].[GisUploadProgramMergeGrouping](
	[GisUploadProgramMergeGroupingID] [int] IDENTITY(1,1) NOT NULL,
    [GisUploadProgramMergeGroupingName] varchar(100) not null
 CONSTRAINT [PK_GisUploadProgramMergeGrouping_GisUploadProgramMergeGroupingID] PRIMARY KEY CLUSTERED 
(
	[GisUploadProgramMergeGroupingID] ASC
)
)
GO

ALTER TABLE [dbo].[GisUploadSourceOrganization] ADD GisUploadProgramMergeGroupingID int



ALTER TABLE [dbo].[GisUploadSourceOrganization]  WITH CHECK ADD  CONSTRAINT [FK_GisUploadSourceOrganization_GisUploadProgramMergeGrouping_GisUploadProgramMergeGroupingID] FOREIGN KEY([GisUploadProgramMergeGroupingID])
REFERENCES [dbo].[GisUploadProgramMergeGrouping] ([GisUploadProgramMergeGroupingID])
GO

insert into dbo.GisUploadProgramMergeGrouping(GisUploadProgramMergeGroupingName)
values ('USFS Merge Group')


update dbo.GisUploadSourceOrganization
set GisUploadProgramMergeGroupingID = 1
where GisUploadSourceOrganizationID in(9,10,11)