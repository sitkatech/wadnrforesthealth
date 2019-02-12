SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AgreementGrantAllocation](
	[AgreementGrantAllocationID] [int] IDENTITY(1,1) NOT NULL,
	[AgreementID] [int] NOT NULL,
	[GrantAllocationID] [int] NOT NULL,
 CONSTRAINT [PK_AgreementGrantAllocation_AgreementGrantAllocationID] PRIMARY KEY CLUSTERED 
(
	[AgreementGrantAllocationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_AgreementGrantAllocation_AgreementID_GrantAllocationID] UNIQUE NONCLUSTERED 
(
	[AgreementID] ASC,
	[GrantAllocationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[AgreementGrantAllocation]  WITH CHECK ADD  CONSTRAINT [FK_AgreementGrantAllocation_Agreement_AgreementID] FOREIGN KEY([AgreementID])
REFERENCES [dbo].[Agreement] ([AgreementID])
GO
ALTER TABLE [dbo].[AgreementGrantAllocation] CHECK CONSTRAINT [FK_AgreementGrantAllocation_Agreement_AgreementID]
GO
ALTER TABLE [dbo].[AgreementGrantAllocation]  WITH CHECK ADD  CONSTRAINT [FK_AgreementGrantAllocation_GrantAllocation_GrantAllocationID] FOREIGN KEY([GrantAllocationID])
REFERENCES [dbo].[GrantAllocation] ([GrantAllocationID])
GO
ALTER TABLE [dbo].[AgreementGrantAllocation] CHECK CONSTRAINT [FK_AgreementGrantAllocation_GrantAllocation_GrantAllocationID]