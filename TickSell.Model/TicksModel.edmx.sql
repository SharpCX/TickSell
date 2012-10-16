
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 10/16/2012 10:37:00
-- Generated from EDMX file: C:\Users\陈响\documents\visual studio 2012\Projects\TickSell\TickSell.Model\TicksModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ticks];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CellCell]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Cells] DROP CONSTRAINT [FK_CellCell];
GO
IF OBJECT_ID(N'[dbo].[FK_Cells_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Cells] DROP CONSTRAINT [FK_Cells_Users];
GO
IF OBJECT_ID(N'[dbo].[FK_Seats_Cells]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Seats] DROP CONSTRAINT [FK_Seats_Cells];
GO
IF OBJECT_ID(N'[dbo].[FK_Seats_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Seats] DROP CONSTRAINT [FK_Seats_Users];
GO
IF OBJECT_ID(N'[dbo].[FK_Users_Users1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_Users_Users1];
GO
IF OBJECT_ID(N'[dbo].[FK_TimeCells_Cells]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TimeCells] DROP CONSTRAINT [FK_TimeCells_Cells];
GO
IF OBJECT_ID(N'[dbo].[FK_TimeCellSeats_TimeCells]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TimeCellSeats] DROP CONSTRAINT [FK_TimeCellSeats_TimeCells];
GO
IF OBJECT_ID(N'[dbo].[FK_UserTimeCellSeat]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TimeCellSeats] DROP CONSTRAINT [FK_UserTimeCellSeat];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Cells]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Cells];
GO
IF OBJECT_ID(N'[dbo].[Seats]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Seats];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[TimeCells]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TimeCells];
GO
IF OBJECT_ID(N'[dbo].[TimeCellSeats]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TimeCellSeats];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[SeatTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SeatTypes];
GO
IF OBJECT_ID(N'[dbo].[TickTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TickTypes];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Cells'
CREATE TABLE [dbo].[Cells] (
    [ID] uniqueidentifier  NOT NULL,
    [CellName] nvarchar(50)  NOT NULL,
    [CellText] nvarchar(max)  NOT NULL,
    [CreatDate] nvarchar(50)  NOT NULL,
    [CreaterId] uniqueidentifier  NULL,
    [ColNum] int  NOT NULL,
    [RowNum] int  NOT NULL,
    [Father] uniqueidentifier  NULL
);
GO

-- Creating table 'Seats'
CREATE TABLE [dbo].[Seats] (
    [ID] uniqueidentifier  NOT NULL,
    [SeatName] nvarchar(max)  NOT NULL,
    [IsSold] bit  NOT NULL,
    [IsUsing] bit  NOT NULL,
    [CreaterId] uniqueidentifier  NULL,
    [CreatDate] nvarchar(50)  NOT NULL,
    [CellsId] uniqueidentifier  NULL,
    [RowIndex] int  NOT NULL,
    [ColIndex] int  NOT NULL,
    [SeatIndex] int  NOT NULL,
    [SeatType] nvarchar(max)  NULL,
    [TicketPrice] float  NULL,
    [TicketType] nvarchar(max)  NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'TimeCells'
CREATE TABLE [dbo].[TimeCells] (
    [ID] uniqueidentifier  NOT NULL,
    [TimeBe] datetime  NOT NULL,
    [TimeEn] datetime  NOT NULL,
    [MovieName] nvarchar(max)  NOT NULL,
    [CellID] uniqueidentifier  NULL,
    [ColNum] int  NOT NULL,
    [RowNum] int  NOT NULL,
    [ShowTimes] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'TimeCellSeats'
CREATE TABLE [dbo].[TimeCellSeats] (
    [ID] uniqueidentifier  NOT NULL,
    [SeatName] nvarchar(max)  NOT NULL,
    [IsSold] bit  NOT NULL,
    [IsUsing] bit  NOT NULL,
    [CreatDate] nvarchar(50)  NOT NULL,
    [RowIndex] int  NOT NULL,
    [ColIndex] int  NOT NULL,
    [SeatIndex] int  NOT NULL,
    [TimeCellID] uniqueidentifier  NULL,
    [SeatType] nvarchar(max)  NULL,
    [TicketType] nvarchar(max)  NULL,
    [TicketPrice] float  NULL,
    [SoldUserID] uniqueidentifier  NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [ID] uniqueidentifier  NOT NULL,
    [UserName] nvarchar(50)  NOT NULL,
    [CreatDate] nvarchar(max)  NOT NULL,
    [UserLevel] nvarchar(50)  NOT NULL,
    [CreaterId] uniqueidentifier  NULL,
    [UserPassword] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'SeatTypes'
CREATE TABLE [dbo].[SeatTypes] (
    [ID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Price] float  NOT NULL,
    [CreatDate] datetime  NOT NULL
);
GO

-- Creating table 'TickTypes'
CREATE TABLE [dbo].[TickTypes] (
    [ID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [CreatDate] datetime  NOT NULL,
    [Price] float  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'Cells'
ALTER TABLE [dbo].[Cells]
ADD CONSTRAINT [PK_Cells]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Seats'
ALTER TABLE [dbo].[Seats]
ADD CONSTRAINT [PK_Seats]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [ID] in table 'TimeCells'
ALTER TABLE [dbo].[TimeCells]
ADD CONSTRAINT [PK_TimeCells]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'TimeCellSeats'
ALTER TABLE [dbo].[TimeCellSeats]
ADD CONSTRAINT [PK_TimeCellSeats]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'SeatTypes'
ALTER TABLE [dbo].[SeatTypes]
ADD CONSTRAINT [PK_SeatTypes]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'TickTypes'
ALTER TABLE [dbo].[TickTypes]
ADD CONSTRAINT [PK_TickTypes]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Father] in table 'Cells'
ALTER TABLE [dbo].[Cells]
ADD CONSTRAINT [FK_CellCell]
    FOREIGN KEY ([Father])
    REFERENCES [dbo].[Cells]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CellCell'
CREATE INDEX [IX_FK_CellCell]
ON [dbo].[Cells]
    ([Father]);
GO

-- Creating foreign key on [CreaterId] in table 'Cells'
ALTER TABLE [dbo].[Cells]
ADD CONSTRAINT [FK_Cells_Users]
    FOREIGN KEY ([CreaterId])
    REFERENCES [dbo].[Users]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Cells_Users'
CREATE INDEX [IX_FK_Cells_Users]
ON [dbo].[Cells]
    ([CreaterId]);
GO

-- Creating foreign key on [CellsId] in table 'Seats'
ALTER TABLE [dbo].[Seats]
ADD CONSTRAINT [FK_Seats_Cells]
    FOREIGN KEY ([CellsId])
    REFERENCES [dbo].[Cells]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Seats_Cells'
CREATE INDEX [IX_FK_Seats_Cells]
ON [dbo].[Seats]
    ([CellsId]);
GO

-- Creating foreign key on [CreaterId] in table 'Seats'
ALTER TABLE [dbo].[Seats]
ADD CONSTRAINT [FK_Seats_Users]
    FOREIGN KEY ([CreaterId])
    REFERENCES [dbo].[Users]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Seats_Users'
CREATE INDEX [IX_FK_Seats_Users]
ON [dbo].[Seats]
    ([CreaterId]);
GO

-- Creating foreign key on [CreaterId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_Users_Users1]
    FOREIGN KEY ([CreaterId])
    REFERENCES [dbo].[Users]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Users_Users1'
CREATE INDEX [IX_FK_Users_Users1]
ON [dbo].[Users]
    ([CreaterId]);
GO

-- Creating foreign key on [CellID] in table 'TimeCells'
ALTER TABLE [dbo].[TimeCells]
ADD CONSTRAINT [FK_TimeCells_Cells]
    FOREIGN KEY ([CellID])
    REFERENCES [dbo].[Cells]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TimeCells_Cells'
CREATE INDEX [IX_FK_TimeCells_Cells]
ON [dbo].[TimeCells]
    ([CellID]);
GO

-- Creating foreign key on [TimeCellID] in table 'TimeCellSeats'
ALTER TABLE [dbo].[TimeCellSeats]
ADD CONSTRAINT [FK_TimeCellSeats_TimeCells]
    FOREIGN KEY ([TimeCellID])
    REFERENCES [dbo].[TimeCells]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TimeCellSeats_TimeCells'
CREATE INDEX [IX_FK_TimeCellSeats_TimeCells]
ON [dbo].[TimeCellSeats]
    ([TimeCellID]);
GO

-- Creating foreign key on [SoldUserID] in table 'TimeCellSeats'
ALTER TABLE [dbo].[TimeCellSeats]
ADD CONSTRAINT [FK_UserTimeCellSeat]
    FOREIGN KEY ([SoldUserID])
    REFERENCES [dbo].[Users]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserTimeCellSeat'
CREATE INDEX [IX_FK_UserTimeCellSeat]
ON [dbo].[TimeCellSeats]
    ([SoldUserID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------