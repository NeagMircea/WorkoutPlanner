CREATE PROCEDURE [dbo].[spWorkoutInsert]
	@Name VARCHAR(100)
	--@Id INT OUTPUT	
AS
BEGIN 
	SET NOCOUNT ON;

	INSERT INTO [dbo].[Workouts]([WorkoutName])
	VALUES(@Name);

	DECLARE @scopeId INT;
	SET @scopeId = SCOPE_IDENTITY();

	UPDATE [dbo].[Workouts]
	SET [WorkoutOrder] = @scopeId
	WHERE [WorkoutId] = @scopeId

	--EXECUTE spWorkoutDaysInsert @WorkoutId = @scopeId;

	--SELECT @Id = SCOPE_IDENTITY();
END
