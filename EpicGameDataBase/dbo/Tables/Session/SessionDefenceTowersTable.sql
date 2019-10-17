CREATE TABLE [dbo].[SessionDefenceTowersTable]
(
	[SessionDefenceTowerId] INT NOT NULL PRIMARY KEY IDENTITY,
	[SessionCoreId] INT NOT NULL,
	[BuildingLevel] INT NOT NULL,
	[UniqueUpgrade] INT NOT NULL,
	[AttackUpgrade] INT NOT NULL,
	[DefenceUpgrade] INT NOT NULL,
	CONSTRAINT [FK_SessionDefenceTowersTable_SessionCoresTable] FOREIGN KEY ([SessionCoreId]) REFERENCES [SessionCoresTable]([SessionCoreId])
)
