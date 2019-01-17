CREATE TABLE dbo.[Grant]
(
    -- Just a few fields to start -- very incomplete
    GrantID int not NULL identity(1,1) Constraint PK_Grant_GrantID primary key,
    TenantID int not null constraint FK_Grant_Tenant_TenantID references dbo.Tenant(TenantID),
    GrantNumber varchar(30) null,
    StartDate DateTime NULL,
    EndDate DateTime NULL,
	ProgramIndex int NULL,
	ProjectCode varchar(100) NULL,
	ConditionsAndRequirements varchar(max) NULL,
	ComplianceNotes varchar(max) NULL,
    AwardedFunds money null -- This should be AwardAmount according to Liz's data model
)
go
insert into dbo.FieldDefinition values
(277, N'Grant', 'Grant', 'Placeholder definition for Grant.')

insert into dbo.FieldDefinitionData (TenantID, FieldDefinitionID, FieldDefinitionDataValue, FieldDefinitionLabel)
select t.TenantID, 277, Null, Null
From dbo.Tenant as t
