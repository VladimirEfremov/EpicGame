CREATE TABLE [dbo].[UserFriendsTable]
(
	[UserFriendId] INT NOT NULL PRIMARY KEY IDENTITY, 
	[UserId] INT NOT NULL,
    [FriendId] INT NOT NULL
	CONSTRAINT [FK_UserFriendsTable_UserTable] FOREIGN KEY ([UserId]) REFERENCES [UserTable]([Id])
)
