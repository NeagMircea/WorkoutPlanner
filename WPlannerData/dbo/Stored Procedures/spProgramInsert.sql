CREATE PROCEDURE [dbo].[spProgramInsert]
	@Id INT = 0,
	@Name VARCHAR(100)
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [dbo].[Programs]([Name])
	VALUES(@Name);

END;
