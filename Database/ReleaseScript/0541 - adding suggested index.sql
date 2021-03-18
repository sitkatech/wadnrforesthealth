
CREATE NONCLUSTERED INDEX [IDX_LoaStageGrantNumber]
ON [dbo].[LoaStage] ([GrantNumber])
INCLUDE ([FocusAreaName],[IsSoutheast])
GO

