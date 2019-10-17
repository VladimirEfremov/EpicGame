CREATE TABLE [dbo].[SessionCoresTable]
(
	[SessionCoreId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] INT NOT NULL,
	[CoreMapId] INT NOT NULL,
	[Money] INT NOT NULL,
	CONSTRAINT [FK_SessionCoresTable_UserTable] FOREIGN KEY ([UserId]) REFERENCES [UserTable]([UserId]),
    CONSTRAINT [FK_SessionCoresTable_SessionMapTable] FOREIGN KEY ([CoreMapId]) REFERENCES [SessionMapTable]([SessionMapId]) 
)
