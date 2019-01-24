alter table dbo.TreatmentActivity drop column TreatmentActivityProgramIndex;
alter table dbo.TreatmentActivity drop column TreatmentActivityProjectCode;

alter table dbo.TreatmentActivity add ProgramIndexID int null constraint FK_TreatmentActivity_ProgramIndex_ProgramIndexID references dbo.ProgramIndex(ProgramIndexID);
alter table dbo.TreatmentActivity add ProjectCodeID int null constraint FK_TreatmentActivity_ProjectCode_ProjectCodeID references dbo.ProjectCode(ProjectCodeID);