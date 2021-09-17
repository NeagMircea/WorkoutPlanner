CREATE PROCEDURE [dbo].[spExercisesGetByCategoryId]
	@CategoryId int = 0
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [ExerciseId], [ExerciseName], [VideoPath], [Sets], [MinReps], [MaxReps], [Duration]
	FROM [Exercise_Categories]
	WHERE [CategoryId] = @CategoryId;

END;
