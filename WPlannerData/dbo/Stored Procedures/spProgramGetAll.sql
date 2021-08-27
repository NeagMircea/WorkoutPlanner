CREATE PROCEDURE [dbo].[spProgramGetAll]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [Id], [Name], [ProgramOrder]
	FROM [dbo].[Programs]
	ORDER BY [ProgramOrder];
END;
