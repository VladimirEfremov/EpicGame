CREATE TABLE [dbo].[UserTable]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [FirstName] NCHAR(20) NOT NULL, 
    [SecondName] NCHAR(20) NOT NULL, 
    [FullName] NCHAR(20) NOT NULL, 
    [PasswordHash] NCHAR(100) NULL, 
    [Nickname] NCHAR(50) NULL, 
    [Email] NCHAR(50) NULL
)