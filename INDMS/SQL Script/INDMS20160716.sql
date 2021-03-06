USE [master]
GO
/****** Object:  Database [INDMS20160716]    Script Date: 27-Aug-16 5:17:01 PM ******/
CREATE DATABASE [INDMS20160716]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'INDMS20160716', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\INDMS20160716.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'INDMS20160716_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\INDMS20160716_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [INDMS20160716] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [INDMS20160716].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [INDMS20160716] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [INDMS20160716] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [INDMS20160716] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [INDMS20160716] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [INDMS20160716] SET ARITHABORT OFF 
GO
ALTER DATABASE [INDMS20160716] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [INDMS20160716] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [INDMS20160716] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [INDMS20160716] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [INDMS20160716] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [INDMS20160716] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [INDMS20160716] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [INDMS20160716] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [INDMS20160716] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [INDMS20160716] SET  DISABLE_BROKER 
GO
ALTER DATABASE [INDMS20160716] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [INDMS20160716] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [INDMS20160716] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [INDMS20160716] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [INDMS20160716] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [INDMS20160716] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [INDMS20160716] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [INDMS20160716] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [INDMS20160716] SET  MULTI_USER 
GO
ALTER DATABASE [INDMS20160716] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [INDMS20160716] SET DB_CHAINING OFF 
GO
ALTER DATABASE [INDMS20160716] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [INDMS20160716] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [INDMS20160716] SET DELAYED_DURABILITY = DISABLED 
GO
USE [INDMS20160716]
GO
/****** Object:  Table [dbo].[AdminCorrespondence]    Script Date: 27-Aug-16 5:17:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AdminCorrespondence](
	[Id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[FileNo] [varchar](max) NULL,
	[LetterNo] [varchar](max) NULL,
	[Subject] [varchar](max) NULL,
	[TypeOf] [varchar](max) NULL,
	[From] [varchar](max) NULL,
	[To] [varchar](max) NULL,
	[CopyTo] [varchar](max) NULL,
	[Date] [datetime] NULL,
	[FilePath] [varchar](max) NULL,
	[CreatedBy] [varchar](max) NULL,
	[CreatedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CoveringLetter]    Script Date: 27-Aug-16 5:17:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CoveringLetter](
	[Id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[POId] [numeric](18, 0) NULL,
	[PONo] [varchar](max) NULL,
	[FileNo] [varchar](max) NULL,
	[Date] [datetime] NULL,
	[NoOfLots] [varchar](max) NULL,
	[POValue] [numeric](18, 0) NULL,
	[Warantee] [varchar](max) NULL,
	[CreatedBy] [varchar](max) NULL,
	[CreatedDate] [datetime] NULL DEFAULT (getdate()),
	[Active] [varchar](50) NULL DEFAULT ('Y'),
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Drawings]    Script Date: 27-Aug-16 5:17:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Drawings](
	[Id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[POId] [numeric](18, 0) NULL,
	[PONo] [varchar](max) NULL,
	[FileNo] [varchar](max) NULL,
	[DrawingNo] [varchar](max) NOT NULL,
	[Subject] [varchar](max) NOT NULL,
	[ApprovalDate] [datetime] NOT NULL,
	[ApprovalBy] [varchar](max) NOT NULL,
	[CreatedBy] [varchar](max) NULL,
	[CreatedDate] [datetime] NULL DEFAULT (getdate()),
	[FilePath] [varchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FCL]    Script Date: 27-Aug-16 5:17:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FCL](
	[Id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[POId] [numeric](18, 0) NOT NULL,
	[POName] [varchar](max) NULL,
	[CreatedDate] [datetime] NULL DEFAULT (getdate()),
	[CreatedBy] [varchar](max) NULL,
	[Flag] [varchar](max) NULL DEFAULT ('OPEN'),
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FCLDetails]    Script Date: 27-Aug-16 5:17:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FCLDetails](
	[Id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[FCLId] [numeric](18, 0) NOT NULL,
	[POSrNo] [varchar](max) NULL,
	[PODetails] [varchar](max) NULL,
	[Active] [varchar](10) NULL DEFAULT ('Y'),
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Files]    Script Date: 27-Aug-16 5:17:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Files](
	[Id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[FileNo] [varchar](max) NULL,
	[Subject] [varchar](max) NULL,
	[FilePath] [varchar](max) NULL,
	[CreatedBy] [varchar](max) NULL,
	[CreatedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Firms]    Script Date: 27-Aug-16 5:17:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Firms](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirmName] [nvarchar](max) NULL,
	[FirmAddress] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NULL DEFAULT (getdate()),
	[Active] [nvarchar](50) NULL DEFAULT ('1'),
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GeneralBooks]    Script Date: 27-Aug-16 5:17:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GeneralBooks](
	[ID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[Title] [varchar](max) NOT NULL,
	[Subject] [varchar](max) NOT NULL,
	[Year] [varchar](10) NOT NULL,
	[FilePath] [varchar](max) NOT NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedDate] [datetime] NULL CONSTRAINT [DF_GeneralBooks_CreatedDate]  DEFAULT (getdate()),
 CONSTRAINT [PK_GeneralBooks] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GuideLine]    Script Date: 27-Aug-16 5:17:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GuideLine](
	[ID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[IssuingAuthority] [varchar](max) NOT NULL,
	[Subject] [varchar](max) NOT NULL,
	[Year] [varchar](max) NOT NULL,
	[FilePath] [varchar](max) NOT NULL,
	[CreatedBy] [varchar](max) NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [PK_GuideLine] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MovementOrder]    Script Date: 27-Aug-16 5:17:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MovementOrder](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderNo] [varchar](max) NOT NULL,
	[Subject] [varchar](max) NOT NULL,
	[Date] [datetime] NOT NULL,
	[InspectorName] [varchar](max) NOT NULL,
	[Designation] [varchar](max) NOT NULL,
	[FirmName] [varchar](max) NOT NULL,
	[OnwardStartDate] [datetime] NOT NULL,
	[OnwardEndDate] [datetime] NOT NULL,
	[OnwordFrom] [varchar](max) NOT NULL,
	[OnwordTo] [varchar](max) NOT NULL,
	[OnwordModeOfTravel] [varchar](max) NOT NULL,
	[ReturnStartDate] [datetime] NOT NULL,
	[ReturnEndDate] [datetime] NOT NULL,
	[ReturnFrom] [varchar](max) NOT NULL,
	[ReturnTo] [varchar](max) NOT NULL,
	[ReturnModeOfTravel] [varchar](max) NOT NULL,
	[SigningAuthority] [varchar](max) NOT NULL,
	[SADesignation] [varchar](max) NOT NULL,
	[Distribution] [varchar](max) NULL,
	[CreatedBy] [varchar](max) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedBy] [varchar](max) NULL,
	[UpdatedDate] [varchar](max) NULL,
	[Flag] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ParameterMaster]    Script Date: 27-Aug-16 5:17:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ParameterMaster](
	[Id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[KeyName] [varchar](50) NOT NULL,
	[KeyValue] [varchar](max) NOT NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedDate] [datetime] NULL CONSTRAINT [DF_ParameterMaster_CreatedDate]  DEFAULT (getdate()),
 CONSTRAINT [PK_ParameterMaster] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Photographs]    Script Date: 27-Aug-16 5:17:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Photographs](
	[Id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[POId] [numeric](18, 0) NULL,
	[PONo] [varchar](max) NULL,
	[FolderName] [varchar](max) NULL,
	[Subject] [varchar](max) NULL,
	[Date] [datetime] NULL,
	[FilePath] [varchar](max) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[POCorrespondence]    Script Date: 27-Aug-16 5:17:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[POCorrespondence](
	[Id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[POId] [numeric](18, 0) NULL,
	[PONo] [varchar](max) NULL,
	[FileNo] [varchar](max) NULL,
	[LetterNo] [varchar](max) NULL,
	[Description] [varchar](max) NULL,
	[Subject] [varchar](max) NULL,
	[TypeOfCorrespondence] [varchar](max) NULL,
	[From] [varchar](max) NULL,
	[To] [varchar](max) NULL,
	[CopyTo] [varchar](max) NULL,
	[Date] [datetime] NULL,
	[FilePath] [varchar](max) NULL,
	[CreatedBy] [varchar](max) NULL,
	[CreatedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[POGeneration]    Script Date: 27-Aug-16 5:17:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[POGeneration](
	[Id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[PO_ID] [numeric](18, 0) NOT NULL,
	[FCL] [int] NULL DEFAULT ((0)),
	[PO_CORRESPONDENCE] [int] NULL DEFAULT ((0)),
	[DRAWING] [int] NULL DEFAULT ((0)),
	[QAP] [int] NULL DEFAULT ((0)),
	[I_NOTE] [int] NULL DEFAULT ((0)),
	[Active] [int] NULL DEFAULT ((1)),
 CONSTRAINT [PK_POGeneration] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PolicyLetter]    Script Date: 27-Aug-16 5:17:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PolicyLetter](
	[id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[Year] [varchar](8) NOT NULL,
	[IssuingAuthority] [varchar](50) NOT NULL,
	[PLNo] [varchar](max) NOT NULL,
	[Subject] [varchar](max) NOT NULL,
	[Date] [date] NOT NULL,
	[FilePath] [varchar](max) NOT NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedDate] [datetime] NULL CONSTRAINT [DF_PolicyLetter_CreatedDate]  DEFAULT (getdate()),
 CONSTRAINT [PK_tblPolicyLetters] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PurchaseOrder]    Script Date: 27-Aug-16 5:17:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PurchaseOrder](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FileNo] [varchar](max) NULL,
	[PONo] [varchar](max) NULL,
	[PODate] [datetime] NULL,
	[POValue] [numeric](18, 2) NULL,
	[Quantity] [numeric](18, 2) NULL,
	[NoOfLots] [numeric](18, 2) NULL,
	[DeliveryDate] [datetime] NULL,
	[PoPlacingAuthority] [varchar](max) NULL,
	[Inspectors] [varchar](max) NULL,
	[Firm] [varchar](max) NULL,
	[FirmAddress] [varchar](max) NULL,
	[FilePath] [varchar](max) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [varchar](max) NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [varchar](max) NULL,
	[Flag] [varchar](max) NULL,
	[Subject] [varchar](max) NULL,
	[Equipment] [varchar](max) NULL,
	[SparesFor] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[QAP]    Script Date: 27-Aug-16 5:17:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[QAP](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PONo] [varchar](max) NULL,
	[FileNo] [varchar](max) NULL,
	[QAPNo] [varchar](max) NOT NULL,
	[Subject] [varchar](max) NOT NULL,
	[ApprovalDate] [datetime] NOT NULL,
	[ApprovedBy] [varchar](max) NOT NULL,
	[DrawingNoRef] [varchar](max) NULL,
	[FilePath] [varchar](max) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[POId] [numeric](18, 0) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 27-Aug-16 5:17:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Role] [varchar](50) NOT NULL,
 CONSTRAINT [PK__Roles__3214EC07DA5A1121] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Standards]    Script Date: 27-Aug-16 5:17:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Standards](
	[Id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[StandardNo] [varchar](max) NULL,
	[Year] [varchar](10) NULL,
	[Revision] [varchar](max) NULL,
	[SubjectParam] [varchar](50) NULL,
	[Subject] [varchar](max) NULL,
	[Type] [varchar](50) NULL,
	[FilePath] [varchar](max) NOT NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[StandingOrders]    Script Date: 27-Aug-16 5:17:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[StandingOrders](
	[id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[IssuingAuthority] [varchar](max) NULL,
	[Subject] [varchar](max) NULL,
	[Year] [varchar](50) NULL,
	[Revision] [varchar](max) NULL,
	[FilePath] [varchar](max) NOT NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedDate] [datetime] NULL CONSTRAINT [DF_StandingOrders_CreatedDate]  DEFAULT (getdate()),
 CONSTRAINT [PK_StandingOrders] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TYMemo]    Script Date: 27-Aug-16 5:17:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TYMemo](
	[Id] [numeric](18, 0) NOT NULL,
	[FileNo] [varchar](max) NULL,
	[TYMemoNO] [varchar](max) NULL,
	[Subject] [varchar](max) NULL,
	[Date] [datetime] NULL,
	[FilePath] [varchar](max) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[User]    Script Date: 27-Aug-16 5:17:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [uniqueidentifier] NOT NULL,
	[UserName] [varchar](20) NOT NULL,
	[Password] [varchar](max) NOT NULL,
	[Role] [varchar](10) NOT NULL,
	[Name] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedDate] [datetime] NULL DEFAULT (getdate()),
	[Active] [varchar](10) NULL DEFAULT ('Y'),
	[Designation] [varchar](max) NULL,
	[OfficerRank] [varchar](max) NULL,
	[ContactNo] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[CoveringLetter] ON 

INSERT [dbo].[CoveringLetter] ([Id], [POId], [PONo], [FileNo], [Date], [NoOfLots], [POValue], [Warantee], [CreatedBy], [CreatedDate], [Active]) VALUES (CAST(1 AS Numeric(18, 0)), CAST(8 AS Numeric(18, 0)), N'1', N'sdfsd', CAST(N'2016-08-06 00:00:00.000' AS DateTime), N'1', CAST(1 AS Numeric(18, 0)), N'dfg', NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[CoveringLetter] OFF
SET IDENTITY_INSERT [dbo].[Drawings] ON 

INSERT [dbo].[Drawings] ([Id], [POId], [PONo], [FileNo], [DrawingNo], [Subject], [ApprovalDate], [ApprovalBy], [CreatedBy], [CreatedDate], [FilePath]) VALUES (CAST(1 AS Numeric(18, 0)), CAST(8 AS Numeric(18, 0)), N'1', N'sdfsg', N'gffghfgh', N'ghdfh', CAST(N'2016-07-08 00:00:00.000' AS DateTime), N'hgfhfd', N'cfa21163-6a01-4c14-9b54-1745ca5ff849', CAST(N'2016-08-22 17:07:01.017' AS DateTime), N'/Uploads/Drawings/de8967f7-5aca-4ab8-a23e-c95971c0de2b.pdf')
INSERT [dbo].[Drawings] ([Id], [POId], [PONo], [FileNo], [DrawingNo], [Subject], [ApprovalDate], [ApprovalBy], [CreatedBy], [CreatedDate], [FilePath]) VALUES (CAST(2 AS Numeric(18, 0)), NULL, N'dfsd', N'12343534', N'gdf', N'dfgdf', CAST(N'2016-08-04 00:00:00.000' AS DateTime), N'gdf', N'cfa21163-6a01-4c14-9b54-1745ca5ff849', NULL, N'/Uploads/Drawings/0ac885eb-0028-4e72-a409-b5f23e108b21.pdf')
SET IDENTITY_INSERT [dbo].[Drawings] OFF
SET IDENTITY_INSERT [dbo].[FCL] ON 

INSERT [dbo].[FCL] ([Id], [POId], [POName], [CreatedDate], [CreatedBy], [Flag]) VALUES (CAST(8 AS Numeric(18, 0)), CAST(8 AS Numeric(18, 0)), N'SFDSDAF123123', CAST(N'2016-08-19 11:28:57.333' AS DateTime), N'cfa21163-6a01-4c14-9b54-1745ca5ff849', N'ACCEPTED')
INSERT [dbo].[FCL] ([Id], [POId], [POName], [CreatedDate], [CreatedBy], [Flag]) VALUES (CAST(9 AS Numeric(18, 0)), CAST(8 AS Numeric(18, 0)), N'1235435435', NULL, N'cfa21163-6a01-4c14-9b54-1745ca5ff849', N'OPEN')
SET IDENTITY_INSERT [dbo].[FCL] OFF
SET IDENTITY_INSERT [dbo].[FCLDetails] ON 

INSERT [dbo].[FCLDetails] ([Id], [FCLId], [POSrNo], [PODetails], [Active]) VALUES (CAST(17 AS Numeric(18, 0)), CAST(8 AS Numeric(18, 0)), N'1', N'DFIHUIDHDFUI', NULL)
INSERT [dbo].[FCLDetails] ([Id], [FCLId], [POSrNo], [PODetails], [Active]) VALUES (CAST(18 AS Numeric(18, 0)), CAST(8 AS Numeric(18, 0)), N'2', N'DFJSGIODHHGUDFIHGU', NULL)
INSERT [dbo].[FCLDetails] ([Id], [FCLId], [POSrNo], [PODetails], [Active]) VALUES (CAST(19 AS Numeric(18, 0)), CAST(8 AS Numeric(18, 0)), N'3', N'DFJGDIOFHGIOUH', NULL)
INSERT [dbo].[FCLDetails] ([Id], [FCLId], [POSrNo], [PODetails], [Active]) VALUES (CAST(20 AS Numeric(18, 0)), CAST(8 AS Numeric(18, 0)), N'4', N'1231', NULL)
INSERT [dbo].[FCLDetails] ([Id], [FCLId], [POSrNo], [PODetails], [Active]) VALUES (CAST(21 AS Numeric(18, 0)), CAST(9 AS Numeric(18, 0)), N'1', N'DFDF', NULL)
SET IDENTITY_INSERT [dbo].[FCLDetails] OFF
SET IDENTITY_INSERT [dbo].[Firms] ON 

INSERT [dbo].[Firms] ([Id], [FirmName], [FirmAddress], [CreatedDate], [Active]) VALUES (16, N'M/S KRISHANA INDUSTRIES', N'9-25/A, CHANDANWALI 138, CP TANK ROAD MUMBAI-400004', CAST(N'2016-08-09 11:37:50.300' AS DateTime), N'1')
INSERT [dbo].[Firms] ([Id], [FirmName], [FirmAddress], [CreatedDate], [Active]) VALUES (17, N'M/S AIR CONTROL * CHEMICAL ENGINEERING CO LTD.', N'63, THE CHAMBERS, NR. GOYALK PLACE OPP. 
GURUDWARA SARKEJ HIGHWAY, AHMEDABAD-302415
', CAST(N'2016-08-09 11:38:13.400' AS DateTime), N'1')
INSERT [dbo].[Firms] ([Id], [FirmName], [FirmAddress], [CreatedDate], [Active]) VALUES (18, N'M/S ALTOP CONTROLS PVT. LTD.', N'5, TURAKHIA PARK, SHOPPING COMPLEX, BUILDING NO.1  NEXT TO CORPORATION BANK, MG ROAD, KANDIVALI(W), MUMBAI-400067', NULL, NULL)
INSERT [dbo].[Firms] ([Id], [FirmName], [FirmAddress], [CreatedDate], [Active]) VALUES (19, N'M/S MEGHA ROTO TECH PVT. LTD.', N'PLOT NO. C-1/A-4, NR. AEC GRID STATION GIDC. ODHAV,  AHMEDABAD-302415', NULL, NULL)
INSERT [dbo].[Firms] ([Id], [FirmName], [FirmAddress], [CreatedDate], [Active]) VALUES (20, N'M/S POLYPHASE MOTORS', N'GIDC, MAKARPURA, VAODARA-390010', NULL, NULL)
INSERT [dbo].[Firms] ([Id], [FirmName], [FirmAddress], [CreatedDate], [Active]) VALUES (21, N'M/S ELECON ENGINEERING CO. LTD.', N'POST BOX NO.6, ANAND SOJITRA ROAD, VALLABH VIDHYANAGAR-308120', NULL, NULL)
INSERT [dbo].[Firms] ([Id], [FirmName], [FirmAddress], [CreatedDate], [Active]) VALUES (22, N'M/S MAN DIESEL & TUBO INDIA LIMITED', N'PLOT NO. 219-220, GIDC, RANOLI VADODARA-391350', NULL, NULL)
INSERT [dbo].[Firms] ([Id], [FirmName], [FirmAddress], [CreatedDate], [Active]) VALUES (23, N'M/S SPX FLOW TECHNOLOGY PVT. LTD.', N'SURVEY NO. 275 ODHAV ROAD ODHAV, AHMEDABAD-382415', NULL, NULL)
INSERT [dbo].[Firms] ([Id], [FirmName], [FirmAddress], [CreatedDate], [Active]) VALUES (24, N'M/S ERHARDT + LEIMER PVT. LTD.', N'SURVEY NO. 252-1, 252/2 NR. ARVEE DEHIM SARKHEJ-BAVLA HIGHWAY,  SARI SANAND-302220', NULL, NULL)
INSERT [dbo].[Firms] ([Id], [FirmName], [FirmAddress], [CreatedDate], [Active]) VALUES (25, N'M/S FLEXA THERM EXPANFLOW PVT. LTD.', N'354 GIDC MAKARPURA VADODARA', NULL, NULL)
INSERT [dbo].[Firms] ([Id], [FirmName], [FirmAddress], [CreatedDate], [Active]) VALUES (26, N'M/S EVEREST KANTO CYLINDERS LIMITED', N'204, RAHEJA CENTER, FREE PRESS JOURNAL MARG, 214 NARIMAN POINT,  MUMBAI-400021', NULL, NULL)
INSERT [dbo].[Firms] ([Id], [FirmName], [FirmAddress], [CreatedDate], [Active]) VALUES (27, N'M/S JYOTI LIMITED', N'NANUBHAI AMIN MARG, INDUSTRIUAL AREA, PO CHEMICAL INDUSTRIES,  VADODARA-390003', NULL, NULL)
INSERT [dbo].[Firms] ([Id], [FirmName], [FirmAddress], [CreatedDate], [Active]) VALUES (28, N'M/S GEA REFRIGERATION PVT. LTD.', N'471, SAKARDA BHADARWA ROAD, NEAR MOXI BUS STAND MOXI-391780', NULL, NULL)
INSERT [dbo].[Firms] ([Id], [FirmName], [FirmAddress], [CreatedDate], [Active]) VALUES (29, N'M/S PATEL BRASS WORKS', N'2, BHAKTI NAGAR STATION PLOT, RAJKOT-360022', NULL, NULL)
INSERT [dbo].[Firms] ([Id], [FirmName], [FirmAddress], [CreatedDate], [Active]) VALUES (30, N'M/S SUPER WAUDITE JOINTINGS PVT. LTD.', N'G/1, SHYAM SUNDER APPARTMENT, NR. SUSHRUSHA HOSPITAL  6 LAXMI SOCIETY, NAVRANGPURA, AHMEDABAD-380006', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Firms] OFF
SET IDENTITY_INSERT [dbo].[GeneralBooks] ON 

INSERT [dbo].[GeneralBooks] ([ID], [Title], [Subject], [Year], [FilePath], [CreatedBy], [CreatedDate]) VALUES (CAST(2 AS Numeric(18, 0)), N'dfgdfgh', N'dfgdf', N'2016', N'/Uploads/GeneralBooks/8099ba0f-1609-4871-85b2-03d912e06206.pdf', N'cfa21163-6a01-4c14-9b54-1745ca5ff849', NULL)
SET IDENTITY_INSERT [dbo].[GeneralBooks] OFF
SET IDENTITY_INSERT [dbo].[GuideLine] ON 

INSERT [dbo].[GuideLine] ([ID], [IssuingAuthority], [Subject], [Year], [FilePath], [CreatedBy], [CreatedDate]) VALUES (CAST(3 AS Numeric(18, 0)), N'DGQA', N'jhdjighusd', N'2016', N'/Uploads/GuideLines/1ec14216-41c3-4607-9637-f840807c2e51.pdf', N'cfa21163-6a01-4c14-9b54-1745ca5ff849', NULL)
SET IDENTITY_INSERT [dbo].[GuideLine] OFF
SET IDENTITY_INSERT [dbo].[ParameterMaster] ON 

INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(81 AS Numeric(18, 0)), N'POPlacingAuthority', N'CGHQ', N'cfa21163-6a01-4c14-9b54-1745ca5ff849', NULL)
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(82 AS Numeric(18, 0)), N'IssuingAuthority', N'DQA(N)', N'cfa21163-6a01-4c14-9b54-1745ca5ff849', NULL)
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(83 AS Numeric(18, 0)), N'StdType', N'IS', N'cfa21163-6a01-4c14-9b54-1745ca5ff849', NULL)
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(84 AS Numeric(18, 0)), N'StdType', N'BS', N'cfa21163-6a01-4c14-9b54-1745ca5ff849', NULL)
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(85 AS Numeric(18, 0)), N'StdType', N'ASTM', N'cfa21163-6a01-4c14-9b54-1745ca5ff849', NULL)
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(86 AS Numeric(18, 0)), N'StdType', N'NES', N'cfa21163-6a01-4c14-9b54-1745ca5ff849', NULL)
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(87 AS Numeric(18, 0)), N'IssuingAuthority', N'DQA(WP)', N'cfa21163-6a01-4c14-9b54-1745ca5ff849', NULL)
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(88 AS Numeric(18, 0)), N'SoSubject', N'TECH', N'cfa21163-6a01-4c14-9b54-1745ca5ff849', NULL)
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(89 AS Numeric(18, 0)), N'IssuingAuthority', N'DGQA', N'cfa21163-6a01-4c14-9b54-1745ca5ff849', NULL)
INSERT [dbo].[ParameterMaster] ([Id], [KeyName], [KeyValue], [CreatedBy], [CreatedDate]) VALUES (CAST(90 AS Numeric(18, 0)), N'SoSubject', N'ADMIN', N'cfa21163-6a01-4c14-9b54-1745ca5ff849', NULL)
SET IDENTITY_INSERT [dbo].[ParameterMaster] OFF
SET IDENTITY_INSERT [dbo].[POCorrespondence] ON 

INSERT [dbo].[POCorrespondence] ([Id], [POId], [PONo], [FileNo], [LetterNo], [Description], [Subject], [TypeOfCorrespondence], [From], [To], [CopyTo], [Date], [FilePath], [CreatedBy], [CreatedDate]) VALUES (CAST(1 AS Numeric(18, 0)), CAST(8 AS Numeric(18, 0)), N'1', N'ghjghj', N'ghjghj', N'jhgj', N'jghj', N'IN', N'sdsfgffhgjkl12313134', N'', N'jghjghj', CAST(N'2016-09-09 00:00:00.000' AS DateTime), N'/Uploads/POCorrespondence/6926b5d1-e9f0-4a30-85c5-7f2dfad3bd22.pdf', N'cfa21163-6a01-4c14-9b54-1745ca5ff849', CAST(N'2016-08-24 09:35:10.667' AS DateTime))
SET IDENTITY_INSERT [dbo].[POCorrespondence] OFF
SET IDENTITY_INSERT [dbo].[POGeneration] ON 

INSERT [dbo].[POGeneration] ([Id], [PO_ID], [FCL], [PO_CORRESPONDENCE], [DRAWING], [QAP], [I_NOTE], [Active]) VALUES (CAST(4 AS Numeric(18, 0)), CAST(8 AS Numeric(18, 0)), 1, 1, 1, 1, 1, NULL)
SET IDENTITY_INSERT [dbo].[POGeneration] OFF
SET IDENTITY_INSERT [dbo].[PolicyLetter] ON 

INSERT [dbo].[PolicyLetter] ([id], [Year], [IssuingAuthority], [PLNo], [Subject], [Date], [FilePath], [CreatedBy], [CreatedDate]) VALUES (CAST(7 AS Numeric(18, 0)), N'2016', N'DQA(N)', N'12123', N'dfjgi', CAST(N'2016-08-12' AS Date), N'/Uploads/PolicyLetter/85a131f7-b54a-4dd4-97d5-0e6cb89cafab.pdf', N'cfa21163-6a01-4c14-9b54-1745ca5ff849', NULL)
SET IDENTITY_INSERT [dbo].[PolicyLetter] OFF
SET IDENTITY_INSERT [dbo].[PurchaseOrder] ON 

INSERT [dbo].[PurchaseOrder] ([Id], [FileNo], [PONo], [PODate], [POValue], [Quantity], [NoOfLots], [DeliveryDate], [PoPlacingAuthority], [Inspectors], [Firm], [FirmAddress], [FilePath], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [Flag], [Subject], [Equipment], [SparesFor]) VALUES (8, N'1', N'1', CAST(N'2016-08-01 00:00:00.000' AS DateTime), CAST(1.00 AS Numeric(18, 2)), CAST(1.00 AS Numeric(18, 2)), CAST(1.00 AS Numeric(18, 2)), CAST(N'2016-08-30 14:24:18.000' AS DateTime), N'CGHQ', N'cfa21163-6a01-4c14-9b54-1745ca5ff849', N'16', NULL, N'/Uploads/PurchaseOrder/4dfe3050-66fb-4b70-a678-32ba303001ca.pdf', CAST(N'2016-08-13 11:04:43.420' AS DateTime), N'cfa21163-6a01-4c14-9b54-1745ca5ff849', NULL, NULL, NULL, N'NEW PO', NULL, NULL)
SET IDENTITY_INSERT [dbo].[PurchaseOrder] OFF
SET IDENTITY_INSERT [dbo].[QAP] ON 

INSERT [dbo].[QAP] ([Id], [PONo], [FileNo], [QAPNo], [Subject], [ApprovalDate], [ApprovedBy], [DrawingNoRef], [FilePath], [CreatedDate], [CreatedBy], [POId]) VALUES (6, N'1', N'asdf', N'fsdg', N'fhdf', CAST(N'2016-08-20 00:00:00.000' AS DateTime), N'cfa21163-6a01-4c14-9b54-1745ca5ff849', N'1', N'/Uploads/QAP/d797bcb0-2cc7-4daa-8aa4-0c2ca57675d6.pdf', CAST(N'2016-08-23 10:55:55.273' AS DateTime), N'cfa21163-6a01-4c14-9b54-1745ca5ff849', CAST(8 AS Numeric(18, 0)))
INSERT [dbo].[QAP] ([Id], [PONo], [FileNo], [QAPNo], [Subject], [ApprovalDate], [ApprovedBy], [DrawingNoRef], [FilePath], [CreatedDate], [CreatedBy], [POId]) VALUES (7, N'dfgdf', N'dfgd', N'2345345', N'dfgdfg', CAST(N'2016-08-13 00:00:00.000' AS DateTime), N'cfa21163-6a01-4c14-9b54-1745ca5ff849', NULL, N'/Uploads/QAP/78f953d2-cd6d-43ae-b5c8-36003914b98b.pdf', NULL, N'cfa21163-6a01-4c14-9b54-1745ca5ff849', NULL)
SET IDENTITY_INSERT [dbo].[QAP] OFF
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([Id], [Role]) VALUES (3, N'Admin')
INSERT [dbo].[Roles] ([Id], [Role]) VALUES (4, N'General')
SET IDENTITY_INSERT [dbo].[Roles] OFF
SET IDENTITY_INSERT [dbo].[Standards] ON 

INSERT [dbo].[Standards] ([Id], [StandardNo], [Year], [Revision], [SubjectParam], [Subject], [Type], [FilePath], [CreatedBy], [CreatedDate]) VALUES (CAST(3 AS Numeric(18, 0)), N'11', N'2016', N'1', N'', N'sdgfdfh', N'OTHERS', N'/Uploads/Standards/8a4d2233-b078-4841-889d-356bb4498879.pdf', N'cfa21163-6a01-4c14-9b54-1745ca5ff849', NULL)
INSERT [dbo].[Standards] ([Id], [StandardNo], [Year], [Revision], [SubjectParam], [Subject], [Type], [FilePath], [CreatedBy], [CreatedDate]) VALUES (CAST(4 AS Numeric(18, 0)), N'1', N'2016', N'1', N'', N'sadf', N'OTHERS', N'/Uploads/Standards/fb01d2b2-50e1-41a5-9ed4-73e05c905d60.pdf', N'cfa21163-6a01-4c14-9b54-1745ca5ff849', NULL)
INSERT [dbo].[Standards] ([Id], [StandardNo], [Year], [Revision], [SubjectParam], [Subject], [Type], [FilePath], [CreatedBy], [CreatedDate]) VALUES (CAST(5 AS Numeric(18, 0)), N'1', N'2016', N'1', NULL, N'1', N'BS', N'/Uploads/Standards/d3cb98f6-c68e-4f75-8bcf-0994a55663f7.pdf', N'cfa21163-6a01-4c14-9b54-1745ca5ff849', CAST(N'2016-08-27 10:19:46.917' AS DateTime))
INSERT [dbo].[Standards] ([Id], [StandardNo], [Year], [Revision], [SubjectParam], [Subject], [Type], [FilePath], [CreatedBy], [CreatedDate]) VALUES (CAST(6 AS Numeric(18, 0)), N'1', N'1', N'1', NULL, N'1', N'NES', N'/Uploads/Standards/5cb29533-af65-4af2-9cf1-987e01c9d015.pdf', N'cfa21163-6a01-4c14-9b54-1745ca5ff849', CAST(N'2016-08-27 10:20:36.313' AS DateTime))
SET IDENTITY_INSERT [dbo].[Standards] OFF
SET IDENTITY_INSERT [dbo].[StandingOrders] ON 

INSERT [dbo].[StandingOrders] ([id], [IssuingAuthority], [Subject], [Year], [Revision], [FilePath], [CreatedBy], [CreatedDate]) VALUES (CAST(6 AS Numeric(18, 0)), N'DGQA', N'ADMIN', N'2016', N'1', N'/Uploads/StandingOrders/fecfc098-c121-4d8c-8ca4-76e5436d404d.pdf', N'cfa21163-6a01-4c14-9b54-1745ca5ff849', NULL)
SET IDENTITY_INSERT [dbo].[StandingOrders] OFF
INSERT [dbo].[User] ([UserId], [UserName], [Password], [Role], [Name], [Email], [CreatedBy], [CreatedDate], [Active], [Designation], [OfficerRank], [ContactNo]) VALUES (N'cfa21163-6a01-4c14-9b54-1745ca5ff849', N'atmiya', N'YXRtaXlh', N'Admin', N'Atmiya Vaghela', NULL, N'00000000-0000-0000-0000-000000000000', NULL, NULL, N' Jr. Engineer', NULL, N'9099905205')
ALTER TABLE [dbo].[AdminCorrespondence] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Files] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[MovementOrder] ADD  DEFAULT ('OPEN') FOR [Flag]
GO
ALTER TABLE [dbo].[Photographs] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[TYMemo] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
USE [master]
GO
ALTER DATABASE [INDMS20160716] SET  READ_WRITE 
GO
