CREATE PROCEDURE [dbo].[spExerciseInsert]
	@ExerciseName VARCHAR(50),
	@VideoPath VARCHAR(200),
	@CategoryId INT,
	@Sets INT = 0,
	@Reps INT = 0,
	@Duration FLOAT = 0,
	@Id INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [Exercises]([ExerciseName], [VideoPath], [Sets], [Reps], [Duration])
	VALUES(@ExerciseName, @VideoPath, @Sets, @Reps, @Duration);

	DECLARE @scopeId AS INT;
	SET @scopeId = SCOPE_IDENTITY();

	INSERT INTO [ExercisesCategories]([fk_ExerciseId], [fk_CategoryId])
	VALUES (@scopeId, @CategoryId);

	SELECT @Id = @scopeId;
END;
