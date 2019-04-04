-- Adding FileResourceID foreign key to GrantAllocation for tracking uploads on a GrantAllocation object
alter table dbo.GrantAllocation
add GrantAllocationFileResourceID int null constraint FK_GrantAllocation_FileResource_GrantAllocationFileResourceID_FileResourceID foreign key references dbo.FileResource(FileResourceID)