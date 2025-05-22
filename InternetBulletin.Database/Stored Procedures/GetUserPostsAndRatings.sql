CREATE OR ALTER PROCEDURE [dbo].[GetUserPostsAndRatings]
(
  @UserName NVARCHAR(MAX) = ''
)
AS
BEGIN
  SET NOCOUNT ON;
  BEGIN TRY
        
        SELECT P.[PostId], P.[PostTitle], P.[PostCreatedDate], P.[PostOwnerUserName], P.[Ratings]
  FROM dbo.[Posts] AS P (NOLOCK)
  WHERE P.[PostOwnerUserName] = @UserName AND P.[IsActive]=1

        SELECT PR.[PostId], P.[PostTitle], PR.[RatedOn], PR.[RatingValue]
  FROM dbo.[PostRatings] AS PR (NOLOCK)
    INNER JOIN dbo.[Posts] AS P (NOLOCK) ON P.[PostId] = PR.[PostId] AND P.[IsActive]=1
  WHERE PR.[IsActive]=1 AND PR.[UserName] = @UserName

    END TRY
    BEGIN CATCH
    END CATCH
END
