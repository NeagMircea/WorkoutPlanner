CREATE PROCEDURE [dbo].[spExercisesCategoriesSubcategoriesInsert]
	@ExerciseId INT,
	@CategoryId INT,
	@SubcategoryId INT
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [ExercisesCategoriesSubcategories] ([fk_ExerciseId], [fk_CategoryId], [fk_SubcategoryId])
	VALUES(@ExerciseId, @CategoryId, @SubcategoryId);
END;
