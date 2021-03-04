
CREATE TABLE [dbo].[GisExcludeIncludeColumn](
	[GisExcludeIncludeColumnID] [int] IDENTITY(1,1) NOT NULL,
	[GisUploadSourceOrganizationID] [int] NOT NULL,
	[GisDefaultMappingColumnName] [varchar](300) NOT NULL,
    [IsWhitelist] bit not null,
    [IsBlacklist] as cast(case when IsWhitelist = 1 then 0 else 1 end as bit),
 CONSTRAINT [PK_GisExcludeIncludeColumn_GisExcludeIncludeColumnID] PRIMARY KEY CLUSTERED 
(
	[GisExcludeIncludeColumnID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[GisExcludeIncludeColumn]  WITH CHECK ADD  CONSTRAINT [FK_GisExcludeIncludeColumn_GisUploadSourceOrganization_GisUploadSourceOrganizationID] FOREIGN KEY([GisUploadSourceOrganizationID])
REFERENCES [dbo].[GisUploadSourceOrganization] ([GisUploadSourceOrganizationID])
GO

go

CREATE TABLE dbo.GisExcludeIncludeColumnValue(
[GisExcludeIncludeColumnValueID] [int] IDENTITY(1,1) NOT NULL,
[GisExcludeIncludeColumnID] [int] not null,
GisExcludeIncludeColumnValueForFiltering varchar(200) not null,
 CONSTRAINT [PK_GisExcludeIncludeColumnValue_GisExcludeIncludeColumnValueID] PRIMARY KEY CLUSTERED 
(
	[GisExcludeIncludeColumnValueID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


ALTER TABLE [dbo].[GisExcludeIncludeColumnValue]  WITH CHECK ADD  CONSTRAINT [FK_GisExcludeIncludeColumnValue_GisExcludeIncludeColumn_GisExcludeIncludeColumnID] FOREIGN KEY([GisExcludeIncludeColumnID])
REFERENCES [dbo].[GisExcludeIncludeColumn] ([GisExcludeIncludeColumnID])
GO