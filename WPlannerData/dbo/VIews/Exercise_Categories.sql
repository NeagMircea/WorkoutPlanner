CREATE VIEW [dbo].[Exercise_Categories]
AS

SELECT [Exercises].[ExerciseId], [Exercises].[ExerciseName], [Exercises].VideoPath, 
[Exercises].[Sets], [Exercises].[Reps], [Exercises].[Duration], [Categories].[CategoryId],
[Categories].[CategoryName]

FROM [Exercises]

INNER JOIN [ExercisesCategories] ON [Exercises].[ExerciseId] = [ExercisesCategories].[fk_ExerciseId]
INNER JOIN [Categories] ON [ExercisesCategories].[fk_CategoryId] = [Categories].[CategoryId]