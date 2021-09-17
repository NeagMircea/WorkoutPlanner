CREATE PROCEDURE [dbo].[spExerciseInsert]
	@ExerciseName VARCHAR(50),
	@VideoPath VARCHAR(200),
	@CategoryId INT,
	@Sets INT = 0,
	@MinReps INT = 0,
	@MaxReps INT = 0,
	@Duration FLOAT = 0,
	@Id INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [Exercises]([ExerciseName], [VideoPath], [Sets], [MinReps], [MaxReps], [Duration])
	VALUES(@ExerciseName, @VideoPath, @Sets, @MinReps, @MaxReps, @Duration);

	DECLARE @scopeId AS INT;
	SET @scopeId = SCOPE_IDENTITY();

	INSERT INTO [ExercisesCategories]([fk_ExerciseId], [fk_CategoryId])
	VALUES (@scopeId, @CategoryId);

	SELECT @Id = @scopeId;
END;
