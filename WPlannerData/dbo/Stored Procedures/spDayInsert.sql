CREATE PROCEDURE [dbo].[spDayInsert]
AS
BEGIN
	SET NOCOUNT ON;

	IF(NOT EXISTS (SELECT 1 FROM [Days]))
	BEGIN
		INSERT INTO [Days]([DayName])
		VALUES('Monday'),
			  ('Tuesday'),
			  ('Wednesday'),
			  ('Thursday'),
			  ('Friday'),
			  ('Saturday'),
			  ('Sunday')
	END;
END;
