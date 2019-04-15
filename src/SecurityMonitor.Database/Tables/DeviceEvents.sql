CREATE TABLE [dbo].[DeviceEvents]
(
	[EventId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [DeviceId] INT NOT NULL, 
    [EventTime] DATETIMEOFFSET NOT NULL, 
	[ImageUrl] NVARCHAR(200) NULL, 
	[Message] NVARCHAR(500) NULL, 
    [Status] INT NOT NULL    
    CONSTRAINT [FK_DeviceEvents_Devices] FOREIGN KEY ([DeviceId]) REFERENCES [Devices]([DeviceId])
)
