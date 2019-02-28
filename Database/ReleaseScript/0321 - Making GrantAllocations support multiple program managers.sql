--begin tran

-- Drop old single cardinality

alter table GrantAllocation
drop constraint FK_GrantAllocation_Person_ProgramManagerPersonID_PersonID

alter table GrantAllocation
drop column ProgramManagerPersonID
GO


-- New linking table
CREATE TABLE [dbo].[GrantAllocationProgramManager]
(
    GrantAllocationProgramManagerID [int] IDENTITY(1,1) NOT NULL,
    GrantAllocationID [int] NOT NULL,
    PersonID [int] NOT NULL
 -- PK
 CONSTRAINT [PK_GrantAllocationProgramManager_GrantAllocationProgramManagerID] PRIMARY KEY CLUSTERED 
(
    GrantAllocationProgramManagerID  ASC
)ON [PRIMARY]
) ON [PRIMARY]
GO

-- FK => GrantAllocationID
ALTER TABLE [dbo].[GrantAllocationProgramManager]  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocationProgramManager_GrantAllocation_GrantAllocationID] FOREIGN KEY([GrantAllocationID])
REFERENCES [dbo].[GrantAllocation] ([GrantAllocationID])
GO

 --FK => Person
ALTER TABLE [dbo].[GrantAllocationProgramManager]  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocationProgramManager_Person_PersonID] FOREIGN KEY(PersonID)
REFERENCES [dbo].[Person] ([PersonID])
GO



--rollback tran


