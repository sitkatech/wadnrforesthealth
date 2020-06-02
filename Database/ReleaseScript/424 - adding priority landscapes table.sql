--IF  NOT (EXISTS (SELECT * 
--                 FROM INFORMATION_SCHEMA.TABLES 
--                 WHERE TABLE_SCHEMA = 'dbo' 
--                 AND  TABLE_NAME = 'PriorityLandscape'))
--BEGIN
--   CREATE TABLE [dbo].[PriorityLandscape](
--	[PriorityLandscapeID] [int] NOT NULL,
--	[PriorityLandscapeName] [varchar](100) NOT NULL,
--	[PriorityLandscapeLocation] [geometry] NULL,
-- CONSTRAINT [PK_PriorityLandscape_PriorityLandscapeID] PRIMARY KEY CLUSTERED 
--(
--	[PriorityLandscapeID] ASC
--)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
-- CONSTRAINT [AK_PriorityLandscape_PriorityLandscapeName] UNIQUE NONCLUSTERED 
--(
--	[PriorityLandscapeName] ASC
--)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
--) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

--END



----insert into dbo.PriorityLandscape ([PriorityLandscapeID] , [PriorityLandscapeName], [PriorityLandscapeLocation])

----select x.ogr_fid,  x.plan_area + ' - ' + cast(cast(x.plan_year as int) as varchar) as PriorityLandscapeName, ogr_geometry as PriorityLandscapeLocation from dbo._temptable x


