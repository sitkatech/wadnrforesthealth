SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectGrantAllocationRequestRequestFundingSource](
	[ProjectGrantAllocationRequestRequestFundingSourceID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectGrantAllocationRequestID] [int] NOT NULL,
	[ProjectGrantAllocationRequestFundingSourceID] [int] NOT NULL,
 CONSTRAINT [PK_ProjectGrantAllocationRequestRequestFundingSource_ProjectGrantAllocationRequestRequestFundingSourceID] PRIMARY KEY CLUSTERED 
(
	[ProjectGrantAllocationRequestRequestFundingSourceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ProjectGrantAllocationRequestRequestFundingSource]  WITH CHECK ADD  CONSTRAINT [FK_ProjectGrantAllocationRequestRequestFundingSource_ProjectGrantAllocationRequest_ProjectGrantAllocationRequestID] FOREIGN KEY([ProjectGrantAllocationRequestID])
REFERENCES [dbo].[ProjectGrantAllocationRequest] ([ProjectGrantAllocationRequestID])
GO
ALTER TABLE [dbo].[ProjectGrantAllocationRequestRequestFundingSource] CHECK CONSTRAINT [FK_ProjectGrantAllocationRequestRequestFundingSource_ProjectGrantAllocationRequest_ProjectGrantAllocationRequestID]
GO
ALTER TABLE [dbo].[ProjectGrantAllocationRequestRequestFundingSource]  WITH CHECK ADD  CONSTRAINT [FK_ProjectGrantAllocationRequestRequestFundingSource_ProjectGrantAllocationRequestFundingSourceID] FOREIGN KEY([ProjectGrantAllocationRequestFundingSourceID])
REFERENCES [dbo].[ProjectGrantAllocationRequestFundingSource] ([ProjectGrantAllocationRequestFundingSourceID])
GO
ALTER TABLE [dbo].[ProjectGrantAllocationRequestRequestFundingSource] CHECK CONSTRAINT [FK_ProjectGrantAllocationRequestRequestFundingSource_ProjectGrantAllocationRequestFundingSourceID]