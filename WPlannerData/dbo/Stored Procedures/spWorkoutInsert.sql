CREATE PROCEDURE [dbo].[spWorkoutInsert]
	@Name VARCHAR(100),
	@Id INT OUTPUT
	
AS
BEGIN 
	SET NOCOUNT ON;

	INSERT INTO [dbo].[Workouts]([Name])
	VALUES(@Name);

	SELECT @Id = SCOPE_IDENTITY();

END
