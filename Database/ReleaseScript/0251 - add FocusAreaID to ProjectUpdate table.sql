ALTER TABLE dbo.ProjectUpdate ADD FocusAreaID int NULL   
    CONSTRAINT FK_ProjectUpdate_FocusArea_FocusAreaID foreign key references dbo.FocusArea(FocusAreaID)  