SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectNotificationRecurranceInterval](
	[ProjectNotificationRecurranceIntervalID] [int] NOT NULL,
	[DisplayName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[RecurranceIntervalInYears] [int] NOT NULL,
 CONSTRAINT [PK_ProjectNotificationRecurranceInterval_ProjectNotificationRecurranceIntervalID] PRIMARY KEY CLUSTERED 
(
	[ProjectNotificationRecurranceIntervalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
