CREATE PROCEDURE [dbo].[spProgramInsert]
	@Name VARCHAR(100)
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [dbo].[Programs]([Name])
	VALUES(@Name);

	UPDATE [dbo].[Programs]
	SET [ProgramOrder] = SCOPE_IDENTITY()
	WHERE [Id] = SCOPE_IDENTITY();

END;
