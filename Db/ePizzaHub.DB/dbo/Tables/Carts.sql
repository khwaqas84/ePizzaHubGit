CREATE TABLE [dbo].[Carts] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [UserId]      INT              NOT NULL,
    [CreatedDate] DATETIME2 (7)    NOT NULL,
    [IsActive]    BIT              NOT NULL,
    CONSTRAINT [PK_Carts] PRIMARY KEY CLUSTERED ([Id] ASC)
);

