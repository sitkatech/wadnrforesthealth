SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Organization](
	[OrganizationID] [int] IDENTITY(1,1) NOT NULL,
	[OrganizationGuid] [uniqueidentifier] NULL,
	[OrganizationName] [varchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[OrganizationShortName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PrimaryContactPersonID] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[OrganizationUrl] [varchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[LogoFileResourceID] [int] NULL,
	[OrganizationTypeID] [int] NOT NULL,
	[OrganizationBoundary] [geometry] NULL,
	[VendorID] [int] NULL,
	[IsEditable] [bit] NOT NULL,
 CONSTRAINT [PK_Organization_OrganizationID] PRIMARY KEY CLUSTERED 
(
	[OrganizationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_Organization_OrganizationName] UNIQUE NONCLUSTERED 
(
	[OrganizationName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_Organization_OrganizationShortName] UNIQUE NONCLUSTERED 
(
	[OrganizationShortName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[Organization]  WITH CHECK ADD  CONSTRAINT [FK_Organization_FileResource_LogoFileResourceID_FileResourceID] FOREIGN KEY([LogoFileResourceID])
REFERENCES [dbo].[FileResource] ([FileResourceID])
GO
ALTER TABLE [dbo].[Organization] CHECK CONSTRAINT [FK_Organization_FileResource_LogoFileResourceID_FileResourceID]
GO
ALTER TABLE [dbo].[Organization]  WITH CHECK ADD  CONSTRAINT [FK_Organization_OrganizationType_OrganizationTypeID] FOREIGN KEY([OrganizationTypeID])
REFERENCES [dbo].[OrganizationType] ([OrganizationTypeID])
GO
ALTER TABLE [dbo].[Organization] CHECK CONSTRAINT [FK_Organization_OrganizationType_OrganizationTypeID]
GO
ALTER TABLE [dbo].[Organization]  WITH CHECK ADD  CONSTRAINT [FK_Organization_Person_PrimaryContactPersonID_PersonID] FOREIGN KEY([PrimaryContactPersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO
ALTER TABLE [dbo].[Organization] CHECK CONSTRAINT [FK_Organization_Person_PrimaryContactPersonID_PersonID]
GO
ALTER TABLE [dbo].[Organization]  WITH CHECK ADD  CONSTRAINT [FK_Organization_Vendor_VendorID] FOREIGN KEY([VendorID])
REFERENCES [dbo].[Vendor] ([VendorID])
GO
ALTER TABLE [dbo].[Organization] CHECK CONSTRAINT [FK_Organization_Vendor_VendorID]