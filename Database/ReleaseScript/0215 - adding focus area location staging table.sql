create table dbo.FocusAreaLocationStaging (
FocusAreaLocationStaggingID int not null identity(1, 1) constraint PK_FocusAreaLocationStaging_FocusAreaLocationStagingID primary key,
TenantID int not null constraint FK_FocusAreaLocationStaging_Tenant_TenantID foreign key references dbo.Tenant,
FocusAreaID int not null constraint FK_FocusAreaLocationStaging_FocusArea_FocusAreaID foreign key references dbo.FocusArea,
FeatureClassName varchar(255) not null,
GeoJson varchar(max) not null 
)