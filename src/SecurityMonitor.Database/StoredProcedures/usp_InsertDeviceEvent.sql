CREATE PROCEDURE [dbo].[usp_InsertDeviceEvent]
@DeviceId INT,
@Id UNIQUEIDENTIFIER,
@EventTime DATETIMEOFFSET(7),
@Status INT
AS

SET NOCOUNT ON;

INSERT INTO [dbo].[DeviceEvents] (DeviceId, EventId, EventTime, Status)
VALUES (@DeviceId, @Id, @EventTime, @Status)
