ALTER TABLE dbo.Project DROP CONSTRAINT CK_Project_ApprovalStartDateLessThanEqualToCompletionDate
GO

ALTER TABLE dbo.Project DROP CONSTRAINT CK_Project_PlannedDateLessThanEqualToImplementationDate
GO

ALTER TABLE dbo.Project DROP COLUMN ApprovalStartDate
ALTER TABLE dbo.Project ADD ExpirationDate datetime null

ALTER TABLE dbo.ProjectUpdate DROP COLUMN ApprovalStartDate
ALTER TABLE dbo.ProjectUpdate ADD ExpirationDate datetime null

ALTER TABLE dbo.TreatmentActivity ADD TreatmentActivitySlashAcres decimal(18,0) not null

insert into dbo.TreatmentActivityStatus (TreatmentActivityStatusID, TreatmentActivityStatusName, TreatmentActivityStatusDisplayName)
values (3, 'Cancelled','Cancelled')

go

delete from dbo.FocusArea
go

insert dbo.FocusAreaStatus (FocusAreaStatusID, FocusAreaStatusName, FocusAreaStatusDisplayName) 
values 
(1, 'Planned', 'Planned'),
(2, 'In Progress', 'In Progress'),
(3, 'Completed', 'Completed')
go

insert into dbo.FocusArea(FocusAreaName, FocusAreaStatusID, RegionID)
values
('Quartzite - 2018', 2, 7520),
('Hunters - 2018', 2, 7520),
('North Highway 25 - 2018', 2, 7520),
('Antoine - 2018', 2, 7520),
('North Onion - 2018', 2, 7520),
('Tacoma Creek - 2018', 2, 7520),
('Diamond Lake - 2015', 2, 7520),
('Pingston - 2015', 2, 7520),
('Flat-Sheep - 2015', 2, 7520),
('Cayuse - 2015', 2, 7520),
('Gulches - 2014', 2, 7520),
('Winthrop - 2014', 2, 7520),
('Bear Lake - 2014', 2, 7520),
('Four Mound - 2014', 2, 7520),
('Tum Tum - 2014', 2, 7520),
('Chiliwist - 2013', 3, 7520),
('Forker Road - 2013', 3, 7520),
('Hwy 195 - 2013', 3, 7520),
('Upper Kettle - 2016', 2, 7520),
('Clear Lake - 2016', 2, 7520),
('Wauconda - 2016', 2, 7520),
('Barstow - 2016', 2, 7520),
('Horseshoe - 2017', 2, 7520),
('Sawtooth - 2017', 2, 7520),
('Metaline - 2017', 2, 7520),
('Huckleberry - 2017', 2, 7520),
('Sitzmark - 2017', 2, 7520),
('Valleyford - 2017', 2, 7520),
('Deer Creek - 2017', 2, 7520),
('Chiwawa - 2016', 2, 7519),
('Blue Mountain - 2015', 2, 7519),
('Blues-Mill Creek - 2018', 2, 7519),
('Chumstick - 2015', 2, 7519),
('Chumstick - 2018', 2, 7519),
('Douglas - 2017', 2, 7519),
('East Box Canyon - 2016', 2, 7519),
('East Chelan - 2013-005', 3, 7519),
('East Chelan - 2013-706', 3, 7519),
('East Klicikitat - 2017', 2, 7519),
('Hells Canyon - 2018', 2, 7519),
('Klickitat - 2013-005', 3, 7519),
('Klickitat - 2013-706', 3, 7519),
('Klickitat River - 2018', 2, 7519),
('Lake Chelan - 2018', 2, 7519),
('Leavenworth - 2016', 2, 7519),
('North Yakima - 2015', 2, 7519),
('North Yakima - 2016', 2, 7519),
('Upper Kittitas - 2013', 3, 7519),
('Upper Kittitas - 2014', 2, 7519),
('Upper Kittitas - 2016', 2, 7519),
('Upper Kittitas - 2017', 2, 7519),
('Upper Kittitas - 2018', 2, 7519),
('West Blues - 2017', 2, 7519),
('West Klickitat - 2014', 2, 7519),
('West Klickitat - 2016', 2, 7519),
('West Klickitat - 2017', 2, 7519),
('West Klickitat - 2018', 2, 7519)


go

insert into dbo.Project(
	ProjectName, 
	ProjectTypeID, 
	FocusAreaID, 
	PrimaryContactPersonID, 
	ProjectStageID, 
	PlannedDate,  
	ExpirationDate, 
	CompletionDate, 
	ProjectDescription, 
	EstimatedTotalCost, 
	ProjectApprovalStatusID, 
	ProjectLocationSimpleTypeID, 
	IsFeatured,
	ProposingPersonID,
	ProposingDate)
values
('Massie, John',2220,3,5256,1,NULL, NULL, NULL, '',4635,2,3,0,5256,CAST(N'2010-01-01T00:00:00.000' AS DateTime)),
('Millican, Ethan',2220,3,5256,1,NULL, NULL, NULL, '',4606,2,3,0,5256,CAST(N'2010-01-01T00:00:00.000' AS DateTime)),
('Reinbold Family LLC',2220,3,5256,3,CAST(N'2018-12-20T00:00:00.000' AS DateTime), NULL, NULL, '',39199.5,3,3,0,NULL,NULL),
('Smith, Michael',2220,3,5256,1,NULL, NULL, NULL, '',5581,2,3,0,5256,CAST(N'2010-01-01T00:00:00.000' AS DateTime)),
('Hahnlen, Tim',2220,5,5256,1,NULL, NULL, NULL, '',5778.5,2,3,0,5256,CAST(N'2010-01-01T00:00:00.000' AS DateTime)),
('Olson, Ricky',2220,5,5256,1,NULL, NULL, NULL, '',864,2,3,0,5256,CAST(N'2010-01-01T00:00:00.000' AS DateTime)),
('Schoonover, Julie',2220,5,5256,1,NULL, NULL, NULL, '',2822,2,3,0,5256,CAST(N'2010-01-01T00:00:00.000' AS DateTime)),
('Soroka, Walter',2220,5,5256,1,NULL, NULL, NULL, '',936,2,3,0,5256,CAST(N'2010-01-01T00:00:00.000' AS DateTime)),
('Wolohan, Matt',2220,5,5256,1,NULL, NULL, NULL, '',7366.5,2,3,0,5256,CAST(N'2010-01-01T00:00:00.000' AS DateTime)),
('Allen, Ray',2220,7,5256,1,NULL, NULL, NULL, '',7812,2,3,0,5256,CAST(N'2010-01-01T00:00:00.000' AS DateTime)),
('Anderson, Roger',2220,7,5256,1,NULL, NULL, NULL, '',4355,2,3,0,5256,CAST(N'2010-01-01T00:00:00.000' AS DateTime)),
('Brady, Jack',2220,7,5256,1,NULL, NULL, NULL, '',1848,2,3,0,5256,CAST(N'2010-01-01T00:00:00.000' AS DateTime)),
('Brandt, Mark',2220,7,5256,1,NULL, NULL, NULL, '',6888,2,3,0,5256,CAST(N'2010-01-01T00:00:00.000' AS DateTime)),
('Bushebi, Maria',2220,7,5256,4,NULL, NULL, CAST(N'2018-08-03T00:00:00.000' AS DateTime), '',5720,3,3,0,NULL,NULL),
('Capehart, George',2220,7,5256,1,NULL, NULL, NULL, '',5040,2,3,0,5256,CAST(N'2010-01-01T00:00:00.000' AS DateTime)),
('Clevenger, Susan',2220,7,5256,1,NULL, NULL, NULL, '',1656,2,3,0,5256,CAST(N'2010-01-01T00:00:00.000' AS DateTime)),
('Duarte, Tony',2220,7,5256,1,NULL, NULL, NULL, '',5206,2,3,0,5256,CAST(N'2010-01-01T00:00:00.000' AS DateTime)),
('Dykema,Amber',2220,7,5256,1,NULL, NULL, NULL, '',3612,2,3,0,5256,CAST(N'2010-01-01T00:00:00.000' AS DateTime)),
('Funkhouser, Edward',2220,7,5256,1,NULL, NULL, NULL, '',6901,2,3,0,5256,CAST(N'2010-01-01T00:00:00.000' AS DateTime)),
('Gillespie, Curtis & Beth',2220,7,5256,1,NULL, NULL, NULL, '',0,2,3,0,5256,CAST(N'2010-01-01T00:00:00.000' AS DateTime)),
('Hanson, Charles',2220,7,5256,4,CAST(N'2016-10-07T00:00:00.000' AS DateTime), NULL, CAST(N'2017-11-02T00:00:00.000' AS DateTime), '',3528,3,3,0,NULL,NULL),
('Hedtke, Chuck',2220,7,5256,4,CAST(N'2016-10-27T00:00:00.000' AS DateTime), NULL, CAST(N'2017-09-27T00:00:00.000' AS DateTime), '',3024,3,3,0,NULL,NULL),
('Hedtke, Terri Ann',2220,7,5256,1,NULL, NULL, NULL, '',5760,2,3,0,5256,CAST(N'2010-01-01T00:00:00.000' AS DateTime)),
('Herrin, Dave',2220,7,5256,4,CAST(N'2017-05-15T00:00:00.000' AS DateTime), NULL, CAST(N'2017-11-02T00:00:00.000' AS DateTime), '',5731,3,3,0,NULL,NULL),
('Hess, Phil',2220,7,5256,1,NULL, NULL, NULL, '',1335,2,3,0,5256,CAST(N'2010-01-01T00:00:00.000' AS DateTime)),
('Hogan, Debbie',2220,7,5256,1,NULL, NULL, NULL, '',2808.5,2,3,0,5256,CAST(N'2010-01-01T00:00:00.000' AS DateTime)),
('Holloway, Rodger H',2220,7,5256,1,NULL, NULL, NULL, '',3696,2,3,0,5256,CAST(N'2010-01-01T00:00:00.000' AS DateTime)),
('Holloway, Roger',2220,7,5256,1,NULL, NULL, NULL, '',4452,2,3,0,5256,CAST(N'2010-01-01T00:00:00.000' AS DateTime)),
('Homer, Gabriele',2220,7,5256,1,NULL, NULL, NULL, '',2370.5,2,3,0,5256,CAST(N'2010-01-01T00:00:00.000' AS DateTime)),
('Irvine, Dennis',2220,7,5256,1,NULL, NULL, NULL, '',0,2,3,0,5256,CAST(N'2010-01-01T00:00:00.000' AS DateTime)),
('Jackson, Patricia',2220,7,5256,4,CAST(N'2016-08-17T00:00:00.000' AS DateTime), NULL, CAST(N'2016-09-21T00:00:00.000' AS DateTime), '',4872,3,3,0,NULL,NULL),
('Jacobs, Chris',2220,7,5256,1,NULL, NULL, NULL, '',2379,2,3,0,5256,CAST(N'2010-01-01T00:00:00.000' AS DateTime)),
('Jones, Terry',2220,7,5256,1,NULL, NULL, NULL, '',2975.5,2,3,0,5256,CAST(N'2010-01-01T00:00:00.000' AS DateTime)),
('Kardos, Ted',2220,7,5256,1,NULL, NULL, NULL, '',3948,2,3,0,5256,CAST(N'2010-01-01T00:00:00.000' AS DateTime)),
('Kelly, Steven',2220,7,5256,4,CAST(N'2016-10-24T00:00:00.000' AS DateTime), NULL, CAST(N'2017-11-02T00:00:00.000' AS DateTime), '',2603,3,3,0,NULL,NULL),
('Kemmer, Rebecca',2220,7,5256,1,NULL, NULL, NULL, '',2412,2,3,0,5256,CAST(N'2010-01-01T00:00:00.000' AS DateTime)),
('Kromann. Kenneth',2220,7,5256,4,CAST(N'2016-07-22T00:00:00.000' AS DateTime), NULL, CAST(N'2017-10-20T00:00:00.000' AS DateTime), '',5695,3,3,0,NULL,NULL),
('Martinez, Tony',2220,7,5256,1,NULL, NULL, NULL, '',1596,2,3,0,5256,CAST(N'2010-01-01T00:00:00.000' AS DateTime)),
('McCurdy, Harold',2220,7,5256,4,CAST(N'2016-10-07T00:00:00.000' AS DateTime), NULL, CAST(N'2017-10-10T00:00:00.000' AS DateTime), '',7056,3,3,0,NULL,NULL),
('McEntee, Roger',2220,7,5256,4,CAST(N'2017-05-15T00:00:00.000' AS DateTime), NULL, CAST(N'2017-10-20T00:00:00.000' AS DateTime), '',2740,3,3,0,NULL,NULL),
('Moore, Ronald',2220,7,5256,1,NULL, NULL, NULL, '',2286,2,3,0,5256,CAST(N'2010-01-01T00:00:00.000' AS DateTime)),
('Newlun, Neil',2220,7,5256,1,NULL, NULL, NULL, '',2534.5,2,3,0,5256,CAST(N'2010-01-01T00:00:00.000' AS DateTime)),
('Newman, Jack',2220,7,5256,1,NULL, NULL, NULL, '',5617,2,3,0,5256,CAST(N'2010-01-01T00:00:00.000' AS DateTime)),
('Noh, Mary',2220,7,5256,4,CAST(N'2016-10-03T00:00:00.000' AS DateTime), NULL, CAST(N'2017-10-10T00:00:00.000' AS DateTime), '',1438.5,3,3,0,NULL,NULL),
('Peacock, Kent',2220,7,5256,1,NULL, NULL, NULL, '',5796,2,3,0,5256,CAST(N'2010-01-01T00:00:00.000' AS DateTime)),
('Pend Oreille Co (Parks)',2220,7,5256,1,NULL, NULL, NULL, '',5220,2,3,0,5256,CAST(N'2010-01-01T00:00:00.000' AS DateTime)),
('Rittgarn, Tammy',2220,7,5256,1,NULL, NULL, NULL, '',2562,2,3,0,5256,CAST(N'2010-01-01T00:00:00.000' AS DateTime)),
('Sackman, Ernie',2220,7,5256,1,NULL, NULL, NULL, '',5307,2,3,0,5256,CAST(N'2010-01-01T00:00:00.000' AS DateTime)),
('Seaman, Donald',2220,7,5256,1,NULL, NULL, NULL, '',5617,2,3,0,5256,CAST(N'2010-01-01T00:00:00.000' AS DateTime)),
('Schult, Dennis',2220,7,5256,1,NULL, NULL, NULL, '',3294,2,3,0,5256,CAST(N'2010-01-01T00:00:00.000' AS DateTime)),
('Schumacher, Keith',2220,7,5256,1,NULL, NULL, NULL, '',2682,2,3,0,5256,CAST(N'2010-01-01T00:00:00.000' AS DateTime)),
('Sravasti, Abbey',2220,7,5256,1,NULL, NULL, NULL, '',4700,2,3,0,5256,CAST(N'2010-01-01T00:00:00.000' AS DateTime)),
('Syacsure, Dennis',2220,7,5256,1,NULL, NULL, NULL, '',0,2,3,0,5256,CAST(N'2010-01-01T00:00:00.000' AS DateTime)),
('Waddell, Joel',2220,7,5256,1,NULL, NULL, NULL, '',6836.5,2,3,0,5256,CAST(N'2010-01-01T00:00:00.000' AS DateTime)),
('Wetzel, Louise',2220,7,5256,4,CAST(N'2016-10-07T00:00:00.000' AS DateTime), NULL, CAST(N'2017-10-25T00:00:00.000' AS DateTime), '',1260,3,3,0,NULL,NULL),
('Whittom, Ronald',2220,7,5256,1,NULL, NULL, NULL, '',5880,2,3,0,5256,CAST(N'2010-01-01T00:00:00.000' AS DateTime)),
('Wright, Chris',2220,7,5256,1,NULL, NULL, NULL, '',6360,2,3,0,5256,CAST(N'2010-01-01T00:00:00.000' AS DateTime)),
('Blue Mountain RC&D',2219,31,5271,4,CAST(N'2015-04-06T00:00:00.000' AS DateTime), NULL, CAST(N'2017-06-30T00:00:00.000' AS DateTime), '',10000,3,3,0,NULL,NULL),
('Bloodline Acres, LLC',2220,31,5271,4,CAST(N'2017-01-17T00:00:00.000' AS DateTime), NULL, CAST(N'2017-12-11T00:00:00.000' AS DateTime), '',23800,3,3,0,NULL,NULL),
('Phinney, Robert',2220,31,5271,5,CAST(N'2016-10-20T00:00:00.000' AS DateTime), CAST(N'2018-10-31T00:00:00.000' AS DateTime), NULL, '',6350,3,3,0,NULL,NULL),
('Reaney, Mark',2220,31,5271,5,CAST(N'2015-10-02T00:00:00.000' AS DateTime), CAST(N'2017-10-31T00:00:00.000' AS DateTime), NULL, '',13800,3,3,0,NULL,NULL),
('Rasmussen, Ervin (Joe)',2220,31,5271,4,CAST(N'2015-10-13T00:00:00.000' AS DateTime), CAST(N'2017-10-31T00:00:00.000' AS DateTime), CAST(N'2017-10-01T00:00:00.000' AS DateTime), '',1480,3,3,0,NULL,NULL),
('Suffield, David',2220,31,5271,5,CAST(N'2015-12-07T00:00:00.000' AS DateTime), CAST(N'2016-04-05T00:00:00.000' AS DateTime), NULL, '',4840,3,3,0,NULL,NULL),
('Zehner, Charles',2220,31,5271,5,CAST(N'2016-10-25T00:00:00.000' AS DateTime), CAST(N'2018-10-31T00:00:00.000' AS DateTime), NULL, '',2875,3,3,0,NULL,NULL),
('Blout, James/Nancy',2220,31,5271,3,CAST(N'2017-07-25T00:00:00.000' AS DateTime), CAST(N'2019-07-31T00:00:00.000' AS DateTime), NULL, '',1905,3,3,0,NULL,NULL),
('Peltonen, Kyle/Michele',2220,33,5271,4,CAST(N'2017-06-01T00:00:00.000' AS DateTime), CAST(N'2019-06-30T00:00:00.000' AS DateTime), CAST(N'2017-10-31T00:00:00.000' AS DateTime), '',22400,3,3,0,NULL,NULL),
('Kirschner, Michael',2220,33,5271,5,CAST(N'2017-05-10T00:00:00.000' AS DateTime), NULL, NULL, '',3200,3,3,0,NULL,NULL),
('Timberline Silvics',2220,33,5271,4,CAST(N'2017-10-16T00:00:00.000' AS DateTime), CAST(N'2018-06-30T00:00:00.000' AS DateTime), CAST(N'2017-12-20T00:00:00.000' AS DateTime), '',48680,3,3,0,NULL,NULL),
('Turner, Adam',2220,33,5271,3,CAST(N'2017-10-19T00:00:00.000' AS DateTime), CAST(N'2019-10-31T00:00:00.000' AS DateTime), NULL, '',12650,3,3,0,NULL,NULL),
('Kostka, Peter',2220,33,5271,3,CAST(N'2017-11-14T00:00:00.000' AS DateTime), CAST(N'2019-11-30T00:00:00.000' AS DateTime), NULL, '',1725,3,3,0,NULL,NULL),
('Chumstick Wildfire Stewardship Coalition',2220,33,5271,5,CAST(N'2016-05-26T00:00:00.000' AS DateTime), CAST(N'2017-10-31T00:00:00.000' AS DateTime), NULL, '',10000,3,3,0,NULL,NULL),
('Arakelian, Raffi',2220,36,5271,3,CAST(N'2018-10-17T00:00:00.000' AS DateTime), CAST(N'2020-10-31T00:00:00.000' AS DateTime), NULL, '',2067.5,3,3,0,NULL,NULL),
('Beals, Patricia',2220,36,5271,3,CAST(N'2017-04-17T00:00:00.000' AS DateTime), CAST(N'2019-04-30T00:00:00.000' AS DateTime), NULL, '',4387.5,3,3,0,NULL,NULL),
('Binning, Brian',2220,36,5271,3,CAST(N'2018-04-16T00:00:00.000' AS DateTime), CAST(N'2020-04-30T00:00:00.000' AS DateTime), NULL, '',3995,3,3,0,NULL,NULL),
('Blandine, Victor',2220,36,5271,3,CAST(N'2018-12-19T00:00:00.000' AS DateTime), CAST(N'2020-12-31T00:00:00.000' AS DateTime), NULL, '',1150,3,3,0,NULL,NULL),
('Boswell, Simon/Julie',2220,36,5271,3,CAST(N'2017-11-17T00:00:00.000' AS DateTime), CAST(N'2019-11-30T00:00:00.000' AS DateTime), NULL, '',2732.5,3,3,0,NULL,NULL),
('Boykin-Hicks, Carol',2220,36,5271,3,CAST(N'2018-10-23T00:00:00.000' AS DateTime), CAST(N'2020-10-31T00:00:00.000' AS DateTime), NULL, '',1725,3,3,0,NULL,NULL),
('Buchanan, Alex',2220,36,5271,3,CAST(N'2018-06-01T00:00:00.000' AS DateTime), CAST(N'2020-06-30T00:00:00.000' AS DateTime), NULL, '',2930,3,3,0,NULL,NULL),
('Cozad, Scott',2220,36,5271,3,CAST(N'2017-11-17T00:00:00.000' AS DateTime), CAST(N'2019-11-30T00:00:00.000' AS DateTime), NULL, '',3710,3,3,0,NULL,NULL),
('Crain, Marc/Kim',2220,36,5271,3,CAST(N'2017-04-19T00:00:00.000' AS DateTime), CAST(N'2019-04-30T00:00:00.000' AS DateTime), NULL, '',2930,3,3,0,NULL,NULL),
('Craven, Randall/Sharon',2220,36,5271,3,CAST(N'2017-08-09T00:00:00.000' AS DateTime), CAST(N'2019-08-31T00:00:00.000' AS DateTime), NULL, '',2875,3,3,0,NULL,NULL),
('Doak, Thomas',2220,36,5271,3,CAST(N'2018-10-17T00:00:00.000' AS DateTime), CAST(N'2020-10-31T00:00:00.000' AS DateTime), NULL, '',3165,3,3,0,NULL,NULL),
('Gardner, Jeff/Ethelene',2220,36,5271,3,CAST(N'2017-11-07T00:00:00.000' AS DateTime), CAST(N'2019-11-30T00:00:00.000' AS DateTime), NULL, '',6145,3,3,0,NULL,NULL),
('Heilman, Jerome',2220,36,5271,3,CAST(N'2018-12-17T00:00:00.000' AS DateTime), CAST(N'2020-12-31T00:00:00.000' AS DateTime), NULL, '',6675,3,3,0,NULL,NULL),
('Honkala, Jack',2220,36,5271,3,CAST(N'2018-02-14T00:00:00.000' AS DateTime), CAST(N'2020-02-29T00:00:00.000' AS DateTime), NULL, '',3570,3,3,0,NULL,NULL),
('Ireland, Tom',2220,36,5271,3,CAST(N'2018-06-27T00:00:00.000' AS DateTime), CAST(N'2020-06-30T00:00:00.000' AS DateTime), NULL, '',3620,3,3,0,NULL,NULL),
('Johnson, Rick/Cindy',2220,36,5271,3,CAST(N'2018-11-30T00:00:00.000' AS DateTime), CAST(N'2020-01-31T00:00:00.000' AS DateTime), NULL, '',5520,3,3,0,NULL,NULL),
('KCC-Peg Swift',2220,36,5271,3,CAST(N'2017-11-07T00:00:00.000' AS DateTime), CAST(N'2019-11-30T00:00:00.000' AS DateTime), NULL, '',3680,3,3,0,NULL,NULL),
('Kinley, Ron',2220,36,5271,3,CAST(N'2018-05-14T00:00:00.000' AS DateTime), CAST(N'2020-05-31T00:00:00.000' AS DateTime), NULL, '',2242.5,3,3,0,NULL,NULL),
('Kinnunen, Dennis/Judith',2220,36,5271,3,CAST(N'2017-08-18T00:00:00.000' AS DateTime), CAST(N'2019-08-31T00:00:00.000' AS DateTime), NULL, '',4030,3,3,0,NULL,NULL),
('Kinsella, Ron',2220,36,5271,4,CAST(N'2017-10-27T00:00:00.000' AS DateTime), CAST(N'2019-10-31T00:00:00.000' AS DateTime), CAST(N'2018-06-18T00:00:00.000' AS DateTime), '',1087.5,3,3,0,NULL,NULL),
('Kobetich, Dean/Cindy',2220,36,5271,3,CAST(N'2017-09-11T00:00:00.000' AS DateTime), CAST(N'2019-09-30T00:00:00.000' AS DateTime), NULL, '',3257.5,3,3,0,NULL,NULL),
('Lawton, Jeffrey',2220,36,5271,3,CAST(N'2018-04-16T00:00:00.000' AS DateTime), CAST(N'2020-04-30T00:00:00.000' AS DateTime), NULL, '',2125,3,3,0,NULL,NULL),
('Lennon, Cheryl/Dan',2220,36,5271,3,CAST(N'2017-11-20T00:00:00.000' AS DateTime), CAST(N'2019-11-30T00:00:00.000' AS DateTime), NULL, '',3260,3,3,0,NULL,NULL),
('Matosich, Gerald/Barbara',2220,36,5271,3,CAST(N'2017-09-11T00:00:00.000' AS DateTime), CAST(N'2019-09-30T00:00:00.000' AS DateTime), NULL, '',977.5,3,3,0,NULL,NULL),
('McBride, Mike/Linda',2220,36,5271,3,CAST(N'2018-12-19T00:00:00.000' AS DateTime), CAST(N'2020-12-31T00:00:00.000' AS DateTime), NULL, '',1725,3,3,0,NULL,NULL),
('Mehner,Miles/Shayna',2220,36,5271,3,CAST(N'2018-02-08T00:00:00.000' AS DateTime), CAST(N'2020-02-29T00:00:00.000' AS DateTime), NULL, '',3655,3,3,0,NULL,NULL),
('Mehrabi, Brandon',2220,36,5271,3,CAST(N'2019-01-14T00:00:00.000' AS DateTime), CAST(N'2021-01-31T00:00:00.000' AS DateTime), NULL, '',6440,3,3,0,NULL,NULL),
('Payne, Michael/Joan',2220,36,5271,3,CAST(N'2018-01-30T00:00:00.000' AS DateTime), CAST(N'2020-01-31T00:00:00.000' AS DateTime), NULL, '',1163.75,3,3,0,NULL,NULL),
('Piper, Justin/Lori',2220,36,5271,3,CAST(N'2018-05-21T00:00:00.000' AS DateTime), CAST(N'2020-05-31T00:00:00.000' AS DateTime), NULL, '',8077.5,3,3,0,NULL,NULL),
('Ponderosa Park HOA',2220,36,5271,3,CAST(N'2018-10-17T00:00:00.000' AS DateTime), CAST(N'2020-10-31T00:00:00.000' AS DateTime), NULL, '',13621.25,3,3,0,NULL,NULL),
('Sacred Earth Foundation',2220,36,5271,3,CAST(N'2018-06-13T00:00:00.000' AS DateTime), CAST(N'2020-06-30T00:00:00.000' AS DateTime), NULL, '',6710,3,3,0,NULL,NULL),
('Sager/Vachon',2220,36,5271,3,CAST(N'2018-04-09T00:00:00.000' AS DateTime), CAST(N'2020-04-30T00:00:00.000' AS DateTime), NULL, '',2867.5,3,3,0,NULL,NULL),
('Simpson, Marshall',2220,36,5271,3,CAST(N'2018-05-23T00:00:00.000' AS DateTime), CAST(N'2020-05-31T00:00:00.000' AS DateTime), NULL, '',2930,3,3,0,NULL,NULL),
('Skinner, Michael/Amber',2220,36,5271,3,CAST(N'2018-09-10T00:00:00.000' AS DateTime), CAST(N'2020-09-30T00:00:00.000' AS DateTime), NULL, '',3910,3,3,0,NULL,NULL),
('Slawson, Rick',2220,36,5271,3,CAST(N'2017-10-13T00:00:00.000' AS DateTime), CAST(N'2019-10-31T00:00:00.000' AS DateTime), NULL, '',13812.5,3,3,0,NULL,NULL),
('Southworth, Tim/Laurie',2220,36,5271,3,CAST(N'2017-08-18T00:00:00.000' AS DateTime), CAST(N'2019-08-31T00:00:00.000' AS DateTime), NULL, '',6711.5,3,3,0,NULL,NULL),
('Toomey, Christie',2220,36,5271,3,CAST(N'2018-06-11T00:00:00.000' AS DateTime), CAST(N'2020-06-30T00:00:00.000' AS DateTime), NULL, '',1810,3,3,0,NULL,NULL),
('Van Antwerp, Bill',2220,36,5271,3,CAST(N'2018-04-30T00:00:00.000' AS DateTime), CAST(N'2020-04-30T00:00:00.000' AS DateTime), NULL, '',3595,3,3,0,NULL,NULL),
('Walcott, Patty',2220,36,5271,3,CAST(N'2018-06-21T00:00:00.000' AS DateTime), CAST(N'2020-06-30T00:00:00.000' AS DateTime), NULL, '',3560,3,3,0,NULL,NULL),
('Watson, Greg/Michelle',2220,36,5271,3,CAST(N'2018-02-02T00:00:00.000' AS DateTime), CAST(N'2020-02-29T00:00:00.000' AS DateTime), NULL, '',2615,3,3,0,NULL,NULL),
('Wedding, Tim',2220,36,5271,3,CAST(N'2018-11-16T00:00:00.000' AS DateTime), CAST(N'2020-11-30T00:00:00.000' AS DateTime), NULL, '',5145,3,3,0,NULL,NULL),
('Bryan, Russell',2220,37,5271,4,CAST(N'2014-11-15T00:00:00.000' AS DateTime), CAST(N'2015-11-30T00:00:00.000' AS DateTime), CAST(N'2015-11-30T00:00:00.000' AS DateTime), '',4175,3,3,0,NULL,NULL),
('Bryan, Russell',2220,37,5271,4,CAST(N'2016-06-10T00:00:00.000' AS DateTime), CAST(N'2016-08-01T00:00:00.000' AS DateTime), CAST(N'2016-08-01T00:00:00.000' AS DateTime), '',1600,3,3,0,NULL,NULL),
('Forest Ridge Wildfire Coalition',2220,37,5271,4,CAST(N'2016-09-20T00:00:00.000' AS DateTime), CAST(N'2017-12-31T00:00:00.000' AS DateTime), CAST(N'2017-07-11T00:00:00.000' AS DateTime), '',7155.82,3,3,0,NULL,NULL),
('Karpoff, Jon',2220,37,5271,4,CAST(N'2014-11-18T00:00:00.000' AS DateTime), CAST(N'2015-05-12T00:00:00.000' AS DateTime), CAST(N'2015-05-12T00:00:00.000' AS DateTime), '',2925,3,3,0,NULL,NULL),
('Mazie, Marjorie',2220,37,5271,4,CAST(N'2015-05-01T00:00:00.000' AS DateTime), CAST(N'2015-10-22T00:00:00.000' AS DateTime), CAST(N'2015-10-22T00:00:00.000' AS DateTime), '',7065,3,3,0,NULL,NULL),
('Schrenk, Larry',2220,37,5271,4,CAST(N'2015-05-14T00:00:00.000' AS DateTime), CAST(N'2015-10-27T00:00:00.000' AS DateTime), CAST(N'2015-10-27T00:00:00.000' AS DateTime), '',18125,3,3,0,NULL,NULL),
('Whalen, Jim',2220,37,5271,4,CAST(N'2015-05-14T00:00:00.000' AS DateTime), CAST(N'2015-12-31T00:00:00.000' AS DateTime), CAST(N'2015-12-31T00:00:00.000' AS DateTime), '',14175,3,3,0,NULL,NULL),
('Rudolph, Dennis',2220,37,5271,5,CAST(N'2018-05-14T00:00:00.000' AS DateTime), CAST(N'2018-08-08T00:00:00.000' AS DateTime), NULL, '',13800,3,3,0,NULL,NULL),
('CCA-IAA93-096393',2220,37,5271,4,CAST(N'2017-10-23T00:00:00.000' AS DateTime), CAST(N'2018-08-08T00:00:00.000' AS DateTime), CAST(N'2018-06-30T00:00:00.000' AS DateTime), '',18654.6,3,3,0,NULL,NULL)

go

insert into dbo.AuditLog(
	PersonID, 
	AuditLogDate, 
	AuditLogEventTypeID, 
	TableName, 
	RecordID, 
	ColumnName, 
	OriginalValue, 
	NewValue, 
	AuditDescription, 
	ProjectID)
select
    5250,
    getdate(),
	1,
	'Project',
	p.ProjectID,
	'ProjectID',
	null,
	p.ProjectID,
    'Project imported',
	p.ProjectID
from
    dbo.Project p

go

insert into dbo.ProjectOrganization(
	ProjectID, 
	OrganizationID, 
	RelationshipTypeID)
select
	p.ProjectID,
	4704,
	33
from
    dbo.Project p

go

insert into dbo.ProjectRegion(
	ProjectID,
	RegionID)
select
	p.ProjectID,
	fa.RegionID
from
    dbo.Project p
	join dbo.FocusArea fa on p.FocusAreaID = fa.FocusAreaID

go

update dbo.Project
set PrimaryContactPersonID = 5250
where PrimaryContactPersonID is null

go

insert into dbo.ProjectPerson(
	ProjectID,
	PersonID,
	ProjectPersonRelationshipTypeID)
select
	p.ProjectID,
	p.PrimaryContactPersonID,
	1
from
    dbo.Project p


go

insert into dbo.TreatmentActivity(
	ProjectID, 
	TreatmentActivityStatusID, 
	TreatmentActivityStartDate, 
	TreatmentActivityEndDate, 
	TreatmentActivityFootprintAcres, 
	TreatmentActivityChippingAcres, 
	TreatmentActivityPruningAcres, 
	TreatmentActivityThinningAcres, 
	TreatmentActivitySlashAcres,
	TreatmentActivityMasticationAcres, 
	TreatmentActivityGrazingAcres, 
	TreatmentActivityLopAndScatterAcres, 
	TreatmentActivityBiomassRemovalAcres, 
	TreatmentActivityHandPileAcres, 
	TreatmentActivityBroadcastBurnAcres, 
	TreatmentActivityHandPileBurnAcres, 
	TreatmentActivityMachinePileBurnAcres, 
	TreatmentActivityOtherTreatmentAcres, 
	TreatmentActivityNotes 
	)
values
(12495,1, NULL, NULL, 7.3,0,7.3,7.3,7.3,0,0,0,0,0,0,0,0,0,''),
(12496,1, NULL, NULL, 9.4,0,9.4,9.4,9.4,0,0,0,0,0,0,0,0,0,''),
(12497,1, CAST(N'2018-12-20T00:00:00.000' AS DateTime), NULL, 54.5,0,54.5,54.5,54.5,0,0,0,0,0,0,0,0,0,''),
(12498,1, NULL, NULL, 6.1,0,6.1,6.1,6.1,0,0,0,0,0,0,0,0,0,''),
(12499,1, NULL, NULL, 9.1,0,9.1,9.1,9.1,0,0,0,0,0,0,0,0,0,''),
(12500,1, NULL, NULL, 1.2,0,1.2,1.2,1.2,0,0,0,0,0,0,0,0,0,''),
(12501,1, NULL, NULL, 3.4,0,3.4,3.4,3.4,0,0,0,0,0,0,0,0,0,''),
(12502,1, NULL, NULL, 1.3,0,1.3,1.3,1.3,0,0,0,0,0,0,0,0,0,''),
(12503,1, NULL, NULL, 11.3,0,11.3,11.3,11.3,0,0,0,0,0,0,0,0,0,''),
(12504,1, NULL, NULL, 9.3,0,9.3,9.3,9.3,0,0,0,0,0,0,0,0,0,''),
(12505,1, NULL, NULL, 6.5,0,0,6.5,6.5,0,0,0,0,0,0,0,0,0,''),
(12506,1, NULL, NULL, 2.2,0,2.2,2.2,2.2,0,0,0,0,0,0,0,0,0,''),
(12507,1, NULL, NULL, 8.2,0,8.2,8.2,8.2,0,0,0,0,0,0,0,0,0,''),
(12508,2, NULL, CAST(N'2018-08-03T00:00:00.000' AS DateTime), 11,0,11,11,11,0,0,0,0,0,0,0,0,0,''),
(12509,1, NULL, NULL, 6,0,6,6,6,0,0,0,0,0,0,0,0,0,''),
(12510,1, NULL, NULL, 2.3,0,2.3,2.3,2.3,0,0,0,0,0,0,0,0,0,''),
(12511,1, NULL, NULL, 7.6,0,7.6,7.6,7.6,0,0,0,0,0,0,0,0,0,''),
(12512,1, NULL, NULL, 4.3,0,4.3,4.3,4.3,0,0,0,0,0,0,0,0,0,''),
(12513,1, NULL, NULL, 10.3,0,0,10.3,10.3,0,0,0,0,0,0,0,0,0,''),
(12514,1, NULL, NULL, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,'Moved to 99C'),
(12515,2, CAST(N'2016-10-07T00:00:00.000' AS DateTime), CAST(N'2017-11-02T00:00:00.000' AS DateTime), 4.2,0,4.2,4.2,4.2,0,0,0,0,0,0,0,0,0,'Larson Forestry Inv# 289'),
(12516,2, CAST(N'2016-10-27T00:00:00.000' AS DateTime), CAST(N'2017-09-27T00:00:00.000' AS DateTime), 3.6,0,3.6,3.6,3.6,0,0,0,0,0,0,0,0,0,'NWM Inc.'),
(12517,1, NULL, NULL, 9.6,0,9.6,9.6,9.6,0,0,0,0,0,0,0,0,0,''),
(12518,2, CAST(N'2017-05-15T00:00:00.000' AS DateTime), CAST(N'2017-11-02T00:00:00.000' AS DateTime), 8.3,0,1,8.3,8.3,0,0,0,0,0,0,0,0,0,'Larson Forestry Inv# 288'),
(12519,1, NULL, NULL, 3,0,0,3,3,0,0,0,0,0,0,0,0,0,''),
(12520,1, NULL, NULL, 4.1,0,4.1,4.1,4.1,0,0,0,0,0,0,0,0,0,''),
(12521,1, NULL, NULL, 4.4,0,4.4,4.4,4.4,0,0,0,0,0,0,0,0,0,''),
(12522,1, NULL, NULL, 5.3,0,5.3,5.3,5.3,0,0,0,0,0,0,0,0,0,''),
(12523,1, NULL, NULL, 3.2,0,3.2,3.2,3.2,0,0,0,0,0,0,0,0,0,''),
(12524,1, NULL, NULL, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,'Moved to 908'),
(12525,2, CAST(N'2016-08-17T00:00:00.000' AS DateTime), CAST(N'2016-09-21T00:00:00.000' AS DateTime), 5.8,0,5.8,5.8,5.8,0,0,0,0,0,0,0,0,0,'Majestic View Invoice #1311'),
(12526,1, NULL, NULL, 2.6,0,2.6,2.6,2.6,0,0,0,0,0,0,0,0,0,''),
(12527,1, NULL, NULL, 8.5,0,0,8.5,8.5,0,0,0,0,0,0,0,0,0,''),
(12528,1, NULL, NULL, 4.7,0,4.7,4.7,4.7,0,0,0,0,0,0,0,0,0,''),
(12529,2, CAST(N'2016-10-24T00:00:00.000' AS DateTime), CAST(N'2017-11-02T00:00:00.000' AS DateTime), 3.8,0,3.8,3.8,3.8,0,0,0,0,0,0,0,0,0,'Larson Forestry Inv# 291'),
(12530,1, NULL, NULL, 3.6,0,0,3.6,3.6,0,0,0,0,0,0,0,0,0,''),
(12531,2, CAST(N'2016-07-22T00:00:00.000' AS DateTime), CAST(N'2017-10-20T00:00:00.000' AS DateTime), 8.5,0,0,8.5,8.5,0,0,0,0,0,0,0,0,0,'Larson Forestry Inv# 287'),
(12532,1, NULL, NULL, 1.9,0,1.9,1.9,1.9,0,0,0,0,0,0,0,0,0,''),
(12533,2, CAST(N'2016-10-07T00:00:00.000' AS DateTime), CAST(N'2017-10-10T00:00:00.000' AS DateTime), 8.4,0,8.4,8.4,8.4,0,0,0,0,0,0,0,0,0,'Larson Forestry Inv# 284'),
(12534,2, CAST(N'2017-05-15T00:00:00.000' AS DateTime), CAST(N'2017-10-20T00:00:00.000' AS DateTime), 4,0,4,4,4,0,0,0,0,0,0,0,0,0,'Larson Forestry Inv# 286'),
(12535,1, NULL, NULL, 3.6,0,3.6,3.6,3.6,0,0,0,0,0,0,0,0,0,''),
(12536,1, NULL, NULL, 3.7,0,3.7,3.7,3.7,0,0,0,0,0,0,0,0,0,''),
(12537,1, NULL, NULL, 8.2,0,8.2,8.2,8.2,0,0,0,0,0,0,0,0,0,''),
(12538,2, CAST(N'2016-10-03T00:00:00.000' AS DateTime), CAST(N'2017-10-10T00:00:00.000' AS DateTime), 2.1,0,2.1,2.1,2.1,0,0,0,0,0,0,0,0,0,'Larson Forestry Inv# 282'),
(12539,1, NULL, NULL, 6.9,0,6.9,6.9,6.9,0,0,0,0,0,0,6.9,0,0,''),
(12540,1, NULL, NULL, 12,0,12,12,12,0,0,0,0,0,0,0,0,0,''),
(12541,1, NULL, NULL, 2.8,0,2.8,2.8,2.8,0,0,0,0,0,0,0,0,0,''),
(12542,1, NULL, NULL, 5.8,0,5.8,5.8,5.8,0,0,0,0,0,0,0,0,0,''),
(12543,1, NULL, NULL, 8.2,0,8.2,8.2,8.2,0,0,0,0,0,0,0,0,0,''),
(12544,1, NULL, NULL, 3.6,0,3.6,3.6,3.6,0,0,0,0,0,0,0,0,0,''),
(12545,1, NULL, NULL, 3.6,0,0,3.6,3.6,0,0,0,0,0,0,0,0,0,''),
(12546,1, NULL, NULL, 7.6,0,7.6,7.6,7.6,0,0,0,0,0,0,0,0,0,''),
(12547,1, NULL, NULL, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,'Moved to 908'),
(12548,1, NULL, NULL, 11.3,0,11.3,11.3,11.3,0,0,0,0,0,0,0,0,0,''),
(12549,2, CAST(N'2016-10-07T00:00:00.000' AS DateTime), CAST(N'2017-10-25T00:00:00.000' AS DateTime), 1.5,0,1.5,1.5,1.5,0,0,0,0,0,0,0,0,0,''),
(12550,1, NULL, NULL, 7,0,7,7,7,0,0,0,0,0,0,0,0,0,''),
(12551,1, NULL, NULL, 8,0,8,8,8,0,0,0,0,0,0,0,0,0,''),
(12553,2, CAST(N'2017-01-17T00:00:00.000' AS DateTime), CAST(N'2017-12-11T00:00:00.000' AS DateTime), 8,0,8,8,8,0,0,0,0,0,0,8,0,0,''),
(12554,3, CAST(N'2016-10-20T00:00:00.000' AS DateTime), NULL, 10,0,10,10,10,0,0,0,0,0,0,0,0,0,''),
(12555,3, CAST(N'2015-10-02T00:00:00.000' AS DateTime), NULL, 20,0,20,20,20,0,0,0,0,0,0,0,0,0,''),
(12556,2, CAST(N'2015-10-13T00:00:00.000' AS DateTime), CAST(N'2017-10-01T00:00:00.000' AS DateTime), 4,0,2,2,2,0,0,0,0,0,0,2,0,0,''),
(12557,3, CAST(N'2015-12-07T00:00:00.000' AS DateTime), NULL, 8,0,3,3,3,0,0,0,0,0,0,3,0,0,''),
(12558,3, CAST(N'2016-10-25T00:00:00.000' AS DateTime), NULL, 5,0,5,5,5,0,0,0,0,0,0,0,0,0,''),
(12559,1, CAST(N'2017-07-25T00:00:00.000' AS DateTime), CAST(N'2019-07-31T00:00:00.000' AS DateTime), 3,0,3,3,3,0,0,0,0,0,0,0,0,0,''),
(12560,2, CAST(N'2017-06-01T00:00:00.000' AS DateTime), CAST(N'2017-10-31T00:00:00.000' AS DateTime), 28,0,28,28,28,0,0,0,0,0,0,28,0,0,''),
(12561,3, CAST(N'2017-05-10T00:00:00.000' AS DateTime), NULL, 4,0,4,4,4,0,0,0,0,0,0,0,0,0,''),
(12562,2, CAST(N'2017-10-16T00:00:00.000' AS DateTime), CAST(N'2017-12-20T00:00:00.000' AS DateTime), 38,0,38,38,38,0,0,0,0,0,0,38,0,0,''),
(12563,1, CAST(N'2017-10-19T00:00:00.000' AS DateTime), NULL, 22,0,22,22,22,0,0,0,0,0,0,0,0,0,''),
(12564,1, CAST(N'2017-11-14T00:00:00.000' AS DateTime), NULL, 3,0,3,3,3,0,0,0,0,0,0,0,0,0,''),
(12565,3, CAST(N'2016-05-26T00:00:00.000' AS DateTime), NULL, 50,0,0,0,0,0,0,0,0,0,0,0,0,0,''),
(12566,1, CAST(N'2018-10-17T00:00:00.000' AS DateTime), NULL, 3.5,0,3.5,3.5,3.5,0,0,0,0,0,0,0,0,0,''),
(12567,1, CAST(N'2017-04-17T00:00:00.000' AS DateTime), NULL, 7.5,0,7.5,7.5,7.5,0,0,0,0,0,0,0,0,0,''),
(12568,1, CAST(N'2018-04-16T00:00:00.000' AS DateTime), NULL, 6,0,6,6,6,0,0,0,0,0,0,0,0,0,''),
(12569,1, CAST(N'2018-12-19T00:00:00.000' AS DateTime), NULL, 2.5,0,2.5,0,2.5,0,0,0,0,0,0,0,0,0,''),
(12570,1, CAST(N'2017-11-17T00:00:00.000' AS DateTime), NULL, 4.5,0,4.5,4.5,4.5,0,0,0,0,0,0,0,0,0,''),
(12571,1, CAST(N'2018-10-23T00:00:00.000' AS DateTime), NULL, 3,0,3,3,3,0,0,0,0,0,0,0,0,0,''),
(12572,1, CAST(N'2018-06-01T00:00:00.000' AS DateTime), NULL, 5,0,5,5,5,0,0,0,0,0,0,0,0,0,''),
(12573,1, CAST(N'2017-11-17T00:00:00.000' AS DateTime), NULL, 5,0,5,5,5,0,0,0,0,0,0,0,0,0,''),
(12574,1, CAST(N'2017-04-19T00:00:00.000' AS DateTime), NULL, 5,0,5,5,5,0,0,0,0,0,0,0,0,0,''),
(12575,1, CAST(N'2017-08-09T00:00:00.000' AS DateTime), NULL, 5,0,5,5,5,0,0,0,0,0,0,0,0,0,''),
(12576,1, CAST(N'2018-10-17T00:00:00.000' AS DateTime), NULL, 6,0,6,6,6,0,0,0,0,0,0,0,0,0,''),
(12577,1, CAST(N'2017-11-07T00:00:00.000' AS DateTime), NULL, 10,0,10,10,10,0,0,0,0,0,0,0,0,0,''),
(12578,1, CAST(N'2018-12-17T00:00:00.000' AS DateTime), NULL, 10,0,10,10,10,0,0,0,0,0,0,0,0,0,''),
(12579,1, CAST(N'2018-02-14T00:00:00.000' AS DateTime), NULL, 3,0,3,3,6,0,0,0,0,0,0,0,0,0,''),
(12580,1, CAST(N'2018-06-27T00:00:00.000' AS DateTime), NULL, 5,0,5,5,5,0,0,0,0,0,0,0,0,0,''),
(12581,1, CAST(N'2018-11-30T00:00:00.000' AS DateTime), NULL, 7.5,0,7.5,7.5,8.5,0,0,0,0,0,0,0,0,0,''),
(12582,1, CAST(N'2017-11-07T00:00:00.000' AS DateTime), NULL, 5,0,5,5,5,0,0,0,0,0,0,0,0,0,''),
(12583,1, CAST(N'2018-05-14T00:00:00.000' AS DateTime), NULL, 3.25,0,3.25,3.25,3.25,0,0,0,0,0,0,0,0,0,''),
(12584,1, CAST(N'2017-08-18T00:00:00.000' AS DateTime), NULL, 5,0,5,5,5,0,0,0,0,0,0,0,0,0,''),
(12585,2, CAST(N'2017-10-27T00:00:00.000' AS DateTime), CAST(N'2018-06-18T00:00:00.000' AS DateTime), 2.5,0,2.5,2.5,2.5,0,0,0,0,0,0,2.5,0,0,''),
(12586,1, CAST(N'2017-09-11T00:00:00.000' AS DateTime), NULL, 5.5,0,5.5,5.5,5.5,0,0,0,0,0,0,0,0,0,''),
(12587,1, CAST(N'2018-04-16T00:00:00.000' AS DateTime), NULL, 3,0,3,3,3,0,0,0,0,0,0,0,0,0,''),
(12588,1, CAST(N'2017-11-20T00:00:00.000' AS DateTime), NULL, 5,0,5,5,5,0,0,0,0,0,0,0,0,0,''),
(12589,1, CAST(N'2017-09-11T00:00:00.000' AS DateTime), NULL, 1.5,0,1.5,1.5,1.5,0,0,0,0,0,0,0,0,0,''),
(12590,1, CAST(N'2018-12-19T00:00:00.000' AS DateTime), NULL, 3,0,3,3,3,0,0,0,0,0,0,0,0,0,''),
(12591,1, CAST(N'2018-02-08T00:00:00.000' AS DateTime), NULL, 5,0,5,5,5,0,0,0,0,0,0,0,0,0,''),
(12592,1, CAST(N'2019-01-14T00:00:00.000' AS DateTime), NULL, 10,0,10,10,10,0,0,0,0,0,0,0,0,0,''),
(12593,1, CAST(N'2018-01-30T00:00:00.000' AS DateTime), NULL, 2,0,2,2,2,0,0,0,0,0,0,0,0,0,''),
(12594,1, CAST(N'2018-05-21T00:00:00.000' AS DateTime), NULL, 9,0,9,9,9,0,0,0,0,0,0,0,0,0,''),
(12595,1, CAST(N'2018-10-17T00:00:00.000' AS DateTime), NULL, 22.5,0,20.5,22.5,22.5,0,0,0,0,0,0,0,0,0,''),
(12596,1, CAST(N'2018-06-13T00:00:00.000' AS DateTime), NULL, 10,0,10,10,10,0,0,0,0,0,0,0,0,0,''),
(12597,1, CAST(N'2018-04-09T00:00:00.000' AS DateTime), NULL, 4.5,0,4.5,4.5,4.5,0,0,0,0,0,0,0,0,0,''),
(12598,1, CAST(N'2018-05-23T00:00:00.000' AS DateTime), NULL, 5,0,5,5,5,0,0,0,0,0,0,0,0,0,''),
(12599,1, CAST(N'2018-09-10T00:00:00.000' AS DateTime), NULL, 5.5,0,5.5,5.5,5.5,0,0,0,0,0,0,0,0,0,''),
(12600,1, CAST(N'2017-10-13T00:00:00.000' AS DateTime), NULL, 19,0,19,19,19,0,0,0,0,0,0,0,0,0,''),
(12601,1, CAST(N'2017-08-18T00:00:00.000' AS DateTime), NULL, 11.3,0,11.3,12.3,11.3,0,0,0,0,0,0,0,0,0,''),
(12602,1, CAST(N'2018-06-11T00:00:00.000' AS DateTime), NULL, 3,0,3,3,3,0,0,0,0,0,0,0,0,0,''),
(12603,1, CAST(N'2018-04-30T00:00:00.000' AS DateTime), NULL, 6,0,6,6,6,0,0,0,0,0,0,0,0,0,''),
(12604,1, CAST(N'2018-06-21T00:00:00.000' AS DateTime), NULL, 6,0,6,6,6,0,0,0,0,0,0,0,0,0,''),
(12605,1, CAST(N'2018-02-02T00:00:00.000' AS DateTime), NULL, 4,0,4,4,4,0,0,0,0,0,0,0,0,0,''),
(12606,1, CAST(N'2018-11-16T00:00:00.000' AS DateTime), NULL, 9,0,9,9,9,0,0,0,0,0,0,0,0,0,''),
(12607,2, CAST(N'2014-11-15T00:00:00.000' AS DateTime), CAST(N'2015-11-30T00:00:00.000' AS DateTime), 5,0,5,5,5,0,0,0,0,0,0,0,0,0,''),
(12608,2, CAST(N'2016-06-10T00:00:00.000' AS DateTime), CAST(N'2016-08-01T00:00:00.000' AS DateTime), 2,0,2,2,2,0,0,0,0,0,0,0,0,0,''),
(12609,2, CAST(N'2016-09-20T00:00:00.000' AS DateTime), CAST(N'2017-07-11T00:00:00.000' AS DateTime), 11,0,11,11,11,0,0,0,0,0,0,0,0,0,''),
(12610,2, CAST(N'2014-11-18T00:00:00.000' AS DateTime), CAST(N'2015-05-12T00:00:00.000' AS DateTime), 4,0,4,4,4,0,0,0,0,0,0,0,0,0,''),
(12611,2, CAST(N'2015-05-01T00:00:00.000' AS DateTime), CAST(N'2015-10-22T00:00:00.000' AS DateTime), 9,0,9,9,9,0,0,0,0,0,0,0,0,0,''),
(12612,2, CAST(N'2015-05-14T00:00:00.000' AS DateTime), CAST(N'2015-10-27T00:00:00.000' AS DateTime), 25,0,25,25,25,0,0,0,0,0,0,0,0,0,''),
(12613,2, CAST(N'2015-05-14T00:00:00.000' AS DateTime), CAST(N'2015-12-31T00:00:00.000' AS DateTime), 21,0,21,21,21,0,0,0,0,0,0,0,0,0,''),
(12614,3, CAST(N'2018-05-14T00:00:00.000' AS DateTime), CAST(N'2018-08-08T00:00:00.000' AS DateTime), 20,0,20,20,20,0,0,0,0,0,0,0,0,0,''),
(12615,2, CAST(N'2017-10-23T00:00:00.000' AS DateTime), CAST(N'2018-06-30T00:00:00.000' AS DateTime), 20,0,29.7,29.7,29.7,0,0,0,0,0,0,0,0,0,'')

