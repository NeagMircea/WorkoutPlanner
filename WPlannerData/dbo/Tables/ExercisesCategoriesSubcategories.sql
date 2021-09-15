CREATE TABLE [dbo].[ExercisesCategoriesSubcategories]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [fk_ExerciseId] INT NOT NULL, 
    [fk_CategoryId] INT NOT NULL, 
    [fk_SubcategoryId] INT NOT NULL, 

    CONSTRAINT [FK_ExercisesCategoriesSubcategories_ToExercises] FOREIGN KEY ([fk_ExerciseId]) REFERENCES [Exercises]([ExerciseId])
	ON UPDATE NO ACTION ON DELETE CASCADE, 

    CONSTRAINT [FK_ExercisesCategoriesSubcategories_ToCategories] FOREIGN KEY ([fk_CategoryId]) REFERENCES [Categories]([CategoryId])
	ON UPDATE NO ACTION ON DELETE CASCADE, 

    CONSTRAINT [FK_ExercisesCategoriesSubcategories_ToSubcategories] FOREIGN KEY ([fk_SubcategoryId]) REFERENCES [Subcategories]([SubcategoryId])
	ON UPDATE NO ACTION ON DELETE CASCADE






	
)
