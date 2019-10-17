CREATE TABLE [dbo].[SessionGoldMiningsTable]
(
	[SessionGoldMiningId] INT NOT NULL PRIMARY KEY IDENTITY,
	[SessionCoreId] INT NOT NULL,
	[BuildingLevel] INT NOT NULL,
	[WorkersNumber] INT NOT NULL,
	[UniqueUpgrade] INT NOT NULL,
	[CapacityUpgrade] INT NOT NULL,
	CONSTRAINT [FK_SessionGoldMiningsTable_SessionCoresTable] FOREIGN KEY ([SessionCoreId]) REFERENCES [SessionCoresTable]([SessionCoreId])
	
)
