SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Grant](
	[GrantID] [int] IDENTITY(1,1) NOT NULL,
	[TenantID] [int] NOT NULL,
	[GrantNumber] [varchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[AwardedFunds] [money] NULL,
 CONSTRAINT [PK_Grant_GrantID] PRIMARY KEY CLUSTERED 
(
	[GrantID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Grant]  WITH CHECK ADD  CONSTRAINT [FK_Grant_Tenant_TenantID] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Tenant] ([TenantID])
GO
ALTER TABLE [dbo].[Grant] CHECK CONSTRAINT [FK_Grant_Tenant_TenantID]