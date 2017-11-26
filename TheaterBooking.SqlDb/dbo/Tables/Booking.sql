CREATE TABLE [dbo].[Booking] (
    [Booking_ID]   INT IDENTITY    NOT NULL,
    [Customer_ID]  NVARCHAR (128)  NOT NULL,
    [Booking_Date] DATE            NOT NULL,
    [Total_Cost]   DECIMAL (10, 2) NOT NULL,
    [Payment_Type] BIT             NOT NULL,
    CONSTRAINT [PK_Booking] PRIMARY KEY CLUSTERED ([Booking_ID] ASC),
    CONSTRAINT [FK_Booking_AspNetUsers] FOREIGN KEY ([Customer_ID]) REFERENCES [dbo].[AspNetUsers] ([Id])
);

