CREATE TABLE [dbo].[UserFollowingTable]
(
	[UserFollowingId] INT NOT NULL PRIMARY KEY IDENTITY, 
	[UserId] INT NOT NULL,
    [FollowingId] INT NOT NULL,
	CONSTRAINT [FK_UserFollowingTable_UserTable] FOREIGN KEY ([UserId]) REFERENCES [UserTable]([UserId])
)
