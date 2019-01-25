SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AgreementProjectCode](
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
ALTER TABLE [dbo].[AgreementProjectCode] CHECK CONSTRAINT [FK_AgreementProjectCode_Agreement_AgreementID]
GO
ALTER TABLE [dbo].[AgreementProjectCode]  WITH CHECK ADD  CONSTRAINT [FK_AgreementProjectCode_ProjectCode_ProjectCodeID] FOREIGN KEY([ProjectCodeID])
REFERENCES [dbo].[ProjectCode] ([ProjectCodeID])
GO
ALTER TABLE [dbo].[AgreementProjectCode] CHECK CONSTRAINT [FK_AgreementProjectCode_ProjectCode_ProjectCodeID]