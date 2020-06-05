
/****** Object:  Table [dbo].[ProjectType]    Script Date: 6/4/2020 1:20:04 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[GisUploadSourceOrganization](
	[GisUploadSourceOrganizationID] [int] IDENTITY(1,1) NOT NULL,
	[GisUploadSourceOrganizationName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_GisUploadSourceOrganization_GisUploadSourceOrganizationID] PRIMARY KEY CLUSTERED 
(
	[GisUploadSourceOrganizationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


insert into dbo.GisUploadSourceOrganization(GisUploadSourceOrganizationName)
select 'DNR'

insert into dbo.GisUploadSourceOrganization(GisUploadSourceOrganizationName)
select 'USFS'