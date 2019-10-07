SET IDENTITY_INSERT [dbo].[GameUnitsTable] ON
INSERT INTO [dbo].[GameUnitsTable] ([GameUnitId], [GameUnitType], [GameUnitName], [GameUnitHP], [GameUnitAttack], [GameUnitDefence], [GameUnitGoldIncome], [GameUnitGoldOutcome]) VALUES (1, 1, N'Worker                                  ', 250, 0, 0, 25, 10)
INSERT INTO [dbo].[GameUnitsTable] ([GameUnitId], [GameUnitType], [GameUnitName], [GameUnitHP], [GameUnitAttack], [GameUnitDefence], [GameUnitGoldIncome], [GameUnitGoldOutcome]) VALUES (2, 2, N'AttackAircraft                          ', 5000, 250, 5, 0, 10)
INSERT INTO [dbo].[GameUnitsTable] ([GameUnitId], [GameUnitType], [GameUnitName], [GameUnitHP], [GameUnitAttack], [GameUnitDefence], [GameUnitGoldIncome], [GameUnitGoldOutcome]) VALUES (3, 3, N'Warrior                                 ', 500, 50, 2, 0, 10)
SET IDENTITY_INSERT [dbo].[GameUnitsTable] OFF
