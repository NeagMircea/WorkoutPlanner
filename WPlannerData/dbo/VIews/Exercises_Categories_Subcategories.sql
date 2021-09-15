CREATE VIEW [dbo].[Exercises_Categories_Subcategories]
	AS 
	SELECT [Exercises].*, [CategoryId], [CategoryName], [SubcategoryId], [SubcategoryName]
	FROM [ExercisesCategoriesSubcategories]

	JOIN [Exercises] ON [Exercises].[ExerciseId] = [ExercisesCategoriesSubcategories].[fk_ExerciseId]
	JOIN [Categories] ON [Categories].[CategoryId] = [ExercisesCategoriesSubcategories].[fk_CategoryId]
	JOIN [Subcategories] ON [Subcategories].[SubcategoryId] = [ExercisesCategoriesSubcategories].[fk_SubcategoryId];


