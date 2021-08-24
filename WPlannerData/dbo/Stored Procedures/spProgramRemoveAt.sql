CREATE PROCEDURE [dbo].[spProgramRemoveAt]
	@Id INT = 0
AS
BEGIN
	DELETE 
	FROM [dbo].[Programs]
	WHERE Id = @Id;
END;

