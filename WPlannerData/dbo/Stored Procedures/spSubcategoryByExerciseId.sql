CREATE PROCEDURE [dbo].[spSubcategoryByExerciseId]
	@ExerciseId INT
AS
BEGIN 
	SET NOCOUNT ON;

	SELECT [SubcategoryId], [SubcategoryName], [SubcategoryInfo]
	FROM [Exercises_Categories_Subcategories]
	WHERE [ExerciseId] = @ExerciseId;
END;
