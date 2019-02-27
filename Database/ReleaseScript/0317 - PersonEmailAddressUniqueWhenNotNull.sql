CREATE UNIQUE NONCLUSTERED INDEX [IX_Person_Email_UniqueWhenNotNull] ON [dbo].[Person]
(
	[Email] ASC
)
WHERE ([Email] IS NOT NULL)
