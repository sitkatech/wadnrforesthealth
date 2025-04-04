SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrganizationCode](
	[OrganizationCodeID] [int] NOT NULL,
	[OrganizationCodeName] [varchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[OrganizationCodeValue] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_OrganizationCode_OrganizationCodeID] PRIMARY KEY CLUSTERED 
(
	[OrganizationCodeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
