CREATE TABLE [dbo].[Ticket] (
    [Ticket_ID]   INT IDENTITY (1, 1) NOT NULL,
    [Booking_ID]  INT NOT NULL,
    [Showtime_ID] INT NOT NULL,
    [Price_ID]    INT NOT NULL,
    CONSTRAINT [PK_Ticket] PRIMARY KEY CLUSTERED ([Ticket_ID] ASC),
    CONSTRAINT [FK_Ticket_Booking] FOREIGN KEY ([Booking_ID]) REFERENCES [dbo].[Booking] ([Booking_ID]),
    CONSTRAINT [FK_Ticket_Price] FOREIGN KEY ([Price_ID]) REFERENCES [dbo].[Price] ([Price_ID]),
    CONSTRAINT [FK_Ticket_Showtime] FOREIGN KEY ([Showtime_ID]) REFERENCES [dbo].[Showtime] ([Showtime_ID])
);

