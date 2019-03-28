CREATE TABLE [dbo].[Division](
	[DivisionID] [int] IDENTITY(1,1) NOT NULL,
	[DivisionName] [varchar](200) NOT NULL,
	[DivisionDisplayName] [varchar](200) NOT NULL
 CONSTRAINT [PK_Division_DivisionID] PRIMARY KEY CLUSTERED 
(
	[DivisionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_Division_DivisionName] UNIQUE NONCLUSTERED 
(
	[DivisionName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

insert dbo.Division (DivisionName, DivisionDisplayName) 
values 
('ForestHealth', 'DNR Headquarters - Forest Health'),
('Wildfire', 'DNR Headquarters - Wildfire')
GO

alter table dbo.GrantAllocation
add DivisionID int null constraint FK_GrantAllocation_Division_DivisionID foreign key references dbo.Division(DivisionID)