SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AgreementPersonRole](
	[AgreementPersonRoleID] [int] NOT NULL,
	[AgreementPersonRoleName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AgreementPersonRoleDisplayName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_AgreementPersonRole_AgreementPersonRoleID] PRIMARY KEY CLUSTERED 
(
	[AgreementPersonRoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_AgreementPersonRole_AgreementPersonRoleDisplayName] UNIQUE NONCLUSTERED 
(
	[AgreementPersonRoleDisplayName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_AgreementPersonRole_AgreementPersonRoleName] UNIQUE NONCLUSTERED 
(
	[AgreementPersonRoleName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
