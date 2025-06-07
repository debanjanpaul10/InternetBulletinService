CREATE TABLE [dbo].[AiUsages]
(
  [Id] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
  [UserName] NVARCHAR (MAX) NOT NULL,
  [Usage] NVARCHAR (MAX) NOT NULL,
  [UsageTime] DATETIME NOT NULL,
  [TotalTokensConsumed] INT NULL,
  [CandidatesTokenCount] INT NULL,
  [PromptTokenCount] INT NULL,
  [IsActive] BIT NOT NULL DEFAULT 1
)
