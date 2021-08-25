CREATE PROCEDURE [dbo].[spWorkoutProgramsInsert]
	@ProgramId INT = 0,
	@WorkoutId INT = 0
AS
BEGIN
	INSERT INTO [dbo].[WorkoutPrograms]([fk_ProgramId], [fk_WorkoutId])
	VALUES(@ProgramId, @WorkoutId);
END;
