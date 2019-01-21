CREATE TABLE [dbo].[GrantStatus](
	[GrantStatusID] [int] IDENTITY(1,1) NOT  NULL,
	[GrantStatusName] [varchar](100) NOT NULL
 CONSTRAINT [PK_GrantStatus_GrantStatusID] PRIMARY KEY CLUSTERED 
(
	GrantStatusID ASC
) ON [PRIMARY],
 CONSTRAINT [AK_GrantStatus_GrantStatusName] UNIQUE NONCLUSTERED 
(
	GrantStatusName ASC
) ON [PRIMARY])

go

insert into dbo.GrantStatus (GrantStatusName)
values('Active')

insert into dbo.GrantStatus (GrantStatusName)
values('Pending')

insert into dbo.GrantStatus (GrantStatusName)
values('Planned')

insert into dbo.GrantStatus (GrantStatusName)
values('Closeout')