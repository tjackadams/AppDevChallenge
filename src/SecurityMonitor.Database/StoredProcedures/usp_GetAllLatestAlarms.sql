CREATE PROCEDURE [dbo].[usp_GetAllLatestAlarms]
AS
SET NOCOUNT ON;
	;WITH SortedDeviceEvents AS
	(
		SELECT
			[DeviceEvents].[EventId],
			[DeviceEvents].[DeviceId],
			[DeviceEvents].[EventTime],
			[DeviceEvents].[Status],
			[DeviceEvents].[ImageUrl],
			[Devices].[Latitude],
			[Devices].[Longitude],
			[Devices].[Name],
			ROW_NUMBER() OVER (PARTITION BY [DeviceEvents].[DeviceId] ORDER BY [DeviceEvents].[EventTime] DESC) AS RowNumber
		FROM
			[DeviceEvents]	
		INNER JOIN [Devices] ON [DeviceEvents].[DeviceId] = [Devices].[DeviceId]
	)
	SELECT
		[EventId] as [AlarmId],
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
