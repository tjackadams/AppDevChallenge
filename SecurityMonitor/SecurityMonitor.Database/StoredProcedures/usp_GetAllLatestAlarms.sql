CREATE PROCEDURE [dbo].[usp_GetAllLatestAlarms]
AS
	;WITH SortedDeviceEvents AS
	(
		SELECT
			[DeviceEvents].[Id],
			[DeviceEvents].[DeviceId],
			[DeviceEvents].[EventTime],
			[DeviceEvents].[Status],
			[Devices].[ImageUrl],
			[Devices].[Latitude],
			[Devices].[Longitude],
			[Devices].[Name],
			ROW_NUMBER() OVER (PARTITION BY [DeviceId] ORDER BY [EventTime] DESC) AS RowNumber
		FROM
			[DeviceEvents]	
		INNER JOIN [Devices] ON [DeviceEvents].[DeviceId] = [Devices].[Id]
	)
	SELECT
		[Id] as [AlarmId],
		[DeviceId],
		[EventTime],
		[ImageUrl],
		[Latitude],
		[Longitude],
		[Name],
		[Status]
	FROM
		SortedDeviceEvents
	WHERE
		SortedDeviceEvents.RowNumber = 1
