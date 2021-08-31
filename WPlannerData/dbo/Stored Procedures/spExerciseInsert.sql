CREATE PROCEDURE [dbo].[spExerciseInsert]
	@ExerciseId INT = 0,
	@ExerciseName VARCHAR(50),
	@VideoPath VARCHAR(200),
	@CategoryId INT,
	@Sets INT = 0,
	@Reps INT = 0,
	@Duration FLOAT = 0
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [Exercises]([ExerciseName], [VideoPath], [Sets], [Reps], [Duration])
	VALUES(@ExerciseName, @VideoPath, @Sets, @Reps, @Duration);

	INSERT INTO [ExercisesCategories]([fk_ExerciseId], [fk_CategoryId])
	VALUES (SCOPE_IDENTITY(), @CategoryId);
END;
