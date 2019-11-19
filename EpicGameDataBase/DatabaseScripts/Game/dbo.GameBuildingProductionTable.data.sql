USE [EpicGameDataBase]
GO

/****** Объект: Table [dbo].[GameUnitsTable] Дата скрипта: 19.11.2019 23:44:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[GameUnitsTable] (
    [GameUnitId]          INT        IDENTITY (1, 1) NOT NULL,
    [GameUnitType]        INT        NOT NULL,
    [GameUnitName]        NCHAR (40) NOT NULL,
    [GameUnitHP]          INT        NOT NULL,
    [GameUnitAttack]      INT        NOT NULL,
    [GameUnitDefence]     INT        NOT NULL,
    [GameUnitGoldIncome]  INT        NOT NULL,
    [GameUnitGoldOutcome] INT        NOT NULL
);


