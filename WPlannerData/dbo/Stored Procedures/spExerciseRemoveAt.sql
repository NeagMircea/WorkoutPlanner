CREATE PROCEDURE [dbo].[spExerciseRemoveAt]
	@Id INT
AS
BEGIN
	SET NOCOUNT ON;

	DELETE TOP(1)
	FROM [Exercises]
	WHERE [ExerciseId] = @Id;
END;
