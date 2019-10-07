CREATE TABLE [dbo].[UserFollowersTable]
(
	[UserFollowersId] INT NOT NULL PRIMARY KEY IDENTITY, 
	[UserId] INT NOT NULL,
    [FollowerId] INT NOT NULL,
	CONSTRAINT [FK_UserFollowersTable_UserTable] FOREIGN KEY ([UserId]) REFERENCES [UserTable]([UserId])
)
