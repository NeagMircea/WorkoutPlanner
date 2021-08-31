CREATE PROCEDURE [dbo].[spDaysGetAll]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [DayId], [DayName]
	FROM [Days];
END
