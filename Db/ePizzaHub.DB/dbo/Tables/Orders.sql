CREATE TABLE [dbo].[Orders] (
    [Id]          NVARCHAR (450) NOT NULL,
    [PaymentId]   NVARCHAR (MAX) NOT NULL,
    [Street]      NVARCHAR (MAX) NOT NULL,
    [ZipCode]     NVARCHAR (MAX) NOT NULL,
    [City]        NVARCHAR (MAX) NOT NULL,
    [Locality]    NVARCHAR (MAX) NOT NULL,
    [PhoneNumber] NVARCHAR (MAX) NOT NULL,
    [CreatedDate] DATETIME2 (7)  NOT NULL,
    [UserId]      INT            NOT NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Orders_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]) ON DELETE CASCADE
);

