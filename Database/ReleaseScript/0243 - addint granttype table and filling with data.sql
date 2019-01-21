CREATE TABLE [dbo].[GrantType](
	[GrantTypeID] [int] IDENTITY(1,1) NOT  NULL,
	[GrantTypeName] [varchar](100) NOT NULL
 CONSTRAINT [PK_GrantType_GrantTypeID] PRIMARY KEY CLUSTERED 
(
	GrantTypeID ASC
) ON [PRIMARY],
 CONSTRAINT [AK_GrantType_GrantTypeName] UNIQUE NONCLUSTERED 
(
	GrantTypeName ASC
) ON [PRIMARY])

go

insert into dbo.GrantType (GrantTypeName)
values('Stand Alone')

insert into dbo.GrantType (GrantTypeName)
values('CPG')