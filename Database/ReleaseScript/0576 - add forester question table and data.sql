




create table dbo.FindYourForesterQuestion(
	FindYourForesterQuestionID int not null identity(1,1) constraint PK_FindYourForesterQuestion_FindYourForesterQuestionID primary key,
	QuestionText varchar(500) not null,
	ParentQuestionID int constraint FK_FindYourForesterQuestion_FindYourForesterQuestion_ParentQuestionID_FindYourForesterQuestionID foreign key references dbo.FindYourForesterQuestion(FindYourForesterQuestionID),
	ForesterRoleID int constraint FK_FindYourForesterQuestion_ForesterRole_ForesterRoleID foreign key references dbo.ForesterRole(ForesterRoleID),
	ResultsBonusContent html

)







SET IDENTITY_INSERT dbo.FindYourForesterQuestion ON;

insert into dbo.FindYourForesterQuestion(FindYourForesterQuestionID, QuestionText, ParentQuestionID, ForesterRoleID, ResultsBonusContent)
values
(1, 'Forest Management Technical Assistance', null, null, null),
(2, 'Educational Opportunities and Community Outreach', null, null, null),
(3, 'Permits & Regulations', null, null, null),
(4, 'Fire Prevention & Preparedness', null, null, null),
(5, 'Funding', null, null, null),
(6, 'Forest Planning', null, null, null),
(7, 'Finding a Consultant, Contract, Crew, or Third Party Distributor', null, null, null)
go

insert into dbo.FindYourForesterQuestion(FindYourForesterQuestionID, QuestionText, ParentQuestionID, ForesterRoleID, ResultsBonusContent)
values
(8, 'I need technical assistance for Commercial Forest Management related to...', 1, null, null),
(9, 'I need technical assistance for Non-Commercial Forest Management related to...', 1, null, null),
(10, 'I am interested in other Private Lands Management programs, such as NRCS', 1, 11, null),
(11, 'I have general questions about Forest Management for...', 2, null, null),
(12, 'I want to learn more about managing my family forest. (Forest Stewardship Coach Planning)', 2, 1, null),
(13, 'I would like a DNR forester for my community outreach or education event...', 2, null, null),
(14, 'I need or have questions about a Forest Practices Application or Alternate Plan', 3, 3, null),
(15, 'I need or have questions about a Forest Herbicide Application', 3, 3, null),

(16, 'I need or have questions about a Burn Permit', 3, null, 'Please visit <a href="https://www.dnr.wa.gov/OutdoorBurning" target="_blank">https://www.dnr.wa.gov/OutdoorBurning</a>'),

(17, 'I am interested in community risk and resiliency planning, Community Wildfire Preparedness plans, Firewise USA, Wildfire Ready Neighbors, or working with fire districts', 4, 6, null),
(18, 'I am interested in DNR''s cost-share grants for forest management, forest health, or fuels mitigation.', 5, 1, null),
(19, 'I am interested in DNR''s regulatory assistance program for...', 5, null, null),
(20, 'I am interested in Stewardship Planning assistance.', 5, 1, null),
(21, 'I am interested in Family Forest Succession Planning', 6, 1, null),
(22, 'I would like to develop a Forest Management or Stewardship Plan', 6, 1, null),
(23, 'I need assistance with forest roads and/or culverts', 6, 3, null),
(24, 'I am interested in urban forestry planning', 6, 12, null),
(25, 'I''m looking for a consulting forester or contractor for...', 7, null, null),
(26, 'I need my Tree Farm Inspected', 7, 1, '*DNR has specific staff certified to inspect tree farms. Your local Service Forestry can connect you with the best person in your area.'),
(27, 'I''m looking for a nursery to buy trees for my property.', 7, 1, null)
go

insert into dbo.FindYourForesterQuestion(FindYourForesterQuestionID, QuestionText, ParentQuestionID, ForesterRoleID, ResultsBonusContent)
values
(28, 'Alternate Plans', 8, 7, null),
(29, 'Forest Herbicide Application', 8, 1, null),
(30, 'Timber Production', 8, 1, null),
(31, 'Forest health, insects, and disease, and/or fuels management', 9, 1, null),
(32, 'Management for recreation, climate resilience, or other forest products', 9, 1, null),
(33, 'Managing for or promoting wildlife on my property', 9, 4, null),
(34, 'Regulatory questions related to forest management', 11, 7, null),
(35, 'Commercial forest management questions', 11, 1, null),
(36, 'Non-Commercial forest management questions', 11, 1, null),
(37, 'Regarding wildfire prevention and preparedness', 13, 6, null),
(38, 'Regarding forest management, conservation, and/or forest health', 13, 1, null),
(39, 'Regarding forest and habitat management to promote wildlife', 13, 4, null),
(40, 'Fish Passage Family Forest Fish Passage Program (FFFPP)', 19, 8, null),
(41, 'Riparian Easements Forest Riparian Easement Program (FREP)', 19, 9, null),
(42, 'Forestland Conservation Rivers and Habitat Open Space Program (RHOSP)', 19, 10, null),
(43, 'Commercial purposes', 25, 13, null),
(44, 'Non-commercial forest management and/or planning', 25, 1, null)




SET IDENTITY_INSERT dbo.FindYourForesterQuestion OFF;



/*


select * from dbo.ForesterRole


*/