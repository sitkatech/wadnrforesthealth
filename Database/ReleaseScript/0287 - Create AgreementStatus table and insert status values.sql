CREATE TABLE [dbo].[AgreementStatus](
	[AgreementStatusID] [int] IDENTITY(1,1) NOT NULL,
	[AgreementStatusName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_AgreementStatus_AgreementStatusID] PRIMARY KEY CLUSTERED 
(
	[AgreementStatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_AgreementStatus_AgreementStatusName] UNIQUE NONCLUSTERED 
(
	[AgreementStatusName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

INSERT INTO dbo.AgreementStatus(AgreementStatusName) 
VALUES
('Active'),
('Complete')
GO