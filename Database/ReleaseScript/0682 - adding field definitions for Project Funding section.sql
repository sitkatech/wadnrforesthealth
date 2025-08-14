



delete from dbo.FieldDefinitionData where FieldDefinitionID = 471
insert into dbo.FieldDefinitionData (FieldDefinitionID, FieldDefinitionDataValue, FieldDefinitionLabel)
values (471, 'The funding sources for the project- Federal, State, Private, and/or Other. Note that not all sources of funds may be represented in the Forest Health Tracker.', 'Source of Funds')


delete from dbo.FieldDefinitionData where FieldDefinitionID = 472
insert into dbo.FieldDefinitionData (FieldDefinitionID, FieldDefinitionDataValue, FieldDefinitionLabel)
values (472, null, 'Notes about Project Funding')


delete from dbo.FieldDefinitionData where FieldDefinitionID = 278
insert into dbo.FieldDefinitionData (FieldDefinitionID, FieldDefinitionDataValue, FieldDefinitionLabel)
values (278, 'WA DNR managed funding allocations from federal and state funding sources.', 'WA DNR Fund Source Allocation')