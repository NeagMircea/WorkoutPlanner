CREATE PROCEDURE [dbo].[spWorkoutRemoveAt]
	@Id INT = 0
AS
BEGIN
	SET NOCOUNT ON;

	DELETE 
	FROM [dbo].[Workouts]
	WHERE [Id] = @Id; 
END;
