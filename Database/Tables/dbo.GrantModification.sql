SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GrantModification](
	[GrantModificationID] [int] IDENTITY(1,1) NOT NULL,
	[GrantModificationName] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[GrantID] [int] NOT NULL,
	[GrantModificationStartDate] [datetime] NOT NULL,
	[GrantModificationEndDate] [datetime] NOT NULL,
	[GrantModificationStatusID] [int] NOT NULL,
	[GrantModificationAmount] [money] NOT NULL,
	[GrantModificationDescription] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_GrantModification_GrantModificationID] PRIMARY KEY CLUSTERED 
(
	[GrantModificationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[GrantModification]  WITH CHECK ADD  CONSTRAINT [FK_GrantModification_Grant_GrantID] FOREIGN KEY([GrantID])
REFERENCES [dbo].[Grant] ([GrantID])
GO
ALTER TABLE [dbo].[GrantModification] CHECK CONSTRAINT [FK_GrantModification_Grant_GrantID]
GO
ALTER TABLE [dbo].[GrantModification]  WITH CHECK ADD  CONSTRAINT [FK_GrantModification_GrantModificationStatus_GrantModificationStatusID] FOREIGN KEY([GrantModificationStatusID])
REFERENCES [dbo].[GrantModificationStatus] ([GrantModificationStatusID])
GO
ALTER TABLE [dbo].[GrantModification] CHECK CONSTRAINT [FK_GrantModification_GrantModificationStatus_GrantModificationStatusID]