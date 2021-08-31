CREATE PROCEDURE [dbo].[spExercisesGetAll]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [ExerciseId], [ExerciseName], [VideoPath], [Sets], [Reps], [Duration]
	FROM [Exercises];
END;
