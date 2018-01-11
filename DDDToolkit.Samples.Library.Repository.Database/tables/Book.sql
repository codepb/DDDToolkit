﻿CREATE TABLE [dbo].[Book]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Title] NVARCHAR(50) NOT NULL,
	[Author_FirstName] NVARCHAR(50) NOT NULL,
	[Author_LastName] NVARCHAR(50) NOT NULL,
	[ISBN_Value] NVARCHAR(13) NOT NULL
)