
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/01/2018 17:53:53
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
IF OBJECT_ID(N'[dbo].[FK_PaintConfiguration]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Configurations] DROP CONSTRAINT [FK_PaintConfiguration];
GO
IF OBJECT_ID(N'[dbo].[FK_EngineSettingsConfiguration]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Configurations] DROP CONSTRAINT [FK_EngineSettingsConfiguration];
GO
IF OBJECT_ID(N'[dbo].[FK_ConfigurationAccessory_Configuration]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ConfigurationAccessory] DROP CONSTRAINT [FK_ConfigurationAccessory_Configuration];
GO
IF OBJECT_ID(N'[dbo].[FK_ConfigurationAccessory_Accessory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ConfigurationAccessory] DROP CONSTRAINT [FK_ConfigurationAccessory_Accessory];
GO
IF OBJECT_ID(N'[dbo].[FK_RimConfiguration]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Configurations] DROP CONSTRAINT [FK_RimConfiguration];
GO
IF OBJECT_ID(N'[dbo].[FK_ConfigurationOrder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Configurations] DROP CONSTRAINT [FK_ConfigurationOrder];
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
IF OBJECT_ID(N'[dbo].[Configurations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Configurations];
GO
IF OBJECT_ID(N'[dbo].[Orders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Orders];
GO
IF OBJECT_ID(N'[dbo].[ConfigurationAccessory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ConfigurationAccessory];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Accessories'
CREATE TABLE [dbo].[Accessories] (
    [Id] int  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Price] float  NOT NULL,
    [Category] int  NOT NULL,
    [SubCategory] int  NOT NULL
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
    [Engine_Id] int  NOT NULL,
    [CarModel_Id] int  NOT NULL
);
GO

-- Creating table 'Paints'
CREATE TABLE [dbo].[Paints] (
    [Id] int  NOT NULL,
    [Color] nvarchar(max)  NOT NULL,
    [Price] float  NOT NULL,
    [Category] int  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [IsDefault] bit  NULL
);
GO

-- Creating table 'Rims'
CREATE TABLE [dbo].[Rims] (
    [Id] int  NOT NULL,
    [Price] float  NOT NULL,
    [Size] int  NOT NULL,
    [IsDefault] bit  NULL
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

-- Creating table 'Configurations'
CREATE TABLE [dbo].[Configurations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Paint_Id] int  NOT NULL,
    [EngineSetting_Id] int  NOT NULL,
    [Rims_Id] int  NOT NULL,
    [ConfigurationOrder_Configuration_Id] int  NOT NULL
);
GO

-- Creating table 'Orders'
CREATE TABLE [dbo].[Orders] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(max)  NULL,
    [Guid] nvarchar(max)  NOT NULL,
    [ExtrasPrice] float  NOT NULL,
    [BasePrice] float  NOT NULL
);
GO

-- Creating table 'ConfigurationAccessory'
CREATE TABLE [dbo].[ConfigurationAccessory] (
    [ConfigurationAccessory_Accessory_Id] int  NOT NULL,
    [Accessories_Id] int  NOT NULL
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

-- Creating primary key on [Id] in table 'Configurations'
ALTER TABLE [dbo].[Configurations]
ADD CONSTRAINT [PK_Configurations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [PK_Orders]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [ConfigurationAccessory_Accessory_Id], [Accessories_Id] in table 'ConfigurationAccessory'
ALTER TABLE [dbo].[ConfigurationAccessory]
ADD CONSTRAINT [PK_ConfigurationAccessory]
    PRIMARY KEY CLUSTERED ([ConfigurationAccessory_Accessory_Id], [Accessories_Id] ASC);
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

-- Creating foreign key on [Paint_Id] in table 'Configurations'
ALTER TABLE [dbo].[Configurations]
ADD CONSTRAINT [FK_PaintConfiguration]
    FOREIGN KEY ([Paint_Id])
    REFERENCES [dbo].[Paints]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PaintConfiguration'
CREATE INDEX [IX_FK_PaintConfiguration]
ON [dbo].[Configurations]
    ([Paint_Id]);
GO

-- Creating foreign key on [EngineSetting_Id] in table 'Configurations'
ALTER TABLE [dbo].[Configurations]
ADD CONSTRAINT [FK_EngineSettingsConfiguration]
    FOREIGN KEY ([EngineSetting_Id])
    REFERENCES [dbo].[EngineSettings]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EngineSettingsConfiguration'
CREATE INDEX [IX_FK_EngineSettingsConfiguration]
ON [dbo].[Configurations]
    ([EngineSetting_Id]);
GO

-- Creating foreign key on [ConfigurationAccessory_Accessory_Id] in table 'ConfigurationAccessory'
ALTER TABLE [dbo].[ConfigurationAccessory]
ADD CONSTRAINT [FK_ConfigurationAccessory_Configuration]
    FOREIGN KEY ([ConfigurationAccessory_Accessory_Id])
    REFERENCES [dbo].[Configurations]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Accessories_Id] in table 'ConfigurationAccessory'
ALTER TABLE [dbo].[ConfigurationAccessory]
ADD CONSTRAINT [FK_ConfigurationAccessory_Accessory]
    FOREIGN KEY ([Accessories_Id])
    REFERENCES [dbo].[Accessories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ConfigurationAccessory_Accessory'
CREATE INDEX [IX_FK_ConfigurationAccessory_Accessory]
ON [dbo].[ConfigurationAccessory]
    ([Accessories_Id]);
GO

-- Creating foreign key on [Rims_Id] in table 'Configurations'
ALTER TABLE [dbo].[Configurations]
ADD CONSTRAINT [FK_RimConfiguration]
    FOREIGN KEY ([Rims_Id])
    REFERENCES [dbo].[Rims]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RimConfiguration'
CREATE INDEX [IX_FK_RimConfiguration]
ON [dbo].[Configurations]
    ([Rims_Id]);
GO

-- Creating foreign key on [ConfigurationOrder_Configuration_Id] in table 'Configurations'
ALTER TABLE [dbo].[Configurations]
ADD CONSTRAINT [FK_ConfigurationOrder]
    FOREIGN KEY ([ConfigurationOrder_Configuration_Id])
    REFERENCES [dbo].[Orders]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ConfigurationOrder'
CREATE INDEX [IX_FK_ConfigurationOrder]
ON [dbo].[Configurations]
    ([ConfigurationOrder_Configuration_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------