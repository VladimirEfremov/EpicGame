CREATE TABLE [dbo].[GameBuildingsTable]
(
	[GameBuildingId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [GameBuildingType] INT NOT NULL, 
	[GameBuildingName] NCHAR(40) NOT NULL,
    [GameBuildingHP] INT NOT NULL, 
    [GameBuildingAttack] INT NOT NULL, 
    [GameBuildingDefence] INT NOT NULL, 
    [GameBuildingGoldIncome] INT NOT NULL, 
    [GameBuildingGoldOutcome] INT NOT NULL, 
    [Capcatity] INT NOT NULL
	CONSTRAINT [FK_GameBuildingsTable_GameBuildingsTypeTable] FOREIGN KEY ([GameBuildingType]) REFERENCES [GameBuildingsTypeTable]([GameBuildingTypeId])
)
