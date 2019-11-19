SET IDENTITY_INSERT [dbo].[GameUnitTypeTable] ON
INSERT INTO [dbo].[GameUnitTypeTable] ([GameUnitTypeId], [GameUnitTypeName]) VALUES (1, N'Production                              ')
INSERT INTO [dbo].[GameUnitTypeTable] ([GameUnitTypeId], [GameUnitTypeName]) VALUES (2, N'Attack                                  ')
INSERT INTO [dbo].[GameUnitTypeTable] ([GameUnitTypeId], [GameUnitTypeName]) VALUES (3, N'Defence                                 ')
SET IDENTITY_INSERT [dbo].[GameUnitTypeTable] OFF

SET IDENTITY_INSERT [dbo].[GameUnitsTable] ON
INSERT INTO [dbo].[GameUnitsTable] ([GameUnitId], [GameUnitType], [GameUnitName], [GameUnitHP], [GameUnitAttack], [GameUnitDefence], [GameUnitGoldIncome], [GameUnitGoldOutcome]) VALUES (1, 1, N'Worker                                  ', 250, 0, 0, 25, 10)
INSERT INTO [dbo].[GameUnitsTable] ([GameUnitId], [GameUnitType], [GameUnitName], [GameUnitHP], [GameUnitAttack], [GameUnitDefence], [GameUnitGoldIncome], [GameUnitGoldOutcome]) VALUES (2, 2, N'AttackAircraft                          ', 5000, 250, 5, 0, 10)
INSERT INTO [dbo].[GameUnitsTable] ([GameUnitId], [GameUnitType], [GameUnitName], [GameUnitHP], [GameUnitAttack], [GameUnitDefence], [GameUnitGoldIncome], [GameUnitGoldOutcome]) VALUES (3, 3, N'Warrior                                 ', 500, 50, 2, 0, 10)
SET IDENTITY_INSERT [dbo].[GameUnitsTable] OFF

SET IDENTITY_INSERT [dbo].[GameBuildingsTypeTable] ON
INSERT INTO [dbo].[GameBuildingsTypeTable] ([GameBuildingTypeId], [GameBuildingTypeName]) VALUES (1, N'ProductionAttack    ')
INSERT INTO [dbo].[GameBuildingsTypeTable] ([GameBuildingTypeId], [GameBuildingTypeName]) VALUES (2, N'Defence             ')
INSERT INTO [dbo].[GameBuildingsTypeTable] ([GameBuildingTypeId], [GameBuildingTypeName]) VALUES (3, N'Production          ')
INSERT INTO [dbo].[GameBuildingsTypeTable] ([GameBuildingTypeId], [GameBuildingTypeName]) VALUES (4, N'Core                ')
SET IDENTITY_INSERT [dbo].[GameBuildingsTypeTable] OFF

SET IDENTITY_INSERT [dbo].[GameBuildingsTable] ON
INSERT INTO [dbo].[GameBuildingsTable] ([GameBuildingId], [GameBuildingType], [GameBuildingName], [GameBuildingHP], [GameBuildingAttack], [GameBuildingDefence], [GameBuildingGoldIncome], [GameBuildingGoldOutcome], [Capacity]) VALUES (1, 4, N'Core                                    ', 10000, 0, 20, 0, 10, 1)
INSERT INTO [dbo].[GameBuildingsTable] ([GameBuildingId], [GameBuildingType], [GameBuildingName], [GameBuildingHP], [GameBuildingAttack], [GameBuildingDefence], [GameBuildingGoldIncome], [GameBuildingGoldOutcome], [Capacity]) VALUES (3, 3, N'GoldMining                              ', 2500, 0, 0, 0, 0, 10)
INSERT INTO [dbo].[GameBuildingsTable] ([GameBuildingId], [GameBuildingType], [GameBuildingName], [GameBuildingHP], [GameBuildingAttack], [GameBuildingDefence], [GameBuildingGoldIncome], [GameBuildingGoldOutcome], [Capacity]) VALUES (5, 2, N'DefenceTower                            ', 12500, 200, 15, 0, 10, 2)
INSERT INTO [dbo].[GameBuildingsTable] ([GameBuildingId], [GameBuildingType], [GameBuildingName], [GameBuildingHP], [GameBuildingAttack], [GameBuildingDefence], [GameBuildingGoldIncome], [GameBuildingGoldOutcome], [Capacity]) VALUES (7, 1, N'Casern                                  ', 7500, 0, 15, 0, 10, 10)
SET IDENTITY_INSERT [dbo].[GameBuildingsTable] OFF
