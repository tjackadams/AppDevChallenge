CREATE PROCEDURE [dbo].[usp_GetAllLatestDeviceEvents]
AS
	;WITH SortedDeviceEvents AS
	(
		SELECT
			[Id],
			[DeviceId],
			[EventTime],
			[Status],
			ROW_NUMBER() OVER (PARTITION BY [DeviceId] ORDER BY [EventTime] DESC) AS RowNumber
		FROM
			[DeviceEvents]		 
	)
	SELECT
		[Id],
		[DeviceId],
		[EventTime],
		[Status]
	FROM
		SortedDeviceEvents
	WHERE
		SortedDeviceEvents.RowNumber = 1
