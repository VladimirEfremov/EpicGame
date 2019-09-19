CREATE TABLE [dbo].[UserRelationTable]
(
	[UserId] INT NOT NULL, 
    [Relation] INT NOT NULL, 
    [List] TEXT NULL, 
    CONSTRAINT [FK_UserRelationTable_UserTable] FOREIGN KEY ([UserId]) REFERENCES [UserTable]([Id])
)
