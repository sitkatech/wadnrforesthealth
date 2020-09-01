SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GisUploadSourceOrganization](
	[GisUploadSourceOrganizationID] [int] IDENTITY(1,1) NOT NULL,
	[GisUploadSourceOrganizationName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ProjectTypeDefaultName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TreatmentTypeDefaultName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ImportIsFlattened] [bit] NULL,
	[RequireCompletionDate] [bit] NOT NULL,
 CONSTRAINT [PK_GisUploadSourceOrganization_GisUploadSourceOrganizationID] PRIMARY KEY CLUSTERED 
(
	[GisUploadSourceOrganizationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
