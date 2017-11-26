CREATE TABLE [dbo].[Movie] (
    [Movie_ID]          INT            IDENTITY (1, 1) NOT NULL,
    [Rating_ID]         INT            NOT NULL,
    [Movie_Name]        NVARCHAR (50)  NOT NULL,
    [Movie_Desc]        NVARCHAR (255) NOT NULL,
    [Movie_Poster_URL]  NVARCHAR (255) NOT NULL,
    [Date_Start]        DATE           NOT NULL,
    [Date_End]          DATE           NOT NULL,
    [Duration_Hour]     TINYINT        NOT NULL,
    [Duration_Minute]   TINYINT        NOT NULL,
    [Movie_Trailer_URL] NVARCHAR (255) NOT NULL,
    CONSTRAINT [PK_Movie] PRIMARY KEY CLUSTERED ([Movie_ID] ASC),
    CONSTRAINT [FK_Movie_Rating] FOREIGN KEY ([Rating_ID]) REFERENCES [dbo].[Rating] ([Rating_ID])
);

