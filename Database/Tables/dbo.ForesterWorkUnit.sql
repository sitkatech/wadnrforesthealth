SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ForesterWorkUnit](
	[ForesterWorkUnitID] [int] IDENTITY(1,1) NOT NULL,
	[ForesterRoleID] [int] NOT NULL,
	[PersonID] [int] NULL,
	[ForesterWorkUnitName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[RegionName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ForesterWorkUnitLocation] [geometry] NOT NULL,
 CONSTRAINT [PK_ForesterWorkUnit_ForesterWorkUnitID] PRIMARY KEY CLUSTERED 
(
	[ForesterWorkUnitID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[ForesterWorkUnit]  WITH CHECK ADD  CONSTRAINT [FK_ForesterWorkUnit_ForesterRole_ForesterRoleID] FOREIGN KEY([ForesterRoleID])
REFERENCES [dbo].[ForesterRole] ([ForesterRoleID])
GO
ALTER TABLE [dbo].[ForesterWorkUnit] CHECK CONSTRAINT [FK_ForesterWorkUnit_ForesterRole_ForesterRoleID]
GO
ALTER TABLE [dbo].[ForesterWorkUnit]  WITH CHECK ADD  CONSTRAINT [FK_ForesterWorkUnit_Person_PersonID] FOREIGN KEY([PersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO
ALTER TABLE [dbo].[ForesterWorkUnit] CHECK CONSTRAINT [FK_ForesterWorkUnit_Person_PersonID]