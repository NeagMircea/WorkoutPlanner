CREATE PROCEDURE [dbo].[spProgramGetAll]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [Id], [Name] 
	FROM [dbo].[Programs];
END;
