CREATE TABLE [dbo].[SessionCoresTable]
(
	[SessionCoreId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] INT NOT NULL,
	[CoreMapId] INT NOT NULL,
	[Money] INT NOT NULL,
    [HPUpgrade] INT NOT NULL, 
    [AttackUpgrade] INT NOT NULL, 
    [DefenceUpgrade] INT NOT NULL, 
    [CapacityUpgrade] INT NOT NULL,
	[WarriorsNumber] INT NOT NULL, 
    [AttackAircraftNumber] INT NOT NULL, 
    [WorkerNumber] INT NOT NULL, 
	CONSTRAINT [FK_SessionCoresTable_UserTable] FOREIGN KEY ([UserId]) REFERENCES [UserTable]([UserId]),
    CONSTRAINT [FK_SessionCoresTable_SessionMapTable] FOREIGN KEY ([CoreMapId]) REFERENCES [SessionMapTable]([SessionMapId]) 
)
