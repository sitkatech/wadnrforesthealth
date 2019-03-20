SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonEnvironmentCredential](
	[PersonEnvironmentCredentialID] [int] IDENTITY(1,1) NOT NULL,
	[PersonID] [int] NOT NULL,
	[AuthenticatorID] [int] NOT NULL,
	[PersonUniqueIdentifier] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_PersonEnvironmentCredential_PersonEnvironmentCredentialID] PRIMARY KEY CLUSTERED 
(
	[PersonEnvironmentCredentialID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_PersonEnvironmentCredential_AuthenticatorID_PersonUniqueIdentifier] UNIQUE NONCLUSTERED 
(
	[AuthenticatorID] ASC,
	[PersonUniqueIdentifier] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[PersonEnvironmentCredential]  WITH CHECK ADD  CONSTRAINT [FK_PersonEnvironmentCredential_Authenticator_AuthenticatorID] FOREIGN KEY([AuthenticatorID])
REFERENCES [dbo].[Authenticator] ([AuthenticatorID])
GO
ALTER TABLE [dbo].[PersonEnvironmentCredential] CHECK CONSTRAINT [FK_PersonEnvironmentCredential_Authenticator_AuthenticatorID]
GO
ALTER TABLE [dbo].[PersonEnvironmentCredential]  WITH CHECK ADD  CONSTRAINT [FK_PersonEnvironmentCredential_Person_PersonID] FOREIGN KEY([PersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO
ALTER TABLE [dbo].[PersonEnvironmentCredential] CHECK CONSTRAINT [FK_PersonEnvironmentCredential_Person_PersonID]