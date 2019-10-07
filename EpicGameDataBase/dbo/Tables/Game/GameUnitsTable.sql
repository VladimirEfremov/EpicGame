CREATE TABLE [dbo].[GameUnitsTable]
(
	[GameUnitId] INT NOT NULL PRIMARY KEY IDENTITY,
	[GameUnitType] INT NOT NULL,
	[GameUnitName] NCHAR(40) NOT NULL,
    [GameUnitHP] INT NOT NULL, 
    [GameUnitAttack] INT NOT NULL, 
    [GameUnitDefence] INT NOT NULL, 
    [GameUnitGoldIncome] INT NOT NULL, 
    [GameUnitGoldOutcome] INT NOT NULL,
	CONSTRAINT [FK_GameUnitsTable_GameUnitTypeTable] FOREIGN KEY ([GameUnitType]) REFERENCES [GameUnitTypeTable]([GameUnitTypeId])
)
