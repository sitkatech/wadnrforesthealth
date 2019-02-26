SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person](
	[PersonID] [int] IDENTITY(1,1) NOT NULL,
	[PersonUniqueIdentifier] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FirstName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[LastName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Email] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Phone] [varchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PasswordPdfK2SaltHash] [nvarchar](1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[RoleID] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NULL,
	[LastActivityDate] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
	[OrganizationID] [int] NULL,
	[ReceiveSupportEmails] [bit] NOT NULL,
	[WebServiceAccessToken] [uniqueidentifier] NULL,
	[LoginName] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MiddleName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Notes] [varchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PersonAddress] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AddedByPersonID] [int] NULL,
	[VendorID] [int] NULL,
 CONSTRAINT [PK_Person_PersonID] PRIMARY KEY CLUSTERED 
(
	[PersonID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING ON

GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_Person_PersonUniqueIdentifier] ON [dbo].[Person]
(
	[PersonUniqueIdentifier] ASC
)
WHERE ([PersonUniqueIdentifier] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Person]  WITH CHECK ADD  CONSTRAINT [FK_Person_Organization_OrganizationID] FOREIGN KEY([OrganizationID])
REFERENCES [dbo].[Organization] ([OrganizationID])
GO
ALTER TABLE [dbo].[Person] CHECK CONSTRAINT [FK_Person_Organization_OrganizationID]
GO
ALTER TABLE [dbo].[Person]  WITH CHECK ADD  CONSTRAINT [FK_Person_Person_AddedByPersonID_PersonID] FOREIGN KEY([AddedByPersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO
ALTER TABLE [dbo].[Person] CHECK CONSTRAINT [FK_Person_Person_AddedByPersonID_PersonID]
GO
ALTER TABLE [dbo].[Person]  WITH CHECK ADD  CONSTRAINT [FK_Person_Role_RoleID] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([RoleID])
GO
ALTER TABLE [dbo].[Person] CHECK CONSTRAINT [FK_Person_Role_RoleID]
GO
ALTER TABLE [dbo].[Person]  WITH CHECK ADD  CONSTRAINT [FK_Person_Vendor_VendorID] FOREIGN KEY([VendorID])
REFERENCES [dbo].[Vendor] ([VendorID])
GO
ALTER TABLE [dbo].[Person] CHECK CONSTRAINT [FK_Person_Vendor_VendorID]