CREATE PROCEDURE [dbo].[spWorkoutDaysInsert]
	@WorkoutId INT = 0
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [WorkoutDays]([fk_WorkoutId], [fk_DayId])
	VALUES (@WorkoutId, 1),
		   (@WorkoutId, 2),
		   (@WorkoutId, 3),
		   (@WorkoutId, 4),
		   (@WorkoutId, 5),
		   (@WorkoutId, 6),
		   (@WorkoutId, 7)
END;
