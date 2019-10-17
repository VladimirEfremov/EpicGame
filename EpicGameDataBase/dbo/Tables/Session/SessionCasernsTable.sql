CREATE TABLE [dbo].[SessionCasernsTable]
(
	[SessionCasernId] INT NOT NULL PRIMARY KEY IDENTITY,
	[SessionCoreId] INT NOT NULL,
	[BuildingLevel] INT NOT NULL,
	[WarriorsNumber] INT NOT NULL, 
    [AttackAircraftNumber] INT NOT NULL, 
	[UniqueUpgrade] INT NOT NULL,
	[CapacityUpgrade] INT NOT NULL,
	CONSTRAINT [FK_SessionCasernsTable_SessionCoresTable] FOREIGN KEY ([SessionCoreId]) REFERENCES [SessionCoresTable]([SessionCoreId])
)
