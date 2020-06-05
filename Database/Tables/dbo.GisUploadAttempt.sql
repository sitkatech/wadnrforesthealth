SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GisUploadAttempt](
	[GisUploadAttemptID] [int] IDENTITY(1,1) NOT NULL,
	[GisUploadSourceOrganizationID] [int] NOT NULL,
	[GisUploadAttemptCreatePersonID] [int] NOT NULL,
	[GisUploadAttemptCreateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_GisUploadAttempt_GisUploadAttemptID] PRIMARY KEY CLUSTERED 
(
	[GisUploadAttemptID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[GisUploadAttempt]  WITH CHECK ADD  CONSTRAINT [FK_GisUploadAttempt_GisUploadSourceOrganization_GisUploadSourceOrganizationID] FOREIGN KEY([GisUploadSourceOrganizationID])
REFERENCES [dbo].[GisUploadSourceOrganization] ([GisUploadSourceOrganizationID])
GO
ALTER TABLE [dbo].[GisUploadAttempt] CHECK CONSTRAINT [FK_GisUploadAttempt_GisUploadSourceOrganization_GisUploadSourceOrganizationID]
GO
ALTER TABLE [dbo].[GisUploadAttempt]  WITH CHECK ADD  CONSTRAINT [FK_GisUploadAttempt_Person_GisUploadAttemptCreatePersonID_PersonID] FOREIGN KEY([GisUploadAttemptCreatePersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO
ALTER TABLE [dbo].[GisUploadAttempt] CHECK CONSTRAINT [FK_GisUploadAttempt_Person_GisUploadAttemptCreatePersonID_PersonID]