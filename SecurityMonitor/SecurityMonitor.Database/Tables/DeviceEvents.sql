CREATE TABLE [dbo].[DeviceEvents]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [DeviceId] INT NOT NULL, 
    [EventTime] DATETIMEOFFSET NOT NULL, 
    [Status] INT NOT NULL, 
    CONSTRAINT [FK_DeviceEvents_Devices] FOREIGN KEY ([DeviceId]) REFERENCES [Devices]([Id])
)
