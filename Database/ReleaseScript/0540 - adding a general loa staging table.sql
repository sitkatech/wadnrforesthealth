
CREATE TABLE [dbo].[LoaStage](
	[LoaStageID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectIdentifier] varchar(600) NOT NULL,
	[ProjectStatus] varchar(600) NULL,
	[GrantNumber] [varchar](600) NULL,
	[FocusAreaName] [varchar](600) NULL,
	[ProjectExpirationDate] [datetime] NULL,
	[LetterDate] [datetime] NULL,
	[MatchAmount] money null,
	[PayAmount] money null,
	[ProgramIndex] varchar(50) null,
    [ProjectCode] varchar(50) null,
    [IsNortheast] bit not null,
    [IsSoutheast] as cast(case when IsNortheast = 1 then 0 else 1 end as bit) Persisted not null
	
 CONSTRAINT [PK_LoaStage_LoaStageID] PRIMARY KEY CLUSTERED 
(
	[LoaStageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
)


go

--insert into dbo.LoaStage(ProjectIdentifier, ProjectStatus, GrantNumber, FocusAreaName, ProjectExpirationDate, LetterDate, MatchAmount, PayAmount, ProgramIndex, ProjectCode, IsNortheast)
--select x.[Project ID],
--       x.[Status],
--       x.[Grant #],
--       x.[Grant],
--       x.[Project Expiration Date],
--       x.[Letter Date],
--       x.[Match],
--       x.Pay,
--       x.[Index],
--       x.Code,
--       1
-- from dbo.LoaRawNortheast x
-- where x.[Project ID] is not null


-- insert into dbo.LoaStage(ProjectIdentifier, ProjectStatus, GrantNumber, FocusAreaName, ProjectExpirationDate, LetterDate, MatchAmount, PayAmount, ProgramIndex, ProjectCode, IsNortheast)
--select x.[Project ID],
--       x.[Status],
--       x.[Grant #],
--       x.[Grant],
--       case when TRY_PARSE(x.[Project Expiration Date] AS DATE) IS NOT NULL then parse(x.[Project Expiration Date] as datetime) else null end as ExpirationDate,
--        case when TRY_PARSE(x.[Letter Date] AS DATE) IS NOT NULL then parse(x.[Letter Date] as datetime) else null end as LetterDate,
--       x.[Match],
--       x.Pay,
--       x.[Index],
--       x.Code,
--       0 as IsNortheast
-- from dbo.LoaRawSoutheast x
-- WHERE  x.[Project ID] is not null