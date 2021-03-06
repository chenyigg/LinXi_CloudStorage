USE [master]
GO
/****** Object:  Database [LinXi_CloudStorage]    Script Date: 2020/6/22 19:24:59 ******/
CREATE DATABASE [LinXi_CloudStorage]
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
/****** Object:  Table [dbo].[Lx_Menus]    Script Date: 2020/6/22 19:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lx_Menus](
	[Lx_MenusID] [int] IDENTITY(1,1) NOT NULL,
	[Lx_MenusName] [nvarchar](50) NOT NULL,
	[Lx_MenusUrl] [nvarchar](50) NOT NULL,
	[Lx_MenusPid] [int] NOT NULL,
	[Lx_MenusIsMenu] [bit] NOT NULL,
	[Lx_MenusRemark] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Lx_MenusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lx_Resource]    Script Date: 2020/6/22 19:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lx_Resource](
	[Lx_ResourceID] [int] IDENTITY(1,1) NOT NULL,
	[Lx_ResourceName] [nvarchar](50) NOT NULL,
	[Lx_ResourceSize] [nvarchar](50) NOT NULL,
	[Lx_ResourceCategoryID] [int] NOT NULL,
	[Lx_ResourceMDFive] [nvarchar](50) NULL,
	[Lx_ResourcePath] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Lx_ResourceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lx_ResourceAccount]    Script Date: 2020/6/22 19:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lx_ResourceAccount](
	[Lx_ResourceAccountID] [int] IDENTITY(1,1) NOT NULL,
	[Lx_ResourceAccountName] [nvarchar](50) NOT NULL,
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
/****** Object:  Table [dbo].[Lx_Roles]    Script Date: 2020/6/22 19:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lx_Roles](
	[Lx_RolesID] [int] IDENTITY(1,1) NOT NULL,
	[Lx_RolesName] [nvarchar](50) NOT NULL,
	[Lx_RolesRemark] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Lx_RolesID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lx_RolesMenus]    Script Date: 2020/6/22 19:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lx_RolesMenus](
	[Lx_RoleMenusID] [int] IDENTITY(1,1) NOT NULL,
	[Lx_RolesID] [int] NOT NULL,
	[Lx_MenusID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Lx_RoleMenusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lx_Share]    Script Date: 2020/6/22 19:24:59 ******/
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
/****** Object:  Table [dbo].[Lx_Users]    Script Date: 2020/6/22 19:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lx_Users](
	[Lx_UsersID] [int] IDENTITY(1,1) NOT NULL,
	[Lx_UsersName] [nvarchar](50) NULL,
	[Lx_UsersPhone] [nvarchar](11) NULL,
	[Lx_UsersLoginName] [nvarchar](50) NOT NULL,
	[Lx_UsersLoginPWD] [nvarchar](40) NOT NULL,
	[Lx_UsersLoginEmail] [nvarchar](40) NOT NULL,
	[Lx_UsersRemark] [nvarchar](50) NULL,
 CONSTRAINT [PK__Lx_Users__6763AF517BBD12A8] PRIMARY KEY CLUSTERED 
(
	[Lx_UsersID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lx_UsersRoles]    Script Date: 2020/6/22 19:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lx_UsersRoles](
	[Lx_UsersRolesID] [int] IDENTITY(1,1) NOT NULL,
	[Lx_UsersID] [int] NOT NULL,
	[Lx_RolesID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Lx_UsersRolesID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
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
SET IDENTITY_INSERT [dbo].[Lx_Resource] OFF
SET IDENTITY_INSERT [dbo].[Lx_ResourceAccount] ON 

INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (1, N'头像啊', CAST(N'2020-06-13T13:41:00.000' AS DateTime), 2, 7, 0, N'skhgghhglshgilshgilhgrihgg', 0)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (2, N'我的vlog', CAST(N'2020-06-13T16:34:00.000' AS DateTime), 3, 7, 0, N'ashflashfehggwg', 0)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (3, N'我的文档', CAST(N'2020-06-13T16:35:00.000' AS DateTime), 4, 7, 0, N'lhjkldhlwdfafhbjkfh', 0)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (4, N'我的音乐', CAST(N'2020-06-13T16:36:00.000' AS DateTime), 5, 7, 0, N'jkhdjhuidfhuifkdh', 0)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (5, N'我的新建文件夹', CAST(N'2020-06-13T16:37:00.000' AS DateTime), 6, 7, 0, N'dwbdjkddkwjdwl', 0)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (6, N'我的压缩包', CAST(N'2020-06-13T16:38:00.000' AS DateTime), 7, 7, 0, N'dhjkhdkjdjkhjdk', 0)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (7, N'我的vlog', CAST(N'2020-06-13T19:48:00.000' AS DateTime), 3, 7, 5, N'dhkshdkdhjks', 0)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (251, N'是四十.txt', CAST(N'2020-06-19T21:02:41.310' AS DateTime), 92, 7, 5, N'63e784bc-2da1-49fd-a37b-db6fefe846f0', 0)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (257, N'新建文本文档.txt', CAST(N'2020-06-19T23:21:53.343' AS DateTime), 92, 7, 5, N'06a8c5ad-634a-46e9-bb4c-5be5c84f632f', 0)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (260, N'新建文件夹test', CAST(N'2020-06-13T16:37:00.000' AS DateTime), 6, 7, 5, N'dwbdjkddkwjdwl', 0)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (261, N'空文本.txt', CAST(N'2020-06-20T23:41:50.503' AS DateTime), 4, 7, 260, N'63e784bc-2da1-49fd-a37b-db6fefe846f0', 1)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (262, N'新建文件夹test2', CAST(N'2020-06-13T16:37:00.000' AS DateTime), 6, 7, 260, N'dwbdjkddkwjdwl', 0)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (263, N'是四十是.txt', CAST(N'2020-06-19T21:02:41.310' AS DateTime), 4, 7, 262, N'63e784bc-2da1-49fd-a37b-db6fefe846f0', 1)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (264, N'媒体1.mp4', CAST(N'2020-06-20T01:18:55.860' AS DateTime), 94, 7, 262, N'1161e1d3-d064-48d9-8757-b16e70ea7755', 0)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (265, N'深圳夜景4k壁纸_彼岸图网.jpg', CAST(N'2020-06-20T14:23:00.487' AS DateTime), 90, 7, 5, N'754a2a86-fee8-4390-b645-0aa7ddf85a8d', 0)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (267, N'wallhaven.-13vym3_3840x1600', CAST(N'2020-06-20T16:42:27.010' AS DateTime), 95, 7, 5, N'25ec3272-424d-4834-a303-d8595501c3ab', 0)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (268, N'鼬', CAST(N'2020-06-20T16:43:39.747' AS DateTime), 96, 7, 5, N'de3f5194-62fa-4c2b-8c37-1e6213948258', 0)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (269, N'(04) [陳奕迅] 黃金時代', CAST(N'2020-06-20T17:29:41.030' AS DateTime), 97, 7, 5, N'3f9bb5d5-fb90-47b9-b887-ba7448bfa4e7', 0)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (270, N'covers', CAST(N'2020-06-21T16:52:58.633' AS DateTime), 98, 7, 0, N'd7090fca-6209-44fe-9b84-6a4a52196a1e', 0)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (271, N'深圳夜景4k壁纸_彼岸图网.jpg', CAST(N'2020-06-21T15:45:42.257' AS DateTime), 90, 7, 5, N'4c33e5a8-ae2f-4b5c-92b6-85811eb0dc57', 0)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (272, N'vgxw63.jpg', CAST(N'2020-06-21T15:45:42.417' AS DateTime), 90, 7, 5, N'222d58e1-a3b8-4b99-90be-948635c353b8', 0)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (273, N'20190510114443512.png', CAST(N'2020-06-21T15:45:42.420' AS DateTime), 90, 7, 5, N'46953854-9eda-46e1-89d3-aba7022b1abf', 0)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (274, N'vg15v3.jpg', CAST(N'2020-06-21T15:45:42.423' AS DateTime), 90, 7, 5, N'd921f35b-749e-4171-8598-18cee90be687', 0)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (275, N'路飞1.jpg', CAST(N'2020-06-21T15:45:42.427' AS DateTime), 90, 7, 5, N'a5d2730f-24c6-430a-b943-4a8ab490bf30', 0)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (276, N'深圳夜景', CAST(N'2020-06-21T23:29:43.207' AS DateTime), 90, 7, 5, N'84dc944d-8fac-40cd-9078-f9f55f077e96', 0)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (278, N'新建文件夹', CAST(N'2020-06-21T16:56:03.307' AS DateTime), 6, 7, 0, N'5d4703e8-fa7c-464a-8c52-c100a541c864', 0)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (279, N'新建文件夹', CAST(N'2020-06-21T16:56:24.663' AS DateTime), 6, 7, 260, N'1585d9ab-55bb-4cab-a5c7-137ed5ac5c92', 0)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (280, N'路飞1.jpg', CAST(N'2020-06-21T16:56:51.467' AS DateTime), 95, 7, 278, N'f5c324fd-4667-4f59-8a11-694451140fd5', 1)
INSERT [dbo].[Lx_ResourceAccount] ([Lx_ResourceAccountID], [Lx_ResourceAccountName], [Lx_ResourceAccountTime], [Lx_ResourceID], [Lx_UsersID], [Lx_Pid], [Lx_Guid], [Lx_Recycle]) VALUES (281, N'女孩', CAST(N'2020-06-21T16:57:51.740' AS DateTime), 95, 7, 278, N'f4c06a0d-600a-404f-90b1-df86a144c161', 0)
SET IDENTITY_INSERT [dbo].[Lx_ResourceAccount] OFF
SET IDENTITY_INSERT [dbo].[Lx_Share] ON 

INSERT [dbo].[Lx_Share] ([Lx_ShareId], [Lx_UserID], [Lx_ResourceAccountID], [Lx_ShareLink], [Lx_ValidCode], [Lx_CodeKey], [Lx_ShareTime], [Lx_CreateTime]) VALUES (20, 7, 270, N'iQ7SJ1S1RAps9LIzp0r7LDyamZpxweuENwdnWP5i4tc=', N'2611', N'fe903771a4084d5fa640b683a92a93fe', N'7天', CAST(N'2020-06-22T18:08:39.217' AS DateTime))
INSERT [dbo].[Lx_Share] ([Lx_ShareId], [Lx_UserID], [Lx_ResourceAccountID], [Lx_ShareLink], [Lx_ValidCode], [Lx_CodeKey], [Lx_ShareTime], [Lx_CreateTime]) VALUES (21, 7, 1, N'LVWsGKP6doMOyOxXNU7lO5cQCSSBPPdF3KnG0eKO7ac=', N'4099', N'b6942698ce51492994c06453eaf50713', N'30天', CAST(N'2020-06-22T18:10:27.407' AS DateTime))
INSERT [dbo].[Lx_Share] ([Lx_ShareId], [Lx_UserID], [Lx_ResourceAccountID], [Lx_ShareLink], [Lx_ValidCode], [Lx_CodeKey], [Lx_ShareTime], [Lx_CreateTime]) VALUES (22, 7, 3, N'7Iau91Jgq52aSpFk1RGyZH81xA6AfN2xd7rmiIAqbt4=', N'3683', N'ee71124c16704eeb87823149f8fcc6d0', N'7天', CAST(N'2020-06-22T18:59:00.103' AS DateTime))
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
ALTER TABLE [dbo].[Lx_RolesMenus]  WITH CHECK ADD FOREIGN KEY([Lx_MenusID])
REFERENCES [dbo].[Lx_Menus] ([Lx_MenusID])
GO
ALTER TABLE [dbo].[Lx_RolesMenus]  WITH CHECK ADD FOREIGN KEY([Lx_RolesID])
REFERENCES [dbo].[Lx_Roles] ([Lx_RolesID])
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
ALTER TABLE [dbo].[Lx_UsersRoles]  WITH CHECK ADD FOREIGN KEY([Lx_RolesID])
REFERENCES [dbo].[Lx_Roles] ([Lx_RolesID])
GO
ALTER TABLE [dbo].[Lx_UsersRoles]  WITH CHECK ADD  CONSTRAINT [FK__Lx_UsersR__Lx_Us__3D5E1FD2] FOREIGN KEY([Lx_UsersID])
REFERENCES [dbo].[Lx_Users] ([Lx_UsersID])
GO
ALTER TABLE [dbo].[Lx_UsersRoles] CHECK CONSTRAINT [FK__Lx_UsersR__Lx_Us__3D5E1FD2]
GO
ALTER TABLE [dbo].[Lx_Resource]  WITH CHECK ADD  CONSTRAINT [CK__Resource__Lx_Res__46E78A0C] CHECK  (([Lx_ResourceCategoryID]=(1) OR [Lx_ResourceCategoryID]=(2) OR [Lx_ResourceCategoryID]=(3) OR [Lx_ResourceCategoryID]=(4) OR [Lx_ResourceCategoryID]=(5) OR [Lx_ResourceCategoryID]=(6) OR [Lx_ResourceCategoryID]=(7)))
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
