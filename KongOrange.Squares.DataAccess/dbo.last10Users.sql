CREATE PROCEDURE [dbo].[Last10Users]

AS
	SELECT TOP(10) * 
	FROM dbo.AspNetUsers
	ORDER BY Id Desc
GO