
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/19/2018 01:06:30
-- Generated from EDMX file: D:\Documents\Projects\CC-KFZ-Generator\git\KFZ-Konfigurator\KFZ-Konfigurator\Models\CarConfiguratorModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [KFZ-Konfigurator];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_EngineEngineSettings]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EngineSettings] DROP CONSTRAINT [FK_EngineEngineSettings];
GO
IF OBJECT_ID(N'[dbo].[FK_CarModelEngineSettings]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EngineSettings] DROP CONSTRAINT [FK_CarModelEngineSettings];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Accessories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Accessories];
GO
IF OBJECT_ID(N'[dbo].[EngineSettings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EngineSettings];
GO
IF OBJECT_ID(N'[dbo].[Paints]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Paints];
GO
IF OBJECT_ID(N'[dbo].[Rims]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Rims];
GO
IF OBJECT_ID(N'[dbo].[CarModels]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CarModels];
GO
IF OBJECT_ID(N'[dbo].[Engines]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Engines];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Accessories'
CREATE TABLE [dbo].[Accessories] (
    [Id] int  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Price] float  NOT NULL,
    [Category] int  NOT NULL
);
GO

-- Creating table 'EngineSettings'
CREATE TABLE [dbo].[EngineSettings] (
    [Id] int  NOT NULL,
    [Acceleration] float  NOT NULL,
    [Consumption] float  NOT NULL,
    [Price] float  NOT NULL,
    [TopSpeed] int  NOT NULL,
    [Emission] int  NOT NULL,
    [Gears] int  NOT NULL,
    [WheelDrive] int  NOT NULL,
    [Engine_Id] int  NULL,
    [CarModel_Id] int  NULL
);
GO

-- Creating table 'Paints'
CREATE TABLE [dbo].[Paints] (
    [Id] int  NOT NULL,
    [Color] nvarchar(max)  NOT NULL,
    [Price] float  NOT NULL
);
GO

-- Creating table 'Rims'
CREATE TABLE [dbo].[Rims] (
    [Id] int  NOT NULL,
    [Price] float  NOT NULL,
    [Brand] int  NOT NULL,
    [Size] int  NOT NULL,
    [Color] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'CarModels'
CREATE TABLE [dbo].[CarModels] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [BodyType] int  NOT NULL,
    [Year] int  NOT NULL,
    [Series] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Engines'
CREATE TABLE [dbo].[Engines] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Liter] int  NOT NULL,
    [EngineKind] int  NOT NULL,
    [Size] int  NOT NULL,
    [Performance] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Accessories'
ALTER TABLE [dbo].[Accessories]
ADD CONSTRAINT [PK_Accessories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EngineSettings'
ALTER TABLE [dbo].[EngineSettings]
ADD CONSTRAINT [PK_EngineSettings]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Paints'
ALTER TABLE [dbo].[Paints]
ADD CONSTRAINT [PK_Paints]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Rims'
ALTER TABLE [dbo].[Rims]
ADD CONSTRAINT [PK_Rims]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CarModels'
ALTER TABLE [dbo].[CarModels]
ADD CONSTRAINT [PK_CarModels]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Engines'
ALTER TABLE [dbo].[Engines]
ADD CONSTRAINT [PK_Engines]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Engine_Id] in table 'EngineSettings'
ALTER TABLE [dbo].[EngineSettings]
ADD CONSTRAINT [FK_EngineEngineSettings]
    FOREIGN KEY ([Engine_Id])
    REFERENCES [dbo].[Engines]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EngineEngineSettings'
CREATE INDEX [IX_FK_EngineEngineSettings]
ON [dbo].[EngineSettings]
    ([Engine_Id]);
GO

-- Creating foreign key on [CarModel_Id] in table 'EngineSettings'
ALTER TABLE [dbo].[EngineSettings]
ADD CONSTRAINT [FK_CarModelEngineSettings]
    FOREIGN KEY ([CarModel_Id])
    REFERENCES [dbo].[CarModels]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CarModelEngineSettings'
CREATE INDEX [IX_FK_CarModelEngineSettings]
ON [dbo].[EngineSettings]
    ([CarModel_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------