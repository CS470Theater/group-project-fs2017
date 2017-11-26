CREATE TABLE [dbo].[Showtime] (
    [Showtime_ID] INT           IDENTITY (1, 1) NOT NULL,
    [Screen_ID]   INT      NOT NULL,
    [Movie_ID]    INT      NOT NULL,
    [Show_Date]   DATE     NOT NULL,
    [Show_Time]   TIME (7) NOT NULL,
    CONSTRAINT [PK_Showtime] PRIMARY KEY CLUSTERED ([Showtime_ID] ASC),
    CONSTRAINT [FK_Showtime_Movie] FOREIGN KEY ([Movie_ID]) REFERENCES [dbo].[Movie] ([Movie_ID]),
    CONSTRAINT [FK_Showtime_Screen] FOREIGN KEY ([Screen_ID]) REFERENCES [dbo].[Screen] ([Screen_ID])
);

