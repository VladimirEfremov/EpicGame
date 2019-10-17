CREATE TABLE [dbo].[SessionBasesTable]
(
	[SessionBaseId] INT NOT NULL PRIMARY KEY IDENTITY,
	[SessionCoreId] INT NOT NULL,
	[BuildingLevel] INT NOT NULL,
	[WorkersNumber] INT NOT NULL,
	[UniqueUpgrade] INT NOT NULL,
	[AttackUpgrade] INT NOT NULL,
	[DefenceUpgrade] INT NOT NULL,
	[CapacityUpgrade] INT NOT NULL,
	CONSTRAINT [FK_SessionBasesTable_SessionCoresTable] FOREIGN KEY ([SessionCoreId]) REFERENCES [SessionCoresTable]([SessionCoreId])
)
