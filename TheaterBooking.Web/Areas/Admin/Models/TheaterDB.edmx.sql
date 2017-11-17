
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/17/2017 16:47:40
-- Generated from EDMX file: C:\Users\Wubbie\Documents\CS470\fs2017-git\group-project-fs2017\TheaterBooking.Web\Areas\Admin\Models\TheaterDB.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [TheaterBooking];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Booking_Customer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Booking] DROP CONSTRAINT [FK_Booking_Customer];
GO
IF OBJECT_ID(N'[TheaterDBModelStoreContainer].[FK_Movie_Genre_Genre]', 'F') IS NOT NULL
    ALTER TABLE [TheaterDBModelStoreContainer].[Movie_Genre] DROP CONSTRAINT [FK_Movie_Genre_Genre];
GO
IF OBJECT_ID(N'[TheaterDBModelStoreContainer].[FK_Movie_Genre_Movie]', 'F') IS NOT NULL
    ALTER TABLE [TheaterDBModelStoreContainer].[Movie_Genre] DROP CONSTRAINT [FK_Movie_Genre_Movie];
GO
IF OBJECT_ID(N'[dbo].[FK_Movie_Rating]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Movie] DROP CONSTRAINT [FK_Movie_Rating];
GO
IF OBJECT_ID(N'[dbo].[FK_Screen_Theater]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Screen] DROP CONSTRAINT [FK_Screen_Theater];
GO
IF OBJECT_ID(N'[dbo].[FK_Showtime_Movie]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Showtime] DROP CONSTRAINT [FK_Showtime_Movie];
GO
IF OBJECT_ID(N'[dbo].[FK_Showtime_Screen]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Showtime] DROP CONSTRAINT [FK_Showtime_Screen];
GO
IF OBJECT_ID(N'[dbo].[FK_Ticket_Booking]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Ticket] DROP CONSTRAINT [FK_Ticket_Booking];
GO
IF OBJECT_ID(N'[dbo].[FK_Ticket_Price]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Ticket] DROP CONSTRAINT [FK_Ticket_Price];
GO
IF OBJECT_ID(N'[dbo].[FK_Ticket_Showtime]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Ticket] DROP CONSTRAINT [FK_Ticket_Showtime];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Booking]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Booking];
GO
IF OBJECT_ID(N'[dbo].[Customer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Customer];
GO
IF OBJECT_ID(N'[dbo].[Genre]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Genre];
GO
IF OBJECT_ID(N'[dbo].[Movie]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Movie];
GO
IF OBJECT_ID(N'[dbo].[Price]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Price];
GO
IF OBJECT_ID(N'[dbo].[Rating]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Rating];
GO
IF OBJECT_ID(N'[dbo].[Screen]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Screen];
GO
IF OBJECT_ID(N'[dbo].[Showtime]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Showtime];
GO
IF OBJECT_ID(N'[dbo].[Theater]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Theater];
GO
IF OBJECT_ID(N'[dbo].[Ticket]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Ticket];
GO
IF OBJECT_ID(N'[TheaterDBModelStoreContainer].[Movie_Genre]', 'U') IS NOT NULL
    DROP TABLE [TheaterDBModelStoreContainer].[Movie_Genre];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Bookings'
CREATE TABLE [dbo].[Bookings] (
    [Booking_ID] int  NOT NULL,
    [Customer_ID] int  NOT NULL,
    [Booking_Date] datetime  NOT NULL
);
GO

-- Creating table 'Customers'
CREATE TABLE [dbo].[Customers] (
    [Customer_ID] int IDENTITY(1,1) NOT NULL,
    [First_Name] nvarchar(50)  NOT NULL,
    [Last_Name] nvarchar(50)  NOT NULL,
    [Username] nvarchar(50)  NOT NULL,
    [Password] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Genres'
CREATE TABLE [dbo].[Genres] (
    [Genre_ID] int IDENTITY(1,1) NOT NULL,
    [Genre_Name] nvarchar(20)  NOT NULL
);
GO

-- Creating table 'Movies'
CREATE TABLE [dbo].[Movies] (
    [Movie_ID] int IDENTITY(1,1) NOT NULL,
    [Rating_ID] int  NOT NULL,
    [Movie_Name] nvarchar(50)  NOT NULL,
    [Movie_Desc] nvarchar(255)  NOT NULL,
    [Movie_Poster] varbinary(max)  NOT NULL,
    [Date_Start] datetime  NOT NULL,
    [Date_End] datetime  NOT NULL,
    [Duration_Hour] tinyint  NOT NULL,
    [Duration_Minute] tinyint  NOT NULL
);
GO

-- Creating table 'Prices'
CREATE TABLE [dbo].[Prices] (
    [Price_ID] int IDENTITY(1,1) NOT NULL,
    [Price_Desc] nvarchar(15)  NOT NULL,
    [Price_Amount] decimal(10,2)  NOT NULL
);
GO

-- Creating table 'Ratings'
CREATE TABLE [dbo].[Ratings] (
    [Rating_ID] int IDENTITY(1,1) NOT NULL,
    [Rating_Symbol] nvarchar(10)  NOT NULL
);
GO

-- Creating table 'Screens'
CREATE TABLE [dbo].[Screens] (
    [Screen_ID] int IDENTITY(1,1) NOT NULL,
    [Theater_ID] int  NOT NULL,
    [Capacity] smallint  NOT NULL
);
GO

-- Creating table 'Showtimes'
CREATE TABLE [dbo].[Showtimes] (
    [Showtime_ID] int  NOT NULL,
    [Screen_ID] int  NOT NULL,
    [Movie_ID] int  NOT NULL,
    [Show_Date] datetime  NOT NULL,
    [Show_Time] time  NOT NULL
);
GO

-- Creating table 'Theaters'
CREATE TABLE [dbo].[Theaters] (
    [Theater_ID] int IDENTITY(1,1) NOT NULL,
    [Theater_Name] nvarchar(50)  NOT NULL,
    [Theater_Owner] nvarchar(50)  NOT NULL,
    [Theater_Address] nvarchar(75)  NOT NULL,
    [Theater_Phone] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Tickets'
CREATE TABLE [dbo].[Tickets] (
    [Ticket_ID] int IDENTITY(1,1) NOT NULL,
    [Booking_ID] int  NOT NULL,
    [Showtime_ID] int  NOT NULL,
    [Price_ID] int  NOT NULL
);
GO

-- Creating table 'Movie_Genre'
CREATE TABLE [dbo].[Movie_Genre] (
    [Genres_Genre_ID] int  NOT NULL,
    [Movies_Movie_ID] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Booking_ID] in table 'Bookings'
ALTER TABLE [dbo].[Bookings]
ADD CONSTRAINT [PK_Bookings]
    PRIMARY KEY CLUSTERED ([Booking_ID] ASC);
GO

-- Creating primary key on [Customer_ID] in table 'Customers'
ALTER TABLE [dbo].[Customers]
ADD CONSTRAINT [PK_Customers]
    PRIMARY KEY CLUSTERED ([Customer_ID] ASC);
GO

-- Creating primary key on [Genre_ID] in table 'Genres'
ALTER TABLE [dbo].[Genres]
ADD CONSTRAINT [PK_Genres]
    PRIMARY KEY CLUSTERED ([Genre_ID] ASC);
GO

-- Creating primary key on [Movie_ID] in table 'Movies'
ALTER TABLE [dbo].[Movies]
ADD CONSTRAINT [PK_Movies]
    PRIMARY KEY CLUSTERED ([Movie_ID] ASC);
GO

-- Creating primary key on [Price_ID] in table 'Prices'
ALTER TABLE [dbo].[Prices]
ADD CONSTRAINT [PK_Prices]
    PRIMARY KEY CLUSTERED ([Price_ID] ASC);
GO

-- Creating primary key on [Rating_ID] in table 'Ratings'
ALTER TABLE [dbo].[Ratings]
ADD CONSTRAINT [PK_Ratings]
    PRIMARY KEY CLUSTERED ([Rating_ID] ASC);
GO

-- Creating primary key on [Screen_ID] in table 'Screens'
ALTER TABLE [dbo].[Screens]
ADD CONSTRAINT [PK_Screens]
    PRIMARY KEY CLUSTERED ([Screen_ID] ASC);
GO

-- Creating primary key on [Showtime_ID] in table 'Showtimes'
ALTER TABLE [dbo].[Showtimes]
ADD CONSTRAINT [PK_Showtimes]
    PRIMARY KEY CLUSTERED ([Showtime_ID] ASC);
GO

-- Creating primary key on [Theater_ID] in table 'Theaters'
ALTER TABLE [dbo].[Theaters]
ADD CONSTRAINT [PK_Theaters]
    PRIMARY KEY CLUSTERED ([Theater_ID] ASC);
GO

-- Creating primary key on [Ticket_ID] in table 'Tickets'
ALTER TABLE [dbo].[Tickets]
ADD CONSTRAINT [PK_Tickets]
    PRIMARY KEY CLUSTERED ([Ticket_ID] ASC);
GO

-- Creating primary key on [Genres_Genre_ID], [Movies_Movie_ID] in table 'Movie_Genre'
ALTER TABLE [dbo].[Movie_Genre]
ADD CONSTRAINT [PK_Movie_Genre]
    PRIMARY KEY CLUSTERED ([Genres_Genre_ID], [Movies_Movie_ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Customer_ID] in table 'Bookings'
ALTER TABLE [dbo].[Bookings]
ADD CONSTRAINT [FK_Booking_Customer]
    FOREIGN KEY ([Customer_ID])
    REFERENCES [dbo].[Customers]
        ([Customer_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Booking_Customer'
CREATE INDEX [IX_FK_Booking_Customer]
ON [dbo].[Bookings]
    ([Customer_ID]);
GO

-- Creating foreign key on [Booking_ID] in table 'Tickets'
ALTER TABLE [dbo].[Tickets]
ADD CONSTRAINT [FK_Ticket_Booking]
    FOREIGN KEY ([Booking_ID])
    REFERENCES [dbo].[Bookings]
        ([Booking_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Ticket_Booking'
CREATE INDEX [IX_FK_Ticket_Booking]
ON [dbo].[Tickets]
    ([Booking_ID]);
GO

-- Creating foreign key on [Rating_ID] in table 'Movies'
ALTER TABLE [dbo].[Movies]
ADD CONSTRAINT [FK_Movie_Rating]
    FOREIGN KEY ([Rating_ID])
    REFERENCES [dbo].[Ratings]
        ([Rating_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Movie_Rating'
CREATE INDEX [IX_FK_Movie_Rating]
ON [dbo].[Movies]
    ([Rating_ID]);
GO

-- Creating foreign key on [Movie_ID] in table 'Showtimes'
ALTER TABLE [dbo].[Showtimes]
ADD CONSTRAINT [FK_Showtime_Movie]
    FOREIGN KEY ([Movie_ID])
    REFERENCES [dbo].[Movies]
        ([Movie_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Showtime_Movie'
CREATE INDEX [IX_FK_Showtime_Movie]
ON [dbo].[Showtimes]
    ([Movie_ID]);
GO

-- Creating foreign key on [Price_ID] in table 'Tickets'
ALTER TABLE [dbo].[Tickets]
ADD CONSTRAINT [FK_Ticket_Price]
    FOREIGN KEY ([Price_ID])
    REFERENCES [dbo].[Prices]
        ([Price_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Ticket_Price'
CREATE INDEX [IX_FK_Ticket_Price]
ON [dbo].[Tickets]
    ([Price_ID]);
GO

-- Creating foreign key on [Theater_ID] in table 'Screens'
ALTER TABLE [dbo].[Screens]
ADD CONSTRAINT [FK_Screen_Theater]
    FOREIGN KEY ([Theater_ID])
    REFERENCES [dbo].[Theaters]
        ([Theater_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Screen_Theater'
CREATE INDEX [IX_FK_Screen_Theater]
ON [dbo].[Screens]
    ([Theater_ID]);
GO

-- Creating foreign key on [Screen_ID] in table 'Showtimes'
ALTER TABLE [dbo].[Showtimes]
ADD CONSTRAINT [FK_Showtime_Screen]
    FOREIGN KEY ([Screen_ID])
    REFERENCES [dbo].[Screens]
        ([Screen_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Showtime_Screen'
CREATE INDEX [IX_FK_Showtime_Screen]
ON [dbo].[Showtimes]
    ([Screen_ID]);
GO

-- Creating foreign key on [Showtime_ID] in table 'Tickets'
ALTER TABLE [dbo].[Tickets]
ADD CONSTRAINT [FK_Ticket_Showtime]
    FOREIGN KEY ([Showtime_ID])
    REFERENCES [dbo].[Showtimes]
        ([Showtime_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Ticket_Showtime'
CREATE INDEX [IX_FK_Ticket_Showtime]
ON [dbo].[Tickets]
    ([Showtime_ID]);
GO

-- Creating foreign key on [Genres_Genre_ID] in table 'Movie_Genre'
ALTER TABLE [dbo].[Movie_Genre]
ADD CONSTRAINT [FK_Movie_Genre_Genre]
    FOREIGN KEY ([Genres_Genre_ID])
    REFERENCES [dbo].[Genres]
        ([Genre_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Movies_Movie_ID] in table 'Movie_Genre'
ALTER TABLE [dbo].[Movie_Genre]
ADD CONSTRAINT [FK_Movie_Genre_Movie]
    FOREIGN KEY ([Movies_Movie_ID])
    REFERENCES [dbo].[Movies]
        ([Movie_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Movie_Genre_Movie'
CREATE INDEX [IX_FK_Movie_Genre_Movie]
ON [dbo].[Movie_Genre]
    ([Movies_Movie_ID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------