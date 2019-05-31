--begin tran

ALTER TABLE [dbo].[tmp2015-19_grant_payments_singlesheet] 
add tmp2015ID [int] IDENTITY(1,1) NOT NULL
GO

/****** Object:  Index [PK_GrantAllocationProjectCode_GrantAllocationProjectCodeID]    Script Date: 4/30/2019 10:26:48 AM ******/
ALTER TABLE [dbo].[tmp2015-19_grant_payments_singlesheet] ADD  CONSTRAINT [PK_tmp2015-19_grant_payments_singlesheet_tmp2015ID] PRIMARY KEY CLUSTERED 
(
    tmp2015ID ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO



--select * from [dbo].[tmp2015-19_grant_payments_singlesheet] 

--rollback tran

