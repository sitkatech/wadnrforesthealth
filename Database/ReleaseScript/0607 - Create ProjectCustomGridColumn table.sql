SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectCustomGridColumn](
	[ProjectCustomGridColumnID] [int] NOT NULL,
	[ProjectCustomGridColumnName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ProjectCustomGridColumnDisplayName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[IsOptional] [bit] NOT NULL,
 CONSTRAINT [PK_ProjectCustomGridColumn_ProjectCustomGridColumnID] PRIMARY KEY CLUSTERED 
(
	[ProjectCustomGridColumnID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_ProjectCustomGridColumn_ProjectCustomGridColumnDisplayName] UNIQUE NONCLUSTERED 
(
	[ProjectCustomGridColumnDisplayName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_ProjectCustomGridColumn_ProjectCustomGridColumnName] UNIQUE NONCLUSTERED 
(
	[ProjectCustomGridColumnName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

insert into ProjectCustomGridColumn 
	(ProjectCustomGridColumnID, ProjectCustomGridColumnName, ProjectCustomGridColumnDisplayName, IsOptional)
values
(1, 'ProjectName', 'Project Name', 0),
(2, 'PrimaryContactOrganization', 'Primary Contact Organization', 0),
(3, 'ProjectStage', 'Project Stage', 0),
(4, 'NumberOfReportedPerformanceMeasures', '# of Reported Performance Measure Records', 1),
(5, 'ProjectsStewardOrganizationRelationshipToProject', 'Projects Steward Organization Relationship To Project', 1),
(6, 'ProjectPrimaryContact', 'Project Primary Contact', 1),
(7, 'ProjectPrimaryContactEmail', 'Project Primary Contact Email', 1),
(8, 'PlanningDesignStartYear', 'Planning Design Start Year', 1),
(9, 'ImplementationStartYear', 'Implementation Start Year', 1),
(10, 'CompletionYear', 'Completion Year', 1),
(11, 'PrimaryTaxonomyLeaf', 'Primary Taxonomy Leaf', 1),
(12, 'SecondaryTaxonomyLeaf', 'Secondary Taxonomy Leaf', 1),
(13, 'NumberOfReportedExpenditures', '# of Reported Expenditures', 1),
(14, 'FundingType', 'Funding Type', 1),
(15, 'EstimatedTotalCost', 'Estimated Total Cost', 1),
(16, 'SecuredFunding', 'Secured Funding', 1),
(17, 'TargetedFunding', 'Targeted Funding', 1),
(18, 'NoFundingSourceIdentified', 'No Funding Source Identified', 1),
(19, 'ProjectDescription', 'Project Description', 1),
(20, 'NumberOfPhotos', '# of Photos', 1),
(21, 'GeospatialAreaName', 'Geospatial Area Name', 1),
(22, 'CustomAttribute', 'Custom Attribute', 1),
(23, 'ProjectID', 'ProjectID', 1),
(24, 'ProjectLastUpdated', 'Last Updated', 1),
(25, 'ProjectStatus', 'Status', 1),
(26, 'FinalStatusUpdateStatus', 'Final Status Update', 1),
(27, 'ProjectCategory', 'Project Category', 1),
(28, 'NumberOfExpectedPerformanceMeasureRecords', '# of Expected Performance Measures Records', 1),
(29, 'Solicitation', 'Solicitation', 1)