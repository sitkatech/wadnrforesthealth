
CREATE NONCLUSTERED INDEX [<Name of Missing Index, sysname,>]
ON [dbo].[LoaStage] ([GrantNumber])
INCLUDE ([FocusAreaName],[IsSoutheast])
GO

