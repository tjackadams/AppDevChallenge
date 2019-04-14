CREATE PROCEDURE [dbo].[usp_GetAllDevices]
AS
	SELECT
		[Id],
		[Name],
		[Latitude],
		[Longitude],
		[ImageUrl]
	FROM
		[Devices]
