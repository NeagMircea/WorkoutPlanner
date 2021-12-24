CREATE PROCEDURE [dbo].[spWorkoutInsert]
	@Name VARCHAR(100),
	@Description NVARCHAR(3000)
	--@Id INT OUTPUT	
AS
BEGIN 
	SET NOCOUNT ON;

	INSERT INTO [dbo].[Workouts]([WorkoutName], [WorkoutInfo])
	VALUES(@Name, @Description);

	DECLARE @scopeId INT;
	SET @scopeId = SCOPE_IDENTITY();

	UPDATE [dbo].[Workouts]
	SET [WorkoutOrder] = @scopeId
	WHERE [WorkoutId] = @scopeId

	--EXECUTE spWorkoutDaysInsert @WorkoutId = @scopeId;

	--SELECT @Id = SCOPE_IDENTITY();
END
