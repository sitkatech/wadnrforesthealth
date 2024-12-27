




alter table dbo.Person drop FK_Person_GisUploadAttempt_CreateGisUploadAttemptID_GisUploadAttemptID

alter table dbo.Person drop column CreateGisUploadAttemptID


alter table dbo.ProjectPerson drop FK_ProjectPerson_GisUploadAttempt_CreateGisUploadAttemptID_GisUploadAttemptID

alter table dbo.ProjectPerson drop column CreateGisUploadAttemptID