-- Just to see if this appears to be the issue
--truncate table dbo.AuditLog

-- More targeted
delete from dbo.AuditLog
where TableName = 'SocrataDataMartRawJsonImport'