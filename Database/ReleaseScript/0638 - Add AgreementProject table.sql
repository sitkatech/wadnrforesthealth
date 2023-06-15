create table dbo.AgreementProject
(
    AgreementProjectID int identity(1, 1) not null,
    AgreementID int not null,
    ProjectID int not null,

    constraint [PK_AgreementProject_AgreementProjectID] primary key clustered 
    (
        AgreementProjectID asc
    ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],

    constraint [PK_AgreementProject_ProjectID_AgreementID] unique nonclustered
    (
        AgreementID ASC,
	    ProjectID ASC
    ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]

) ON [PRIMARY]

ALTER TABLE dbo.AgreementProject  WITH CHECK ADD CONSTRAINT [FK_AgreementProject_Agreement_AgreementID] FOREIGN KEY(AgreementID)
REFERENCES dbo.Agreement (AgreementID)
GO

ALTER TABLE dbo.AgreementProject CHECK CONSTRAINT [FK_AgreementProject_Agreement_AgreementID]
GO

ALTER TABLE dbo.AgreementProject  WITH CHECK ADD CONSTRAINT [FK_AgreementProject_Project_ProjectID] FOREIGN KEY(ProjectID)
REFERENCES dbo.Project (ProjectID)
GO

ALTER TABLE dbo.AgreementProject CHECK CONSTRAINT [FK_AgreementProject_Project_ProjectID]
GO

--Data per spreadsheet sent by Jen Watkins, WADNR
/*

    93-100689	DNR-US Forest Service OWNF:  Upper Swauk and Mount Hull	FHT-2021-02189
    93-100689	DNR-US Forest Service OWNF:  Upper Swauk and Mount Hull FHT-2021-02167
    93-100815	DNR-US Forest Service GPNF:  Upper White and Little White Salmon	FHT-2021-02184
    93-100588	DNR-US Forest Service UNF:  Non-Commercial Thin	FHT-2021-02064
    93-100916	DNR-Kalispel Tribe of Indians:  Sxwuytn-Kaniksu Connections Project (Trails) Development	FHT-2023-02125
    93-102338	DNR-City of Roslyn: May 2021	FHT-2021-04037
    3003	    Good Neighbor Authority Tillicum Hazardous Fuels Reduction Contract Number #3003	FHT-2020-00046
    93-103352	DNR-Okanogan Wenatchee National Forest: All-Lands Direct Investments HB 1168	FHT-2021-02193
    93-103043	DNR-Columbia Gorge Scenic Area:  All Lands Direct Investments HB 1168	FHT-2020-00009

*/

insert into dbo.AgreementProject
    (AgreementID, ProjectID)
select a.AgreementID, p.ProjectID from dbo.Agreement a, dbo.Project p where a.AgreementNumber = '93-100689' and p.FhtProjectNumber = 'FHT-2021-02189'
union
select a.AgreementID, p.ProjectID from dbo.Agreement a, dbo.Project p where a.AgreementNumber = '93-100689' and p.FhtProjectNumber = 'FHT-2021-02167'
union
select a.AgreementID, p.ProjectID from dbo.Agreement a, dbo.Project p where a.AgreementNumber = '93-100815' and p.FhtProjectNumber = 'FHT-2021-02184'
union
select a.AgreementID, p.ProjectID from dbo.Agreement a, dbo.Project p where a.AgreementNumber = '93-100588' and p.FhtProjectNumber = 'FHT-2021-02064'
union
select a.AgreementID, p.ProjectID from dbo.Agreement a, dbo.Project p where a.AgreementNumber = '93-100916' and p.FhtProjectNumber = 'FHT-2023-02125'
union
select a.AgreementID, p.ProjectID from dbo.Agreement a, dbo.Project p where a.AgreementNumber = '93-102338' and p.FhtProjectNumber = 'FHT-2021-04037'
union
select a.AgreementID, p.ProjectID from dbo.Agreement a, dbo.Project p where a.AgreementNumber = '3003' and p.FhtProjectNumber = 'FHT-2020-00046'
union
select a.AgreementID, p.ProjectID from dbo.Agreement a, dbo.Project p where a.AgreementNumber = '93-103352' and p.FhtProjectNumber = 'FHT-2021-02193'
union
select a.AgreementID, p.ProjectID from dbo.Agreement a, dbo.Project p where a.AgreementNumber = '93-103043' and p.FhtProjectNumber = 'FHT-2020-00009'
