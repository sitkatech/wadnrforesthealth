SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GrantAllocation](
	[GrantAllocationID] [int] IDENTITY(1,1) NOT NULL,
	[TenantID] [int] NOT NULL,
	[GrantID] [int] NOT NULL,
	[ProjectName] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[AllocationAmount] [money] NULL,
	[CostTypeID] [int] NULL,
	[ProgramIndex] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ProgramManagerPersonID] [int] NULL,
 CONSTRAINT [PK_GrantAllocation_GrantAllocationID] PRIMARY KEY CLUSTERED 
(
	[GrantAllocationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[GrantAllocation]  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocation_CostType_CostTypeID] FOREIGN KEY([CostTypeID])
REFERENCES [dbo].[CostType] ([CostTypeID])
GO
ALTER TABLE [dbo].[GrantAllocation] CHECK CONSTRAINT [FK_GrantAllocation_CostType_CostTypeID]
GO
ALTER TABLE [dbo].[GrantAllocation]  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocation_Grant_GrantID] FOREIGN KEY([GrantID])
REFERENCES [dbo].[Grant] ([GrantID])
GO
ALTER TABLE [dbo].[GrantAllocation] CHECK CONSTRAINT [FK_GrantAllocation_Grant_GrantID]
GO
ALTER TABLE [dbo].[GrantAllocation]  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocation_Tenant_TenantID] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Tenant] ([TenantID])
GO
ALTER TABLE [dbo].[GrantAllocation] CHECK CONSTRAINT [FK_GrantAllocation_Tenant_TenantID]