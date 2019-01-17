IF EXISTS(SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.fnSplitString'))
    drop function dbo.fnSplitString
go

CREATE FUNCTION [dbo].fnSplitString
(
   @string NVARCHAR(MAX),
   @delimiter CHAR(1)
)
RETURNS @output TABLE(splitdata NVARCHAR(MAX)
)
BEGIN
   DECLARE @start INT, @end INT
   SELECT @start = 1, @end = CHARINDEX(@delimiter, @string)
   WHILE @start < LEN(@string) + 1 BEGIN
       IF @end = 0
           SET @end = LEN(@string) + 1

       INSERT INTO @output (splitdata)
       VALUES(SUBSTRING(@string, @start, @end - @start))
       SET @start = @end + 1
       SET @end = CHARINDEX(@delimiter, @string, @start)

   END
   RETURN
END
go

declare @AllProjectCodes varchar(max)
declare @separatedProjectCodes table (
	id int identity(1,1) not null,
	abbrev nvarchar(max) null
);

set @AllProjectCodes = (
SELECT top 1
   abc = STUFF(
                (SELECT ',' + [Project Codes] FROM dbo.tmpChildrenGrantsInGrantsTab FOR XML PATH ('')), 1, 1, ''
              )
FROM dbo.tmpChildrenGrantsInGrantsTab)

 insert into @separatedProjectCodes (abbrev)
	select * from dbo.fnSplitString (@AllProjectCodes, ',')
 
insert into dbo.ProjectCode(TenantID, ProjectCodeAbbrev)
	select distinct 10, REPLACE(LTRIM(RTRIM(abbrev)), '"', '') from @separatedProjectCodes
	where LTRIM(RTRIM(abbrev)) not in ('', '---')
	order by REPLACE(LTRIM(RTRIM(abbrev)), '"', '')
select * from dbo.ProjectCode

drop function dbo.fnSplitString