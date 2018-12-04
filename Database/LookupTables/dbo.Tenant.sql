delete from dbo.Tenant
go

insert into dbo.Tenant(TenantID, TenantName, CanonicalHostNameLocal, CanonicalHostNameQa, CanonicalHostNameProd, ReportingYearStartDate, UseFiscalYears, UsesTechnicalAssistanceParameters)
values 
(10, 'WashingtonDepartmentOfNaturalResources', 'wadnrforesthealth.localhost.projectfirma.com', 'wadnrforesthealth.qa.projectfirma.com', 'foresthealthtracker.dnr.wa.gov', '1/1/1990', 0, 0)