

create table dbo.ProgramNotificationType(
	ProgramNotificationTypeID int not null constraint PK_ProgramNotificationType_ProgramNotificationTypeID primary key,
	ProgramNotificationTypeName varchar(100) not null,
	ProgramNotificationTypeDisplayName varchar(100) not null
)


insert into dbo.ProgramNotificationType(ProgramNotificationTypeID, ProgramNotificationTypeName, ProgramNotificationTypeDisplayName)
values
(1, 'CompletedProjectsMaintenanceReminder', 'Completed Projects Maintenance Reminder')


create table dbo.RecurrenceInterval(
	RecurrenceIntervalID int not null constraint PK_RecurrenceInterval_RecurrenceIntervalID primary key,
	RecurrenceIntervalInYears int not null,
	RecurrenceIntervalDisplayName varchar(100) not null,
	RecurrenceIntervalName varchar(100) not null
)


insert into dbo.RecurrenceInterval(RecurrenceIntervalID, RecurrenceIntervalInYears, RecurrenceIntervalDisplayName, RecurrenceIntervalName)
values
(1, 1, '1 Year', 'OneYear'),
(2, 5, '5 Years', 'FiveYears'),
(3, 10, '10 Years', 'TenYears'),
(4, 15, '15 Years', 'FifteenYears');


create table dbo.ProgramNotificationConfiguration(
	ProgramNotificationConfigurationID int not null identity(1,1) constraint PK_ProgramNotificationConfiguration_ProgramNotificationConfigurationID primary key,
	ProgramID int not null constraint FK_ProgramNotificationConfiguration_Program_ProgramID foreign key references dbo.Program(ProgramID),
	ProgramNotificationTypeID int not null constraint FK_ProgramNotificationConfiguration_ProgramNotificationType_ProgramNotificationTypeID foreign key references dbo.ProgramNotificationType(ProgramNotificationTypeID),
	RecurrenceIntervalID int not null constraint FK_ProgramNotificationConfiguration_RecurrenceInterval_RecurrenceIntervalID foreign key references dbo.RecurrenceInterval(RecurrenceIntervalID),
	NotificationEmailText html not null
)


create table dbo.ProgramNotificationSent(
	ProgramNotificationSendID int not null identity(1,1) constraint PK_ProgramNotificationSent_ProgramNotificationSentID primary key,
	ProgramNotificationConfigurationID int not null constraint FK_ProgramNotificationSent_ProgramNotificationConfiguration_ProgramNotificationConfigurationID foreign key references dbo.ProgramNotificationConfiguration(ProgramNotificationConfigurationID),
	SentToPersonID int not null constraint FK_ProgramNotificationSent_Person_SentToPersonID_PersonID foreign key references dbo.Person(PersonID),
	ProjectID int not null constraint FK_ProgramNotificationSent_Project_ProjectID foreign key references dbo.Project(ProjectID),
	ProgramNotificationSentDate datetime not null
)