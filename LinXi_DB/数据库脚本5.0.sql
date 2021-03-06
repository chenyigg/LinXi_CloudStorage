USE [master]
GO
/****** Object:  Database [LinXi_CloudStorage]    Script Date: 2020/6/29 21:30:14 ******/
CREATE DATABASE [LinXi_CloudStorage]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'LinXi_CloudStorage', FILENAME = N'D:\C#\LinXi_CloudStorage\DB\LinXi_CloudStorage.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'LinXi_CloudStorage_log', FILENAME = N'D:\C#\LinXi_CloudStorage\DB\LinXi_CloudStorage_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [LinXi_CloudStorage] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [LinXi_CloudStorage].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [LinXi_CloudStorage] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [LinXi_CloudStorage] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [LinXi_CloudStorage] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [LinXi_CloudStorage] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [LinXi_CloudStorage] SET ARITHABORT OFF 
GO
ALTER DATABASE [LinXi_CloudStorage] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [LinXi_CloudStorage] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [LinXi_CloudStorage] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [LinXi_CloudStorage] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [LinXi_CloudStorage] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [LinXi_CloudStorage] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [LinXi_CloudStorage] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [LinXi_CloudStorage] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [LinXi_CloudStorage] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [LinXi_CloudStorage] SET  DISABLE_BROKER 
GO
ALTER DATABASE [LinXi_CloudStorage] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [LinXi_CloudStorage] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [LinXi_CloudStorage] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [LinXi_CloudStorage] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [LinXi_CloudStorage] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [LinXi_CloudStorage] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [LinXi_CloudStorage] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [LinXi_CloudStorage] SET RECOVERY FULL 
GO
ALTER DATABASE [LinXi_CloudStorage] SET  MULTI_USER 
GO
ALTER DATABASE [LinXi_CloudStorage] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [LinXi_CloudStorage] SET DB_CHAINING OFF 
GO
ALTER DATABASE [LinXi_CloudStorage] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [LinXi_CloudStorage] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [LinXi_CloudStorage] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'LinXi_CloudStorage', N'ON'
GO
ALTER DATABASE [LinXi_CloudStorage] SET QUERY_STORE = OFF
GO
USE [LinXi_CloudStorage]
GO
/****** Object:  Table [dbo].[Lx_Resource]    Script Date: 2020/6/29 21:30:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lx_Resource](
	[Lx_ResourceID] [int] IDENTITY(1,1) NOT NULL,
	[Lx_ResourceName] [nvarchar](max) NOT NULL,
	[Lx_ResourceSize] [nvarchar](50) NOT NULL,
	[Lx_ResourceCategoryID] [int] NOT NULL,
	[Lx_ResourceMDFive] [nvarchar](50) NULL,
	[Lx_ResourcePath] [nvarchar](50) NULL,
 CONSTRAINT [PK__Resource__3B1CE1C55C698018] PRIMARY KEY CLUSTERED 
(
	[Lx_ResourceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lx_ResourceAccount]    Script Date: 2020/6/29 21:30:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lx_ResourceAccount](
	[Lx_ResourceAccountID] [int] IDENTITY(1,1) NOT NULL,
	[Lx_ResourceAccountName] [nvarchar](max) NOT NULL,
	[Lx_ResourceAccountTime] [datetime] NOT NULL,
	[Lx_ResourceID] [int] NOT NULL,
	[Lx_UsersID] [int] NOT NULL,
	[Lx_Pid] [int] NOT NULL,
	[Lx_Guid] [nvarchar](max) NOT NULL,
	[Lx_Recycle] [bit] NOT NULL,
 CONSTRAINT [PK__Resource__BD457DB27EFA34E4] PRIMARY KEY CLUSTERED 
(
	[Lx_ResourceAccountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lx_Share]    Script Date: 2020/6/29 21:30:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lx_Share](
	[Lx_ShareId] [int] IDENTITY(1,1) NOT NULL,
	[Lx_UserID] [int] NOT NULL,
	[Lx_ResourceAccountID] [int] NOT NULL,
	[Lx_ShareLink] [nvarchar](50) NOT NULL,
	[Lx_ValidCode] [nvarchar](50) NOT NULL,
	[Lx_CodeKey] [nvarchar](max) NOT NULL,
	[Lx_ShareTime] [nvarchar](50) NOT NULL,
	[Lx_CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Lx_Share] PRIMARY KEY CLUSTERED 
(
	[Lx_ShareId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lx_Users]    Script Date: 2020/6/29 21:30:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lx_Users](
	[Lx_UsersID] [int] IDENTITY(1,1) NOT NULL,
	[Lx_UsersName] [nvarchar](max) NULL,
	[Lx_UsersPhone] [nvarchar](11) NULL,
	[Lx_UsersLoginName] [nvarchar](50) NOT NULL,
	[Lx_UsersLoginPWD] [nvarchar](40) NOT NULL,
	[Lx_UsersLoginEmail] [nvarchar](40) NOT NULL,
	[Lx_UsersRemark] [nvarchar](50) NULL,
 CONSTRAINT [PK__Lx_Users__6763AF517BBD12A8] PRIMARY KEY CLUSTERED 
(
	[Lx_UsersID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Lx_Resource] ON 

INSERT [dbo].[Lx_Resource] ([Lx_ResourceID], [Lx_ResourceName], [Lx_ResourceSize], [Lx_ResourceCategoryID], [Lx_ResourceMDFive], [Lx_ResourcePath]) VALUES (2, N'人像1.jpg', N'0.4mb', 2, N'67seafd170761e417b3eade3a5a07709', N'Pic\人像1.jpg')
INSERT [dbo].[Lx_Resource] ([Lx_ResourceID], [Lx_ResourceName], [Lx_ResourceSize], [Lx_ResourceCategoryID], [Lx_ResourceMDFive], [Lx_ResourcePath]) VALUES (3, N'视频1.mp4', N'1.2mb', 3, N'28aeafd170761e417b3eapoi85a07709', N'Video\视频1.mp4')
INSERT [dbo].[Lx_Resource] ([Lx_ResourceID], [Lx_ResourceName], [Lx_ResourceSize], [Lx_ResourceCategoryID], [Lx_ResourceMDFive], [Lx_ResourcePath]) VALUES (4, N'文档1.txt', N'0.5mb', 5, N'eufkafd189061e417b3eade3a5a07709', N'TxT\文档1.txt')
INSERT [dbo].[Lx_Resource] ([Lx_ResourceID], [Lx_ResourceName], [Lx_ResourceSize], [Lx_ResourceCategoryID], [Lx_ResourceMDFive], [Lx_ResourcePath]) VALUES (5, N'音乐1.mp3', N'12mb', 4, N'plsyafd170761e417b3eade3a5a0shxt', N'Music\音乐1.mp3')
INSERT [dbo].[Lx_Resource] ([Lx_ResourceID], [Lx_ResourceName], [Lx_ResourceSize], [Lx_ResourceCategoryID], [Lx_ResourceMDFive], [Lx_ResourcePath]) VALUES (6, N'新建文件夹1', N'0mb', 1, N'sowlpqt170761e417b3eade3a5a07709', NULL)
INSERT [dbo].[Lx_Resource] ([Lx_ResourceID], [Lx_ResourceName], [Lx_ResourceSize], [Lx_ResourceCategoryID], [Lx_ResourceMDFive], [Lx_ResourcePath]) VALUES (7, N'压缩包.rar', N'12mb', 6, N'mncyc6d170761e417b3eade3a5a07709', N'Rar\压缩包.rar')
INSERT [dbo].[Lx_Resource] ([Lx_ResourceID], [Lx_ResourceName], [Lx_ResourceSize], [Lx_ResourceCategoryID], [Lx_ResourceMDFive], [Lx_ResourcePath]) VALUES (21, N'暂无文件.png', N'0.01mb', 2, N'98860b6c718cce437e0c9b384c0bd6da', N'Pic\暂无文件.png')
INSERT [dbo].[Lx_Resource] ([Lx_ResourceID], [Lx_ResourceName], [Lx_ResourceSize], [Lx_ResourceCategoryID], [Lx_ResourceMDFive], [Lx_ResourcePath]) VALUES (89, N'路飞1.jpg', N'5.49mb', 2, N'db3eb8e6c71530d18d60da682fda5ff4', N'Pic\路飞1.jpg')
INSERT [dbo].[Lx_Resource] ([Lx_ResourceID], [Lx_ResourceName], [Lx_ResourceSize], [Lx_ResourceCategoryID], [Lx_ResourceMDFive], [Lx_ResourcePath]) VALUES (90, N'深圳夜景4k壁纸_彼岸图网.jpg', N'6.12mb', 2, N'492a426ddfaa961ec01428923fe0bab1', N'Pic\深圳夜景4k壁纸_彼岸图网.jpg')
INSERT [dbo].[Lx_Resource] ([Lx_ResourceID], [Lx_ResourceName], [Lx_ResourceSize], [Lx_ResourceCategoryID], [Lx_ResourceMDFive], [Lx_ResourcePath]) VALUES (91, N'vg15v3.jpg', N'2.20mb', 2, N'bc5025447115fbf807cef0d2899fbfa5', N'Pic\vg15v3.jpg')
INSERT [dbo].[Lx_Resource] ([Lx_ResourceID], [Lx_ResourceName], [Lx_ResourceSize], [Lx_ResourceCategoryID], [Lx_ResourceMDFive], [Lx_ResourcePath]) VALUES (92, N'是四十.txt', N'0.00mb', 5, N'd41d8cd98f00b204e9800998ecf8427e', N'TxT\是四十.txt')
INSERT [dbo].[Lx_Resource] ([Lx_ResourceID], [Lx_ResourceName], [Lx_ResourceSize], [Lx_ResourceCategoryID], [Lx_ResourceMDFive], [Lx_ResourcePath]) VALUES (93, N'4.txt', N'0.00mb', 5, N'35e315fe8e41f08bc9169973bcf25633', N'TxT\4.txt')
INSERT [dbo].[Lx_Resource] ([Lx_ResourceID], [Lx_ResourceName], [Lx_ResourceSize], [Lx_ResourceCategoryID], [Lx_ResourceMDFive], [Lx_ResourcePath]) VALUES (94, N'媒体1.mp4', N'7.77mb', 3, N'269fc7b91e867fa7aaf5c6bc6fc6cb29', N'Video\媒体1.mp4')
INSERT [dbo].[Lx_Resource] ([Lx_ResourceID], [Lx_ResourceName], [Lx_ResourceSize], [Lx_ResourceCategoryID], [Lx_ResourceMDFive], [Lx_ResourcePath]) VALUES (95, N'wallhaven.-13vym3_3840x1600.png', N'5.11mb', 2, N'38ed5a28b5376617997f98afeb4397c3', N'Pic\wallhaven.png')
INSERT [dbo].[Lx_Resource] ([Lx_ResourceID], [Lx_ResourceName], [Lx_ResourceSize], [Lx_ResourceCategoryID], [Lx_ResourceMDFive], [Lx_ResourcePath]) VALUES (96, N'鼬.png', N'4.07mb', 2, N'8f68a7438e666f65edcca37e4824918d', N'Pic\鼬.png')
INSERT [dbo].[Lx_Resource] ([Lx_ResourceID], [Lx_ResourceName], [Lx_ResourceSize], [Lx_ResourceCategoryID], [Lx_ResourceMDFive], [Lx_ResourcePath]) VALUES (97, N'(04) [陳奕迅] 黃金時代.flac', N'28.05mb', 4, N'43c0145ea18f8331f4eea4f61eced7ce', N'Music\(04) [陳奕迅] 黃金時代.flac')
INSERT [dbo].[Lx_Resource] ([Lx_ResourceID], [Lx_ResourceName], [Lx_ResourceSize], [Lx_ResourceCategoryID], [Lx_ResourceMDFive], [Lx_ResourcePath]) VALUES (98, N'cover.jpg', N'0.08mb', 2, N'ccdee309e1df1d6a3ca6120a264e8c0a', N'Pic\cover.jpg')
INSERT [dbo].[Lx_Resource] ([Lx_ResourceID], [Lx_ResourceName], [Lx_ResourceSize], [Lx_ResourceCategoryID], [Lx_ResourceMDFive], [Lx_ResourcePath]) VALUES (99, N'vgxw63.jpg', N'1.38mb', 2, N'3a5e160d8218fea55e57f71f304d50da', N'Pic\vgxw63.jpg')
INSERT [dbo].[Lx_Resource] ([Lx_ResourceID], [Lx_ResourceName], [Lx_ResourceSize], [Lx_ResourceCategoryID], [Lx_ResourceMDFive], [Lx_ResourcePath]) VALUES (100, N'海贼王 顶上战争三兄弟5k动漫壁纸_彼岸图网.jpg', N'1.36mb', 2, N'9b759c80c439edd204417d9172344f29', N'Pic\海贼王 顶上战争三兄弟5k动漫壁纸_彼岸图网.jpg')
INSERT [dbo].[Lx_Resource] ([Lx_ResourceID], [Lx_ResourceName], [Lx_ResourceSize], [Lx_ResourceCategoryID], [Lx_ResourceMDFive], [Lx_ResourcePath]) VALUES (101, N'20200320_151343.mp4', N'4.63mb', 3, N'9a8159641954df7515927a2b65546635', N'Video\20200320_151343.mp4')
INSERT [dbo].[Lx_Resource] ([Lx_ResourceID], [Lx_ResourceName], [Lx_ResourceSize], [Lx_ResourceCategoryID], [Lx_ResourceMDFive], [Lx_ResourcePath]) VALUES (104, N'[韩迷字幕组_幻想乐园字幕组联合发布] Running man.E474.170319.720p.rmvb.torrent', N'0.05mb', 7, N'de8ada7a1a65802ea3e8ba22ca44ee74', N'BT\[韩迷字幕组_幻想乐园字幕组联合发布] Running man.torrent')
INSERT [dbo].[Lx_Resource] ([Lx_ResourceID], [Lx_ResourceName], [Lx_ResourceSize], [Lx_ResourceCategoryID], [Lx_ResourceMDFive], [Lx_ResourcePath]) VALUES (105, N'[幻想乐园 韩迷 联合发布综艺][hxly9.com]Running man.E482.170514.720p.rmvb.torrent', N'0.06mb', 7, N'43e5a4ecead895660e324a9cf09a29eb', N'BT\[幻想乐园 韩迷 联合发布综艺][hxly9.torrent')
INSERT [dbo].[Lx_Resource] ([Lx_ResourceID], [Lx_ResourceName], [Lx_ResourceSize], [Lx_ResourceCategoryID], [Lx_ResourceMDFive], [Lx_ResourcePath]) VALUES (106, N'360Safe.exe', N'1.22mb', 8, N'3ed4dc4f68968d588740449969b9f533', N'Other\360Safe.exe')
INSERT [dbo].[Lx_Resource] ([Lx_ResourceID], [Lx_ResourceName], [Lx_ResourceSize], [Lx_ResourceCategoryID], [Lx_ResourceMDFive], [Lx_ResourcePath]) VALUES (107, N'ico_rescue.png', N'0.00mb', 2, N'784ab7b311cb4b86d675c87dc39652cb', N'Pic\ico_rescue.png')
SET IDENTITY_INSERT [dbo].[Lx_Resource] OFF
SET IDENTITY_INSERT [dbo].[Lx_ResourceAccount] ON 

INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (1, N'头像啊', CAST(N'2020-06-13T13:41:00.000' AS DateTime), 2, 7, 0, N'skhgghhglshgilshgilhgrihgg', 0)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (2, N'我的vlog', CAST(N'2020-06-13T16:34:00.000' AS DateTime), 3, 7, 0, N'ashflashfehggwg', 0)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (3, N'我的文档', CAST(N'2020-06-13T16:35:00.000' AS DateTime), 4, 7, 0, N'lhjkldhlwdfafhbjkfh', 0)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (4, N'我的音乐', CAST(N'2020-06-13T16:36:00.000' AS DateTime), 5, 7, 0, N'jkhdjhuidfhuifkdh', 0)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (5, N'我的新建文件夹a', CAST(N'2020-06-28T18:20:24.210' AS DateTime), 6, 7, 0, N'dwbdjkddkwjdwl', 1)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (6, N'我的压缩包', CAST(N'2020-06-13T16:38:00.000' AS DateTime), 7, 7, 0, N'dhjkhdkjdjkhjdk', 0)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (7, N'我的vlog好不好看？', CAST(N'2020-06-28T18:20:24.130' AS DateTime), 3, 7, 5, N'dhkshdkdhjks', 1)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (251, N'是四十.txt', CAST(N'2020-06-28T18:20:24.137' AS DateTime), 92, 7, 5, N'63e784bc-2da1-49fd-a37b-db6fefe846f0', 1)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (257, N'新建文本文档.txt', CAST(N'2020-06-28T18:20:24.140' AS DateTime), 92, 7, 5, N'06a8c5ad-634a-46e9-bb4c-5be5c84f632f', 1)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (260, N'新建文件夹test', CAST(N'2020-06-28T18:20:24.160' AS DateTime), 6, 7, 5, N'dwbdjkddkwjdwl', 1)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (261, N'空文本.txt', CAST(N'2020-06-28T18:20:24.143' AS DateTime), 4, 7, 260, N'63e784bc-2da1-49fd-a37b-db6fefe846f0', 1)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (262, N'新建文件夹test2', CAST(N'2020-06-28T18:20:24.157' AS DateTime), 6, 7, 260, N'dwbdjkddkwjdwl', 1)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (263, N'是四十是.txt', CAST(N'2020-06-28T18:20:24.150' AS DateTime), 4, 7, 262, N'63e784bc-2da1-49fd-a37b-db6fefe846f0', 1)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (264, N'媒体1.mp4', CAST(N'2020-06-28T18:20:24.153' AS DateTime), 94, 7, 262, N'1161e1d3-d064-48d9-8757-b16e70ea7755', 1)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (265, N'深圳夜景4k壁纸_彼岸图网.jpg', CAST(N'2020-06-28T18:20:24.163' AS DateTime), 90, 7, 5, N'754a2a86-fee8-4390-b645-0aa7ddf85a8d', 1)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (267, N'wallhaven.-13vym3_3840x1600', CAST(N'2020-06-28T18:20:24.170' AS DateTime), 95, 7, 5, N'25ec3272-424d-4834-a303-d8595501c3ab', 1)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (268, N'鼬', CAST(N'2020-06-28T18:20:24.173' AS DateTime), 96, 7, 5, N'de3f5194-62fa-4c2b-8c37-1e6213948258', 1)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (269, N'(04) [陳奕迅] 黃金時代', CAST(N'2020-06-28T18:20:24.177' AS DateTime), 97, 7, 5, N'3f9bb5d5-fb90-47b9-b887-ba7448bfa4e7', 1)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (270, N'cover', CAST(N'2020-06-24T18:07:53.473' AS DateTime), 98, 7, 0, N'd7090fca-6209-44fe-9b84-6a4a52196a1e', 0)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (271, N'深圳夜景4k壁纸_彼岸图网.jpg', CAST(N'2020-06-28T18:20:24.180' AS DateTime), 90, 7, 5, N'4c33e5a8-ae2f-4b5c-92b6-85811eb0dc57', 1)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (272, N'vgxw63.jpg', CAST(N'2020-06-28T18:20:24.187' AS DateTime), 90, 7, 5, N'222d58e1-a3b8-4b99-90be-948635c353b8', 1)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (273, N'20190510114443512.png', CAST(N'2020-06-28T18:20:24.190' AS DateTime), 90, 7, 5, N'46953854-9eda-46e1-89d3-aba7022b1abf', 1)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (274, N'vg15v3.jpg', CAST(N'2020-06-28T18:20:24.193' AS DateTime), 90, 7, 5, N'd921f35b-749e-4171-8598-18cee90be687', 1)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (275, N'路飞1.jpg', CAST(N'2020-06-28T18:20:24.197' AS DateTime), 90, 7, 5, N'a5d2730f-24c6-430a-b943-4a8ab490bf30', 1)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (276, N'深圳夜景', CAST(N'2020-06-28T18:20:24.203' AS DateTime), 90, 7, 5, N'84dc944d-8fac-40cd-9078-f9f55f077e96', 1)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (278, N'新建文件夹1', CAST(N'2020-06-28T18:18:09.567' AS DateTime), 6, 7, 0, N'5d4703e8-fa7c-464a-8c52-c100a541c864', 0)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (279, N'新建文件夹', CAST(N'2020-06-28T18:20:24.160' AS DateTime), 6, 7, 260, N'1585d9ab-55bb-4cab-a5c7-137ed5ac5c92', 1)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (280, N'路飞1.jpg', CAST(N'2020-06-28T18:17:54.123' AS DateTime), 95, 7, 278, N'f5c324fd-4667-4f59-8a11-694451140fd5', 0)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (281, N'女孩', CAST(N'2020-06-28T18:17:54.130' AS DateTime), 95, 7, 278, N'f4c06a0d-600a-404f-90b1-df86a144c161', 0)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (282, N'新建文件夹2', CAST(N'2020-06-28T18:18:15.363' AS DateTime), 6, 7, 0, N'3fa719bd-c3cd-4b2e-a4aa-0659d2d59250', 0)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (283, N'新建文件夹', CAST(N'2020-06-28T18:20:26.783' AS DateTime), 6, 7, 0, N'c1e1eb23-5cf3-4624-82e9-58a2d2709ff3', 1)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (284, N'vgxw63', CAST(N'2020-06-26T15:49:42.003' AS DateTime), 99, 7, 282, N'e317e4fd-d826-483a-821b-acfd1b245175', 0)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (285, N'海贼王 顶上战争三兄弟5k动漫壁纸_彼岸图网', CAST(N'2020-06-26T15:49:42.050' AS DateTime), 100, 7, 282, N'dffa4fc4-6599-4456-9397-2aa7e1af967b', 0)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (286, N'Gaki', CAST(N'2020-06-28T18:20:24.207' AS DateTime), 101, 7, 5, N'21c67c88-aa7a-4887-b65f-d5f7ab028df4', 1)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (287, N'Gaki', CAST(N'2020-06-27T14:01:09.373' AS DateTime), 101, 7, 0, N'21573336-93fc-43a5-948c-8b44da4bff16', 0)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (288, N'Running man.E474.180319.720p.rmvb', CAST(N'2020-06-28T18:21:17.337' AS DateTime), 104, 7, 0, N'06ab1135-7399-4442-a5fd-69c42dbb2d07', 0)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (289, N'Running man.E482.170514.720p.rmvb', CAST(N'2020-06-28T18:21:23.397' AS DateTime), 105, 7, 0, N'42aeced6-cd75-42c0-b88e-be4ed23457c3', 1)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (290, N'360Safe', CAST(N'2020-06-29T08:22:35.933' AS DateTime), 106, 7, 0, N'd6f03ca9-a226-4ba6-bbd1-a853bc8f1172', 0)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (291, N'ico_rescue', CAST(N'2020-06-29T08:24:01.273' AS DateTime), 107, 7, 282, N'1cbda8a1-96e0-4ea3-ac04-f09ab8477a30', 0)
SET IDENTITY_INSERT [dbo].[Lx_ResourceAccount] OFF
SET IDENTITY_INSERT [dbo].[Lx_Share] ON 

INSERT [dbo].[Lx_Share] ([Lx_ShareId], [Lx_UserID], [Lx_ResourceAccountID], [Lx_ShareLink], [Lx_ValidCode], [Lx_CodeKey], [Lx_ShareTime], [Lx_CreateTime]) VALUES (49, 7, 288, N'fsLI78WfrDgvyjkYveSa2uSkTuAy9GaUHNFqZPo41qA=', N'6441', N'd1ec968b065843218ab2ad3271f55a44', N'7天', CAST(N'2020-06-27T23:14:41.760' AS DateTime))
INSERT [dbo].[Lx_Share] ([Lx_ShareId], [Lx_UserID], [Lx_ResourceAccountID], [Lx_ShareLink], [Lx_ValidCode], [Lx_CodeKey], [Lx_ShareTime], [Lx_CreateTime]) VALUES (53, 7, 1, N'E9p1yCYbwZdb1Lz11AREnbmOb1BxepgzSTPAIu9kZHI=', N'6916', N'99d500a48832454f9e760e687485e716', N'7天', CAST(N'2020-06-28T12:32:35.190' AS DateTime))
INSERT [dbo].[Lx_Share] ([Lx_ShareId], [Lx_UserID], [Lx_ResourceAccountID], [Lx_ShareLink], [Lx_ValidCode], [Lx_CodeKey], [Lx_ShareTime], [Lx_CreateTime]) VALUES (56, 7, 2, N'lGHJRRQIcHbBxsIaMN1T8SUB3u1o5luwFcGwAIkBqWE=', N'9629', N'ae89da8336994b4cbb98e52cb1dc4a44', N'7天', CAST(N'2020-06-28T12:49:25.563' AS DateTime))
INSERT [dbo].[Lx_Share] ([Lx_ShareId], [Lx_UserID], [Lx_ResourceAccountID], [Lx_ShareLink], [Lx_ValidCode], [Lx_CodeKey], [Lx_ShareTime], [Lx_CreateTime]) VALUES (57, 7, 269, N'EMwF8LRafWKm5gHxIeIzVtLh1vq4yzrRlNbfkFrLdkw=', N'8287', N'2f4aa3acd77e4d8e940e9e0784ddb4b8', N'7天', CAST(N'2020-06-28T12:49:53.013' AS DateTime))
INSERT [dbo].[Lx_Share] ([Lx_ShareId], [Lx_UserID], [Lx_ResourceAccountID], [Lx_ShareLink], [Lx_ValidCode], [Lx_CodeKey], [Lx_ShareTime], [Lx_CreateTime]) VALUES (58, 7, 290, N'QeeaWdaxQIgeFBatMHVke8DX8agAvRWmjEvSNnXye3Q=', N'7633', N'6c88f26ff3e04a268b67900e19a19237', N'30天', CAST(N'2020-06-29T08:24:20.287' AS DateTime))
SET IDENTITY_INSERT [dbo].[Lx_Share] OFF
SET IDENTITY_INSERT [dbo].[Lx_Users] ON 

INSERT [dbo].[Lx_Users] ([Lx_UsersID], [Lx_UsersName], [Lx_UsersPhone], [Lx_UsersLoginName], [Lx_UsersLoginPWD], [Lx_UsersLoginEmail], [Lx_UsersRemark]) VALUES (1, N'李白', N'17673457151', N'libai', N'111Auto', N'chenyigg123@163.com', N'爬')
INSERT [dbo].[Lx_Users] ([Lx_UsersID], [Lx_UsersName], [Lx_UsersPhone], [Lx_UsersLoginName], [Lx_UsersLoginPWD], [Lx_UsersLoginEmail], [Lx_UsersRemark]) VALUES (7, N'DefaultUser', NULL, N'曜', N'3ab8be03f9315c0ee23eb5fbe439d653', N'chenyigg123@163.com', NULL)
INSERT [dbo].[Lx_Users] ([Lx_UsersID], [Lx_UsersName], [Lx_UsersPhone], [Lx_UsersLoginName], [Lx_UsersLoginPWD], [Lx_UsersLoginEmail], [Lx_UsersRemark]) VALUES (8, N'DefaultUser', NULL, N'杜甫', N'3ab8be03f9315c0ee23eb5fbe439d653', N'chenyigg123@163.com', NULL)
INSERT [dbo].[Lx_Users] ([Lx_UsersID], [Lx_UsersName], [Lx_UsersPhone], [Lx_UsersLoginName], [Lx_UsersLoginPWD], [Lx_UsersLoginEmail], [Lx_UsersRemark]) VALUES (13, N'DefaultUser', NULL, N'111奥特曼', N'111', N'chenyigg123@163.com', NULL)
INSERT [dbo].[Lx_Users] ([Lx_UsersID], [Lx_UsersName], [Lx_UsersPhone], [Lx_UsersLoginName], [Lx_UsersLoginPWD], [Lx_UsersLoginEmail], [Lx_UsersRemark]) VALUES (14, N'DefaultUser', NULL, N'赛文', N'3ab8be03f9315c0ee23eb5fbe439d653', N'chenyigg123@163.com', NULL)
INSERT [dbo].[Lx_Users] ([Lx_UsersID], [Lx_UsersName], [Lx_UsersPhone], [Lx_UsersLoginName], [Lx_UsersLoginPWD], [Lx_UsersLoginEmail], [Lx_UsersRemark]) VALUES (15, N'DefaultUser', NULL, N'曜1', N'3ab8be03f9315c0ee23eb5fbe439d653', N'chenyigg123@163.com', NULL)
INSERT [dbo].[Lx_Users] ([Lx_UsersID], [Lx_UsersName], [Lx_UsersPhone], [Lx_UsersLoginName], [Lx_UsersLoginPWD], [Lx_UsersLoginEmail], [Lx_UsersRemark]) VALUES (16, N'DefaultUser', NULL, N'雷欧', N'3ab8be03f9315c0ee23eb5fbe439d653', N'chenyigg123@163.com', NULL)
SET IDENTITY_INSERT [dbo].[Lx_Users] OFF
ALTER TABLE [dbo].[Lx_Users] ADD  CONSTRAINT [DF__Lx_Users__Lx_Use__37A5467C]  DEFAULT ('DefaultUser') FOR [Lx_UsersName]
GO
ALTER TABLE [dbo].[Lx_ResourceAccount]  WITH CHECK ADD  CONSTRAINT [FK_Lx_ResourceAccount_Lx_Resource] FOREIGN KEY([Lx_ResourceID])
REFERENCES [dbo].[Lx_Resource] ([Lx_ResourceID])
GO
ALTER TABLE [dbo].[Lx_ResourceAccount] CHECK CONSTRAINT [FK_Lx_ResourceAccount_Lx_Resource]
GO
ALTER TABLE [dbo].[Lx_ResourceAccount]  WITH CHECK ADD  CONSTRAINT [FK_Lx_ResourceAccount_Lx_Users] FOREIGN KEY([Lx_UsersID])
REFERENCES [dbo].[Lx_Users] ([Lx_UsersID])
GO
ALTER TABLE [dbo].[Lx_ResourceAccount] CHECK CONSTRAINT [FK_Lx_ResourceAccount_Lx_Users]
GO
ALTER TABLE [dbo].[Lx_Share]  WITH CHECK ADD  CONSTRAINT [FK_Lx_Share_Lx_ResourceAccount] FOREIGN KEY([Lx_ResourceAccountID])
REFERENCES [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID])
GO
ALTER TABLE [dbo].[Lx_Share] CHECK CONSTRAINT [FK_Lx_Share_Lx_ResourceAccount]
GO
ALTER TABLE [dbo].[Lx_Share]  WITH CHECK ADD  CONSTRAINT [FK_Lx_Share_Lx_Users] FOREIGN KEY([Lx_UserID])
REFERENCES [dbo].[Lx_Users] ([Lx_UsersID])
GO
ALTER TABLE [dbo].[Lx_Share] CHECK CONSTRAINT [FK_Lx_Share_Lx_Users]
GO
ALTER TABLE [dbo].[Lx_Resource]  WITH CHECK ADD  CONSTRAINT [CK__Resource__Lx_Res__46E78A0C] CHECK  (([Lx_ResourceCategoryID]=(1) OR [Lx_ResourceCategoryID]=(2) OR [Lx_ResourceCategoryID]=(3) OR [Lx_ResourceCategoryID]=(4) OR [Lx_ResourceCategoryID]=(5) OR [Lx_ResourceCategoryID]=(6) OR [Lx_ResourceCategoryID]=(7) OR [Lx_ResourceCategoryID]=(8)))
GO
ALTER TABLE [dbo].[Lx_Resource] CHECK CONSTRAINT [CK__Resource__Lx_Res__46E78A0C]
GO
ALTER TABLE [dbo].[Lx_Share]  WITH CHECK ADD  CONSTRAINT [CK_Lx_Share] CHECK  (([Lx_ShareTime]='7天' OR [Lx_ShareTime]='30天' OR [Lx_ShareTime]='永久有效'))
GO
ALTER TABLE [dbo].[Lx_Share] CHECK CONSTRAINT [CK_Lx_Share]
GO
USE [master]
GO
ALTER DATABASE [LinXi_CloudStorage] SET  READ_WRITE 
GO
