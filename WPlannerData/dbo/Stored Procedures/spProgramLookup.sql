CREATE PROCEDURE [dbo].[spProgramLookup]
	@Id INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [Id], [Name]
	FROM [dbo].[Programs]
	WHERE Id = @Id;
END
