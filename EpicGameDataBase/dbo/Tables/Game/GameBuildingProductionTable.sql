CREATE TABLE [dbo].[GameBuildingProductionTable]
(
	[GameBuildingProductionTableId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [GameBuildingId] int NOT NULL, 
    [GameUnitId] int NOT NULL
	CONSTRAINT [FK_GameBuildingProductionTable_GameBuildingsTable] FOREIGN KEY ([GameBuildingId]) REFERENCES [GameBuildingsTable]([GameBuildingId])
	CONSTRAINT [FK_GameBuildingProductionTable_GameUnitsTable] FOREIGN KEY ([GameUnitId]) REFERENCES [GameUnitsTable]([GameUnitId])
)
