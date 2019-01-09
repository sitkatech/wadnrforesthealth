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

-- update SAW logins for Sitka folks since SAW prod and SAW test have different sets of users
update dbo.Person
set PersonUniqueIdentifier = 'DP3TP7WZ2MM7W-1DZ8DP4Q-1DL2VV0ZF1-D1FV3ZT5VM'
where Email = 'ray@sitkatech.com'

update dbo.Person
set PersonUniqueIdentifier = 'DP4VW0PV4LL0W-1TW0LT5VV8-D1LW4VZ0FD-FF8FP8MQ1'
where Email = 'liz.christeleit@sitkatech.com'

update dbo.Person
set PersonUniqueIdentifier = 'C9E033A2-E53D-4A29-A368-037BF0518E38'
where Email = 'brian.grass@sitkatech.com'

update dbo.Person
set PersonUniqueIdentifier = 'F96C4B4F-BFE1-4FC6-8F1E-2CB1B0B19913'
where Email = 'dal.marsters@sitkatech.com'

update dbo.Person
set PersonUniqueIdentifier = 'DP4MT6TV3ZT7M-3QT5ZL6DM-DD7WV4ZZ8D-1FZ3DD4VZ4', RoleID = 8
where Email = 'tom.kamin@sitkatech.com'

update dbo.Person
set PersonUniqueIdentifier = 'DP4MT6TZ6FZ7D-1QW0VV7DV5-D1LW4VZ0FD-DT7QT3LT1P', RoleID = 8
where Email = 'ian.stavros@sitkatech.com'


goto goodbye

failed:

raiserror('The database update tenant attributes script failed. Additional error details should be available above.', 16, 1)

goodbye:

use master


