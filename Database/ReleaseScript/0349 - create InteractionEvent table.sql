create table dbo.InteractionEventType(
	InteractionEventTypeID int not null constraint PK_InteractionEventType_InteractionEventTypeID primary key,
	InteractionEventTypeName varchar(200) not null,
	InteractionEventTypeDisplayName varchar(255) not null
)

INSERT INTO dbo.InteractionEventType VALUES 
(1, 'Complaint', 'Complaint'),
(2, 'FireSafetyPresentation', 'Fire Safety Presentation'),
(3, 'ForestLandownerFieldDay', 'Forest Landowner Field Day'),
(4, 'Other', 'Other'),
(5, 'Outreach', 'Outreach'),
(6, 'PhoneCall', 'Phone Call'),
(7, 'SiteVisit', 'Site Visit'),
(8, 'TechnicalAssistance', 'Technical Assistance'),
(9, 'Workshop', 'Workshop');
 



create table dbo.InteractionEvent(
	InteractionEventID int identity(1,1) not null CONSTRAINT [PK_InteractionEvent_InteractionEventID] PRIMARY KEY,
	InteractionEventTypeID int not null constraint FK_InteractionEvent_InteractionEventType_InteractionEventTypeID references InteractionEventType(InteractionEventTypeID),
	StaffPersonID int not null constraint FK_InteractionEvent_Person_StaffPersonID_PersonID references Person(PersonID),
	InteractionEventTitle varchar(255) not null,
	InteractionEventDescription varchar(3000),
	InteractionEventDate datetime not null
	
) 



insert into dbo.FirmaPageType(FirmaPageTypeID, FirmaPageTypeName, FirmaPageTypeDisplayName, FirmaPageRenderTypeID)
values (62, 'InteractionEventList', 'Interaction/Event List', 1);

insert into dbo.FirmaPage(FirmaPageTypeID) values(62);