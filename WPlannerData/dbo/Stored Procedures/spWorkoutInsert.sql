CREATE PROCEDURE [dbo].[spWorkoutInsert]
	@Name VARCHAR(100),
	@Id INT OUTPUT
	
AS
BEGIN 
	SET NOCOUNT ON;

	INSERT INTO [dbo].[Workouts]([WorkoutName])
	VALUES(@Name);

	UPDATE [dbo].[Workouts]
	SET [WorkoutOrder] = SCOPE_IDENTITY()
	WHERE [WorkoutId] = SCOPE_IDENTITY();

	SELECT @Id = SCOPE_IDENTITY();

END
