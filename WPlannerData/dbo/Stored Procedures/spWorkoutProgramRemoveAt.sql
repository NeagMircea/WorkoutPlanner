CREATE PROCEDURE [dbo].[spWorkoutProgramRemoveAt]
	@ProgramId int = 0,
	@WorkoutId int = 0
AS
BEGIN
	SET NOCOUNT ON;

	DELETE TOP(1)
	FROM [dbo].[WorkoutPrograms]
	WHERE [fk_ProgramId] = @ProgramId AND [fk_WorkoutId] = @WorkoutId;
	
END;

