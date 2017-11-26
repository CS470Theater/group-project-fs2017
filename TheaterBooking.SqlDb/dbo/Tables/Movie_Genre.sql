CREATE TABLE [dbo].[Movie_Genre] (
    [Genre_ID] INT NOT NULL,
    [Movie_ID] INT NOT NULL,
    CONSTRAINT [PK_Movie_Genre] PRIMARY KEY CLUSTERED ([Genre_ID] ASC, [Movie_ID] ASC),
    CONSTRAINT [FK_Movie_Genre_Genre] FOREIGN KEY ([Genre_ID]) REFERENCES [dbo].[Genre] ([Genre_ID]),
    CONSTRAINT [FK_Movie_Genre_Movie] FOREIGN KEY ([Movie_ID]) REFERENCES [dbo].[Movie] ([Movie_ID])
);

