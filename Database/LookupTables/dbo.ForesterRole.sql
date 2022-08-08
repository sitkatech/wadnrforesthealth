SET IDENTITY_INSERT dbo.ForesterRole ON;

delete from dbo.ForesterRole

Insert into dbo.ForesterRole (ForesterRoleID, ForesterRoleDisplayName, ForesterRoleName)
values
(1, 'Service Forester', 'ServiceForester'),
(2, 'Service Forestry Specialist', 'ServiceForestrySpecialist'),
(3, 'Forest Practices Forester', 'ForestPracticesForester'),
(4, 'Stewardship Biologist', 'StewardshipBiologist'),
(5, 'Urban Forestry Technician', 'UrbanForestryTechnician'),
(6, 'Community Wildfire Preparedness Specialist', 'CommunityWildfirePreparednessSpecialist'),
(7, 'Regulation Assistance Forester', 'RegulationAssistanceForester'),
(8, 'Family Forest Fish Passage Program', 'FamilyForestFishPassageProgram'),
(9, 'Forestry Riparian Easement Program', 'ForestryRiparianEasementProgram'),
(10, 'Rivers and Habitat Open Space Program Manager', 'RiversAndHabitatOpenSpaceProgramManager'),
(11, 'Service Forestry Program Manager', 'ServiceForestryProgramManager'),
(12, 'UCF Statewide Specialist', 'UcfStatewideSpecialist'),
(13, 'Small Forest Landowner Office Program Manager', 'SmallForestLandownerOfficeProgramManager')


SET IDENTITY_INSERT dbo.ForesterRole OFF;




