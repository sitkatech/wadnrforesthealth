SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ForesterWorkUnitPerson](
	[ForesterWorkUnitPersonID] [int] IDENTITY(1,1) NOT NULL,
	[ForesterWorkUnitID] [int] NOT NULL,
	[PersonID] [int] NOT NULL,
 CONSTRAINT [PK_ForesterWorkUnitPerson_ForesterWorkUnitPersonID] PRIMARY KEY CLUSTERED 
(
	[ForesterWorkUnitPersonID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ForesterWorkUnitPerson]  WITH CHECK ADD  CONSTRAINT [FK_ForesterWorkUnitPerson_ForesterWorkUnit_ForesterWorkUnitID] FOREIGN KEY([ForesterWorkUnitID])
REFERENCES [dbo].[ForesterWorkUnit] ([ForesterWorkUnitID])
GO
ALTER TABLE [dbo].[ForesterWorkUnitPerson] CHECK CONSTRAINT [FK_ForesterWorkUnitPerson_ForesterWorkUnit_ForesterWorkUnitID]
GO
ALTER TABLE [dbo].[ForesterWorkUnitPerson]  WITH CHECK ADD  CONSTRAINT [FK_ForesterWorkUnitPerson_Person_PersonID] FOREIGN KEY([PersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO
ALTER TABLE [dbo].[ForesterWorkUnitPerson] CHECK CONSTRAINT [FK_ForesterWorkUnitPerson_Person_PersonID]