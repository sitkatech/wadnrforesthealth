SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GrantModificationGrantModificationPurpose](
	[GrantModificationGrantModificationPurposeID] [int] IDENTITY(1,1) NOT NULL,
	[GrantModificationID] [int] NOT NULL,
	[GrantModificationPurposeID] [int] NOT NULL,
 CONSTRAINT [PK_GrantModificationGrantModificationPurpose_GrantModificationGrantModificationPurposeID] PRIMARY KEY CLUSTERED 
(
	[GrantModificationGrantModificationPurposeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_GrantModificationGrantModificationPurpose_GrantModificationPurposeID_GrantModificationID] UNIQUE NONCLUSTERED 
(
	[GrantModificationPurposeID] ASC,
	[GrantModificationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[GrantModificationGrantModificationPurpose]  WITH CHECK ADD  CONSTRAINT [FK_GrantModificationGrantModificationPurpose_GrantModification_GrantModificationID] FOREIGN KEY([GrantModificationID])
REFERENCES [dbo].[GrantModification] ([GrantModificationID])
GO
ALTER TABLE [dbo].[GrantModificationGrantModificationPurpose] CHECK CONSTRAINT [FK_GrantModificationGrantModificationPurpose_GrantModification_GrantModificationID]
GO
ALTER TABLE [dbo].[GrantModificationGrantModificationPurpose]  WITH CHECK ADD  CONSTRAINT [FK_GrantModificationGrantModificationPurpose_GrantModificationPurpose_GrantModificationPurposeID] FOREIGN KEY([GrantModificationPurposeID])
REFERENCES [dbo].[GrantModificationPurpose] ([GrantModificationPurposeID])
GO
ALTER TABLE [dbo].[GrantModificationGrantModificationPurpose] CHECK CONSTRAINT [FK_GrantModificationGrantModificationPurpose_GrantModificationPurpose_GrantModificationPurposeID]