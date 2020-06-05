

CREATE TABLE [dbo].[GisUploadAttemptWorkflowSection](
	[GisUploadAttemptWorkflowSectionID] [int] NOT NULL,
	[GisUploadAttemptWorkflowSectionName] [varchar](50) NOT NULL,
	[GisUploadAttemptWorkflowSectionDisplayName] [varchar](50) NOT NULL,
	[SortOrder] [int] NOT NULL,
	[HasCompletionStatus] [bit] NOT NULL,
	[GisUploadAttemptWorkflowSectionGroupingID] [int] NOT NULL,
 CONSTRAINT [PK_GisUploadAttemptWorkflowSection_GisUploadAttemptWorkflowSectionID] PRIMARY KEY CLUSTERED 
(
	[GisUploadAttemptWorkflowSectionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_GisUploadAttemptWorkflowSection_GisUploadAttemptWorkflowSectionDisplayName] UNIQUE NONCLUSTERED 
(
	[GisUploadAttemptWorkflowSectionDisplayName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_GisUploadAttemptWorkflowSection_GisUploadAttemptWorkflowSectionName] UNIQUE NONCLUSTERED 
(
	[GisUploadAttemptWorkflowSectionName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[GisUploadAttemptWorkflowSection]  WITH CHECK ADD  CONSTRAINT [FK_GisUploadAttemptWorkflowSection_GisUploadAttemptWorkflowSectionGrouping_GisUploadAttemptWorkflowSectionGroupingID] FOREIGN KEY([GisUploadAttemptWorkflowSectionGroupingID])
REFERENCES [dbo].[GisUploadAttemptWorkflowSectionGrouping] ([GisUploadAttemptWorkflowSectionGroupingID])
GO

