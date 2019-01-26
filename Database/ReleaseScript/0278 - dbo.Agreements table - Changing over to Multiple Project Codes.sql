--begin tran

/*
select * from dbo.tmpAgreement
select * from dbo.Agreement
select * from dbo.ProjectCode
*/


-- Adding joining table for Agreement - ProjectCode
CREATE TABLE [dbo].[AgreementProjectCode]
(
    [AgreementProjectCodeID] [int] IDENTITY(1,1) NOT NULL,
    [AgreementID] [int] NOT NULL,
    [ProjectCodeID] [int] NOT NULL,
 CONSTRAINT [PK_AgreementProjectCode_AgreementProjectCodeID] PRIMARY KEY CLUSTERED 
(
    [AgreementProjectCodeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_AgreementProjectCode_AgreementID_ProjectCodeID] UNIQUE NONCLUSTERED 
(
    [AgreementID] ASC,
    [ProjectCodeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[AgreementProjectCode]  WITH CHECK ADD  CONSTRAINT [FK_AgreementProjectCode_Agreement_AgreementID] FOREIGN KEY([AgreementID])
REFERENCES [dbo].[Agreement] ([AgreementID])
GO

ALTER TABLE [dbo].[AgreementProjectCode]  WITH CHECK ADD  CONSTRAINT [FK_AgreementProjectCode_ProjectCode_ProjectCodeID] FOREIGN KEY([ProjectCodeID])
REFERENCES [dbo].[ProjectCode] ([ProjectCodeID])
GO

-- Dropping original, singular ProjectCodeID from Agreement table

alter table dbo.Agreement
drop constraint FK_Agreement_ProjectCode_ProjectCodeID

alter table dbo.Agreement
drop column ProjectCodeID

-- pre-chewing the instance of multiple project codes where they used & instead of ,
update tmpAgreement
set ProgramIndexProjectCode = REPLACE(ProgramIndexProjectCode, ' & ', ', ')

--rollback tran

