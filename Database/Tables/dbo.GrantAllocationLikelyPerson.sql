SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GrantAllocationLikelyPerson](
	[GrantAllocationLikelyUserID] [int] IDENTITY(1,1) NOT NULL,
	[GrantAllocationID] [int] NOT NULL,
	[PersonID] [int] NOT NULL,
 CONSTRAINT [PK_GrantAllocationLikelyUser_GrantAllocationLikelyUserID] PRIMARY KEY CLUSTERED 
(
	[GrantAllocationLikelyUserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_GrantAllocationLikelyPerson_GrantAllocationID_PersonID] UNIQUE NONCLUSTERED 
(
	[GrantAllocationID] ASC,
	[PersonID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[GrantAllocationLikelyPerson]  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocationLikelyPerson_GrantAllocation_GrantAllocationID] FOREIGN KEY([GrantAllocationID])
REFERENCES [dbo].[GrantAllocation] ([GrantAllocationID])
GO
ALTER TABLE [dbo].[GrantAllocationLikelyPerson] CHECK CONSTRAINT [FK_GrantAllocationLikelyPerson_GrantAllocation_GrantAllocationID]
GO
ALTER TABLE [dbo].[GrantAllocationLikelyPerson]  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocationLikelyPerson_Person_PersonID] FOREIGN KEY([PersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO
ALTER TABLE [dbo].[GrantAllocationLikelyPerson] CHECK CONSTRAINT [FK_GrantAllocationLikelyPerson_Person_PersonID]