CREATE PROCEDURE [dbo].[spCategorySwapOrder]
	@CategoryOneId int = 0,
	@CategoryOneOrder int = 0,
	@CategoryTwoId int = 0,
	@CategoryTwoOrder int = 0
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE [dbo].[Categories]
	SET [CategoryOrder] = @CategoryTwoOrder
	WHERE [CategoryId] = @CategoryOneId

	UPDATE [dbo].[Categories]
	SET [CategoryOrder] = @CategoryOneOrder
	WHERE [CategoryId] = @CategoryTwoId
END;
