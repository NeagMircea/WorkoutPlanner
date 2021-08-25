CREATE PROCEDURE [dbo].[spWorkoutLookupByProgramId]
	@ProgramId int = 0
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [WorkoutId], [WorkoutName]
	FROM [dbo].[Program_Workouts]
	WHERE [ProgramId] = @ProgramId;
END;
