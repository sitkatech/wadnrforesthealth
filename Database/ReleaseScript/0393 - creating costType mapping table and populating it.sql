
CREATE TABLE [dbo].[CostTypeDatamartMapping](
	[CostTypeDatamartMappingID] [int] NOT NULL,
	CostTypeID int not null constraint FK_CostTypeDatamartMapping_CostType_CostTypeID references dbo.CostType(CostTypeID),
	DatamartObjectCode [varchar](10) NOT NULL,
	DatamartObjectName varchar(100) NOT NULL,
 CONSTRAINT [PK_CostTypeDatamartMapping_CostTypeDatamartMappingID] PRIMARY KEY CLUSTERED 
(
	[CostTypeDatamartMappingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


Insert into dbo.CostTypeDatamartMapping (CostTypeDatamartMappingID, CostTypeID, DatamartObjectCode, DatamartObjectName)
values
(1, 3, 'A', 'SALARIES AND WAGES'),
(2, 3, 'B', 'EMPLOYEE BENEFITS'),
(3, 4, 'G', 'TRAVEL'),
(4, 5, 'N', 'GRANTS, BENEFITS & CLIENT SERVICES')
