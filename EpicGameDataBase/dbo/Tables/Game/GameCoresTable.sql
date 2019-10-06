CREATE TABLE [dbo].[GameCoresTable]
(
	[GameCoreId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] INT NOT NULL
	CONSTRAINT [FK_GameCoresTable_UserTable] FOREIGN KEY ([UserId]) REFERENCES [UserTable]([UserId])
)
