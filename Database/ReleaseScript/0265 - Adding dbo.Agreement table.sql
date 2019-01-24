--begin tran

create table dbo.AgreementType
(
    AgreementTypeID int identity(1,1) not null,
    AgreementTypeAbbrev varchar(100) null,
    -- I don't know what these mean to start, but I hope to
    AgreementTypeName varchar(100) null
)
GO

ALTER TABLE [dbo].AgreementType ADD  CONSTRAINT [PK_AgreementType_AgreementTypeID] PRIMARY KEY CLUSTERED 
(
    AgreementTypeID ASC
) ON [PRIMARY]
GO

insert into dbo.AgreementType (AgreementTypeAbbrev, AgreementTypeName)
values
('981-HIS', null),
(null, 'Authority'),
('CA', 'Cooperative Agreement'),
(null, 'Cooperative Agreement 14-267'),
(null, 'Collection Agreement'),
('CS', 'Contract for Services'),
('CS-FR', null),
('CST', 'Cost-Share Agreement'),
('IAA', 'Interagency Agreement'),
(null, 'MASTER'),
('MOU', 'Memorandum of Understanding'),
(null, 'Participating Agreement'),
('SPP', 'Supplemental Project Plan'),
(null, 'Work Order'),
('WRC&DC', null)
GO

CREATE TABLE dbo.Agreement
(
    AgreementID [int] IDENTITY(1,1) NOT NULL,
    TmpAgreementID int null,
    AgreementTypeID [int] NULL,
    AgreementNumber [nvarchar](100) NULL,
    -- SourceOfFunding
    --AgencyEntity
    --PM (Person?)
    ProjectCodeID int null,
    StartDate [datetime] NULL,
    EndDate [datetime] NULL,
    AgreementAmount [money] NULL,
    ExpendedAmount [money] NULL,
    BalanceAmount [money] NULL,
    RegionID [int] NULL,
    -- Activity
    FirstBillDueOn [datetime] null,
    Notes varchar(max) null
    -- PartnerContact  (email address)

-- EmpID INT Foreign Key References EmployeeDetails(EmpID) 
)
GO

ALTER TABLE [dbo].Agreement ADD  CONSTRAINT [PK_Agreement_AgreementID] PRIMARY KEY CLUSTERED 
(
    AgreementID ASC
) ON [PRIMARY]
GO

-- Not needed long term, but very helpful in the short term while we work on migration
ALTER TABLE [dbo].Agreement  WITH CHECK ADD  CONSTRAINT [FK_Agreement_TmpAgreement_TmpAgreementID] FOREIGN KEY([TmpAgreementID])
REFERENCES [dbo].[TmpAgreement] ([TmpAgreementID])
GO

ALTER TABLE [dbo].Agreement  WITH CHECK ADD  CONSTRAINT [FK_Agreement_AgreementType_AgreementTypeID] FOREIGN KEY([AgreementTypeID])
REFERENCES [dbo].[AgreementType] ([AgreementTypeID])
GO

ALTER TABLE [dbo].Agreement  WITH CHECK ADD  CONSTRAINT [FK_Agreement_ProjectCode_ProjectCodeID] FOREIGN KEY([ProjectCodeID])
REFERENCES [dbo].[ProjectCode] ([ProjectCodeID])
GO

ALTER TABLE [dbo].Agreement  WITH CHECK ADD  CONSTRAINT [FK_Agreement_Region_RegionID] FOREIGN KEY(RegionID)
REFERENCES [dbo].Region (RegionID)
GO

-- The data actually has duplicate Agreement numbers; it's an open question about what to do about them, we need to talk
-- to the client. I'm starting off from the sane perspective that Agreement numbers shouldn't be duplicated, however.
CREATE UNIQUE NONCLUSTERED INDEX UNIQUE_INDEX_Agreement_AgreementNumber
ON dbo.Agreement(AgreementNumber)
WHERE AgreementNumber IS NOT NULL

--rollback tran