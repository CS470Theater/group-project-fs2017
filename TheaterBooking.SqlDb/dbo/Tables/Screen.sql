CREATE TABLE [dbo].[Screen] (
    [Screen_ID]  INT      IDENTITY (1, 1) NOT NULL,
    [Theater_ID] INT      NOT NULL,
    [Capacity]   SMALLINT NOT NULL,
    CONSTRAINT [PK_Screen] PRIMARY KEY CLUSTERED ([Screen_ID] ASC),
    CONSTRAINT [FK_Screen_Theater] FOREIGN KEY ([Theater_ID]) REFERENCES [dbo].[Theater] ([Theater_ID])
);

