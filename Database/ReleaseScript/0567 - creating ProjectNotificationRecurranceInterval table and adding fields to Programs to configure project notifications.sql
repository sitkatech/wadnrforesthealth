alter table dbo.Program
add ProgramProjectsReceiveCompletedNotifications bit default(0)


create table dbo.ProjectNotificationRecurranceInterval(
	ProjectNotificationRecurranceIntervalID int not null constraint PK_ProjectNotificationRecurranceInterval_ProjectNotificationRecurranceIntervalID primary key,
	DisplayName varchar(100) not null,
	RecurranceIntervalInYears int not null
)


insert into dbo.ProjectNotificationRecurranceInterval(ProjectNotificationRecurranceIntervalID, DisplayName, RecurranceIntervalInYears)
values 
(1, '1 Year', 1),
(2, '5 Years', 5),
(3, '10 Years', 10),
(4, '15 Years', 15);


alter table dbo.Program
add ProjectNotificationRecurranceInterval int null constraint FK_Program_ProjectNotificationRecurranceInterval_ProjectNotificationRecurranceIntervalID foreign key references dbo.ProjectNotificationRecurranceInterval(ProjectNotificationRecurranceIntervalID)

