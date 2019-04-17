--begin tran

-- Drop relevant constraints
ALTER TABLE [dbo].[ProgramIndex] DROP CONSTRAINT [AK_ProgramIndex_ProgramIndexCode_Biennium]
GO
ALTER TABLE [dbo].[ProgramIndex] DROP CONSTRAINT [AK_ProgramIndex_ProgramIndexTitle_Biennium]
GO


alter table dbo.ProgramIndex
alter column Biennium int not null


-- Re-institute relevant constraints
ALTER TABLE [dbo].[ProgramIndex] ADD  CONSTRAINT [AK_ProgramIndex_ProgramIndexCode_Biennium] UNIQUE NONCLUSTERED 
(
    [ProgramIndexCode] ASC,
    [Biennium] ASC
)
GO

ALTER TABLE [dbo].[ProgramIndex] ADD  CONSTRAINT [AK_ProgramIndex_ProgramIndexTitle_Biennium] UNIQUE NONCLUSTERED 
(
    [ProgramIndexTitle] ASC,
    [Biennium] ASC
)
GO

--rollback tran