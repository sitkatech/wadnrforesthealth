
alter table dbo.[Grant]
drop CONSTRAINT FK_Grant_GrantStatus_GrantStatusID

alter table dbo.[GrantStatus]
drop PK_GrantStatus_GrantStatusID

go

drop table dbo.GrantStatus

go

CREATE TABLE [dbo].[GrantStatus](
	[GrantStatusID] [int] NOT NULL,
	[GrantStatusName] varchar(50) NOT NULL
 CONSTRAINT [PK_GrantStatus_GrantStatusID] PRIMARY KEY CLUSTERED 
(
	[GrantStatusID] ASC
)
) ON [PRIMARY]

go

Insert into dbo.GrantStatus (GrantStatusID, GrantStatusName)
values


(1, 'Active'),
(2, 'Pending'),
(3, 'Planned'),
(4, 'Closeout')

go


ALTER TABLE [dbo].[Grant]  WITH CHECK ADD  CONSTRAINT [FK_Grant_GrantStatus_GrantStatusID] FOREIGN KEY([GrantStatusID])
REFERENCES [dbo].[GrantStatus] ([GrantStatusID])
GO

ALTER TABLE [dbo].[Grant] CHECK CONSTRAINT [FK_Grant_GrantStatus_GrantStatusID]
GO

