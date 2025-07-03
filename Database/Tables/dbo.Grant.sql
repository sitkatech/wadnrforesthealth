SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Grant](
	[GrantID] [int] IDENTITY(1,1) NOT NULL,
	[GrantNumber] [varchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[ConditionsAndRequirements] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ComplianceNotes] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CFDANumber] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[GrantName] [varchar](64) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[GrantTypeID] [int] NULL,
	[ShortName] [varchar](64) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[GrantStatusID] [int] NOT NULL,
	[OrganizationID] [int] NOT NULL,
	[TotalAwardAmount] [money] NOT NULL,
 CONSTRAINT [PK_Grant_GrantID] PRIMARY KEY CLUSTERED 
(
	[GrantID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[Grant]  WITH CHECK ADD  CONSTRAINT [FK_Grant_GrantStatus_GrantStatusID] FOREIGN KEY([GrantStatusID])
REFERENCES [dbo].[GrantStatus] ([GrantStatusID])
GO
ALTER TABLE [dbo].[Grant] CHECK CONSTRAINT [FK_Grant_GrantStatus_GrantStatusID]
GO
ALTER TABLE [dbo].[Grant]  WITH CHECK ADD  CONSTRAINT [FK_Grant_GrantType_GrantTypeID] FOREIGN KEY([GrantTypeID])
REFERENCES [dbo].[GrantType] ([GrantTypeID])
GO
ALTER TABLE [dbo].[Grant] CHECK CONSTRAINT [FK_Grant_GrantType_GrantTypeID]
GO
ALTER TABLE [dbo].[Grant]  WITH CHECK ADD  CONSTRAINT [FK_Grant_Organization_OrganizationID] FOREIGN KEY([OrganizationID])
REFERENCES [dbo].[Organization] ([OrganizationID])
GO
ALTER TABLE [dbo].[Grant] CHECK CONSTRAINT [FK_Grant_Organization_OrganizationID]