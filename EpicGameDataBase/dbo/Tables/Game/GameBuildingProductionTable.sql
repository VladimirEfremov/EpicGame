CREATE TABLE [dbo].[GameBuildingProductionTable]
(
	[GameBuildingProductionTableId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [GameBuildingId] INT NOT NULL, 
    [GameUnitId] INT NOT NULL
	CONSTRAINT [FK2_GameBuildingProductionTable_GameBuildingsTable] FOREIGN KEY ([GameBuildingId]) REFERENCES [GameBuildingsTable]([GameBuildingId])
	CONSTRAINT [FK2_GameBuildingProductionTable_GameUnitsTable] FOREIGN KEY ([GameUnitId]) REFERENCES [GameUnitsTable]([GameUnitId])
)
