SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GrantAllocationProjectCode](
	[GrantAllocationProjectCodeID] [int] IDENTITY(1,1) NOT NULL,
	[TenantID] [int] NOT NULL,
	[GrantAllocationID] [int] NOT NULL,
	[ProjectCodeID] [int] NOT NULL,
 CONSTRAINT [PK_GrantAllocationProjectCode_GrantAllocationProjectCodeID] PRIMARY KEY CLUSTERED 
(
	[GrantAllocationProjectCodeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_GrantAllocationProjectCode_GrantAllocationID_ProjectCodeID] UNIQUE NONCLUSTERED 
(
	[GrantAllocationID] ASC,
	[ProjectCodeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[GrantAllocationProjectCode]  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocationProjectCode_GrantAllocation_GrantAllocationID] FOREIGN KEY([GrantAllocationID])
REFERENCES [dbo].[Tenant] ([TenantID])
GO
ALTER TABLE [dbo].[GrantAllocationProjectCode] CHECK CONSTRAINT [FK_GrantAllocationProjectCode_GrantAllocation_GrantAllocationID]
GO
ALTER TABLE [dbo].[GrantAllocationProjectCode]  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocationProjectCode_ProjectCode_ProjectCodeID] FOREIGN KEY([ProjectCodeID])
REFERENCES [dbo].[Tenant] ([TenantID])
GO
ALTER TABLE [dbo].[GrantAllocationProjectCode] CHECK CONSTRAINT [FK_GrantAllocationProjectCode_ProjectCode_ProjectCodeID]
GO
ALTER TABLE [dbo].[GrantAllocationProjectCode]  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocationProjectCode_Tenant_TenantID] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Tenant] ([TenantID])
GO
ALTER TABLE [dbo].[GrantAllocationProjectCode] CHECK CONSTRAINT [FK_GrantAllocationProjectCode_Tenant_TenantID]