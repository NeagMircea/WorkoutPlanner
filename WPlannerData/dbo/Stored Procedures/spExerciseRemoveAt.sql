CREATE PROCEDURE [dbo].[spExerciseRemoveAt]
	@Id INT
AS
BEGIN
	SET NOCOUNT ON;

	DELETE 
	FROM [Exercises]
	WHERE [ExerciseId] = @Id;
END;
