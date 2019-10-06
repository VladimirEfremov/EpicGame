CREATE TABLE [dbo].[SessionCoresTable]
(
	[SessionCoreId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] INT NOT NULL
	CONSTRAINT [FK_SessionCoresTable_UserTable] FOREIGN KEY ([UserId]) REFERENCES [UserTable]([UserId])
)
