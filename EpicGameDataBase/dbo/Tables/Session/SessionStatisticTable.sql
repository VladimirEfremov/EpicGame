CREATE TABLE [dbo].[SessionStatisticTable]
(
	[SessionStatisticId] INT NOT NULL PRIMARY KEY IDENTITY,
	[SessionCoreId] INT NOT NULL,
	[Rating] INT NOT NULL,
	[Wins] INT NOT NULL,
	[Defeats] INT NOT NULL,
	[SuccessfullCoreAttacks] INT NOT NULL,
	[NotSuccessfullCoreAttacks] INT NOT NULL,
	[Scores] INT NOT NULL,
	CONSTRAINT [FK_SessionStatisticTable_SessionCoresTable] FOREIGN KEY ([SessionCoreId]) REFERENCES [SessionCoresTable]([SessionCoreId])
)