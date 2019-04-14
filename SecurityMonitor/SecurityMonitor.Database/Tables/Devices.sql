CREATE TABLE [dbo].[Devices]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(200) NOT NULL, 
    [Latitude] FLOAT NOT NULL, 
    [Longitude] FLOAT NOT NULL, 
    [ImageUrl] NVARCHAR(200) NOT NULL
)
