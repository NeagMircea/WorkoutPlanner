CREATE PROCEDURE [dbo].[spExercisesGetByCatSubcatId]
	@CategoryId INT,
	@SubcategoryId INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT MIN([ExerciseId]), [ExerciseName], [VideoPath], [Sets], [MinReps], [MaxReps], [Duration]
	FROM [Exercises_Categories_Subcategories]
	WHERE [CategoryId] = @CategoryId AND [SubcategoryId] IN (@SubcategoryId)
	GROUP BY [ExerciseId], [ExerciseName], [VideoPath], [Sets], [MinReps], [MaxReps], [Duration];
END;
