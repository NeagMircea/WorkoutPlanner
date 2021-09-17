CREATE PROCEDURE [dbo].[spExercisesGetAll]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [ExerciseId], [ExerciseName], [VideoPath], [Sets], [MinReps], [MaxReps], [Duration]
	FROM [Exercises];
END;
