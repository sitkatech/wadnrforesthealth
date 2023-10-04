SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectSimpleLocationFixes](
	[ProjectID] [int] NOT NULL,
	[OldProjectLocationPoint] [geometry] NULL,
	[NewProjectLocationPoint] [geometry] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ProjectSimpleLocationFixes_ProjectID] PRIMARY KEY CLUSTERED 
(
	[ProjectID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[ProjectSimpleLocationFixes]  WITH CHECK ADD  CONSTRAINT [FK_ProjectSimpleLocationFixes_Project_ProjectID] FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Project] ([ProjectID])
GO
ALTER TABLE [dbo].[ProjectSimpleLocationFixes] CHECK CONSTRAINT [FK_ProjectSimpleLocationFixes_Project_ProjectID]