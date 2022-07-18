
alter table ProjectImportBlockList
add ProjectID int null constraint FK_ProjectImportBlockList_Project_ProjectID foreign key (ProjectID) references dbo.Project (ProjectID)
