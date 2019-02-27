-- this script is used by nant to update the TenantAttribute table to make certain fields be environment aware
use ${db-name}
if (@@error != 0) goto failed

if(exists(select 1 from sys.columns c join sys.tables t on c.object_id = t.object_id where c.name = 'MapServiceUrl' and t.name = 'GeospatialAreaType'))
begin
	declare @sql nvarchar(max)
	set @sql = 'update dbo.GeospatialAreaType set MapServiceUrl = replace(MapServiceUrl, ''wadnrforesthealth-mapserver'', ''@ENVIRONMENT@-wadnrforesthealth-mapserver'')'
	exec sp_executesql @sql
	if (@@error != 0) goto failed
end

-- update SAW logins for Sitka folks since SAW prod and SAW test have different identifiers for users
update dbo.Person
set PersonUniqueIdentifier = y.PersonUniqueIdentifier, RoleID = 8
from dbo.Person p
join
(
    select x.Email, x.PersonUniqueIdentifier
    from
    (
        select null as Email, null as PersonUniqueIdentifier
        union all select 'brian.grass@sitkatech.com', 'C9E033A2-E53D-4A29-A368-037BF0518E38'
        union all select 'dal.marsters@sitkatech.com', 'DP4LM5FL0TM6F-4PQ8VL4PQ-DD7WV4ZZ8D-9VL7MQ6QL'
        union all select 'ian.stavros@sitkatech.com', 'DP4MT6TZ6FZ7D-1QW0VV7DV5-D1LW4VZ0FD-DT7QT3LT1P'
        union all select 'john.vivio@sitkatech.com', 'xxxxxx'
        union all select 'liz.christeleit@sitkatech.com', 'DP4VW0PV4LL0W-1TW0LT5VV8-D1LW4VZ0FD-FF8FP8MQ1'
        union all select 'michael@sitkatech.com', 'DP4VD4ZP8QQ6L-1PF0ZV2FF4-D1LW4VZ0FD-DW9QW8ZL9Q'
        union all select 'mike.jolliffe@sitkatech.com', 'DP4VD4WZ7PT5M-2ZT0ZP9VW-DD7WV4ZZ8D-3DF2DQ9MP'
        union all select 'ray@sitkatech.com', 'DP3TP7WZ2MM7W-1DZ8DP4Q-1DL2VV0ZF1-D1FV3ZT5VM'
        union all select 'stewart@sitkatech.com', 'DP4VD4DQ9PT2M-1PM3PL8QZ5-D1LW4VZ0FD-DF1ZQ5VZ3T'
        union all select 'tom.kamin@sitkatech.com', 'DP4MT6TV3ZT7M-3QT5ZL6DM-DD7WV4ZZ8D-1FZ3DD4VZ4'
    ) x
    where x.Email is not null
) y on p.Email = y.Email
goto goodbye

failed:

raiserror('The database update tenant attributes script failed. Additional error details should be available above.', 16, 1)

goodbye:

use master
