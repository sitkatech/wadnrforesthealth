SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Grant](
	[GrantID] [int] IDENTITY(1,1) NOT NULL,
	[GrantNumber] [varchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[ProgramIndex] [int] NULL,
	[ProjectCode] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ConditionsAndRequirements] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ComplianceNotes] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AwardedFunds] [money] NULL,
	[CFDANumber] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[GrantTitle] [varchar](64) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[GrantTypeID] [int] NULL,
 CONSTRAINT [PK_Grant_GrantID] PRIMARY KEY CLUSTERED 
(
	[GrantID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[Grant]  WITH CHECK ADD  CONSTRAINT [FK_Grant_GrantType_GrantTypeID] FOREIGN KEY([GrantTypeID])
REFERENCES [dbo].[GrantType] ([GrantTypeID])
GO
ALTER TABLE [dbo].[Grant] CHECK CONSTRAINT [FK_Grant_GrantType_GrantTypeID]