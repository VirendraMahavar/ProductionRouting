USE [master]
GO

/****** Object:  Database [ProductionRoutingDb]    Script Date: 19-02-2026 15:13:06 ******/
CREATE DATABASE [ProductionRoutingDb]
 CONTAINMENT = NONE
 
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ProductionRoutingDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [ProductionRoutingDb] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [ProductionRoutingDb] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [ProductionRoutingDb] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [ProductionRoutingDb] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [ProductionRoutingDb] SET ARITHABORT OFF 
GO

ALTER DATABASE [ProductionRoutingDb] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [ProductionRoutingDb] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [ProductionRoutingDb] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [ProductionRoutingDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [ProductionRoutingDb] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [ProductionRoutingDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [ProductionRoutingDb] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [ProductionRoutingDb] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [ProductionRoutingDb] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [ProductionRoutingDb] SET  DISABLE_BROKER 
GO

ALTER DATABASE [ProductionRoutingDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [ProductionRoutingDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [ProductionRoutingDb] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [ProductionRoutingDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [ProductionRoutingDb] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [ProductionRoutingDb] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [ProductionRoutingDb] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [ProductionRoutingDb] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [ProductionRoutingDb] SET  MULTI_USER 
GO

ALTER DATABASE [ProductionRoutingDb] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [ProductionRoutingDb] SET DB_CHAINING OFF 
GO

ALTER DATABASE [ProductionRoutingDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [ProductionRoutingDb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [ProductionRoutingDb] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [ProductionRoutingDb] SET  READ_WRITE 
GO

------------------------------------------------------------------------------------------

USE [ProductionRoutingDb]
GO

/****** Object:  Table [dbo].[Rulesets]    Script Date: 19-02-2026 15:17:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Rulesets](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Priority] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Rulesets] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
-------------------------------------------------------------------------------------------------

USE [ProductionRoutingDb]
GO

/****** Object:  Table [dbo].[Rules]    Script Date: 19-02-2026 15:19:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Rules](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[ProductionPlant] [nvarchar](max) NOT NULL,
	[Priority] [int] NOT NULL,
	[RulesetId] [int] NULL,
 CONSTRAINT [PK_Rules] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[Rules]  WITH CHECK ADD  CONSTRAINT [FK_Rules_Rulesets_RulesetId] FOREIGN KEY([RulesetId])
REFERENCES [dbo].[Rulesets] ([Id])
GO

ALTER TABLE [dbo].[Rules] CHECK CONSTRAINT [FK_Rules_Rulesets_RulesetId]
GO




---------------------------------------------------------------------------------
USE [ProductionRoutingDb]
GO

/****** Object:  Table [dbo].[Conditions]    Script Date: 19-02-2026 15:15:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Conditions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Field] [nvarchar](max) NOT NULL,
	[Operator] [int] NOT NULL,
	[Value] [nvarchar](max) NOT NULL,
	[RuleId] [int] NULL,
	[RulesetId] [int] NULL,
 CONSTRAINT [PK_Conditions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[Conditions]  WITH CHECK ADD  CONSTRAINT [FK_Conditions_Rules_RuleId] FOREIGN KEY([RuleId])
REFERENCES [dbo].[Rules] ([Id])
GO

ALTER TABLE [dbo].[Conditions] CHECK CONSTRAINT [FK_Conditions_Rules_RuleId]
GO

ALTER TABLE [dbo].[Conditions]  WITH CHECK ADD  CONSTRAINT [FK_Conditions_Rulesets_RulesetId] FOREIGN KEY([RulesetId])
REFERENCES [dbo].[Rulesets] ([Id])
GO

ALTER TABLE [dbo].[Conditions] CHECK CONSTRAINT [FK_Conditions_Rulesets_RulesetId]
GO

------------------------------------------------------------------------------
USE [ProductionRoutingDb]
GO

/****** Object:  Table [dbo].[EvaluationLogs]    Script Date: 19-02-2026 15:20:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EvaluationLogs](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderId] [nvarchar](max) NOT NULL,
	[Matched] [bit] NOT NULL,
	[MatchedRuleset] [nvarchar](max) NOT NULL,
	[MatchedRule] [nvarchar](max) NOT NULL,
	[ProductionPlant] [nvarchar](max) NOT NULL,
	[Reason] [nvarchar](max) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_EvaluationLogs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

