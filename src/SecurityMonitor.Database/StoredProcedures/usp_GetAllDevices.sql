CREATE PROCEDURE [dbo].[usp_GetAllDevices]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		[Devices].DeviceId,
		[Devices].Latitude,
		[Devices].Longitude,
		[Devices].[Name]
	FROM
		[Devices]

END