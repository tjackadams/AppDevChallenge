CREATE TABLE [dbo].[Devices]
(
	[DeviceId] INT NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(200) NOT NULL, 
    [Latitude] FLOAT NOT NULL, 
    [Longitude] FLOAT NOT NULL 
)
