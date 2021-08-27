CREATE PROCEDURE [dbo].[spProgramSwapOrder]
	@ProgramOneId int = 0,
	@ProgramOneOrder int = 0,
	@ProgramTwoId int = 0,
	@ProgramTwoOrder int = 0
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE [dbo].[Programs]
	SET [ProgramOrder] = @ProgramTwoOrder
	WHERE [Id] = @ProgramOneId;

	UPDATE [dbo].[Programs]
	SET [ProgramOrder] = @ProgramOneOrder
	WHERE [Id] = @ProgramTwoId;
END;
