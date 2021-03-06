USE [master]
GO
/****** Object:  Database [LinXi_CloudStorage]    Script Date: 2020/5/31 21:48:57 ******/
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
/****** Object:  Table [dbo].[Lx_Menus]    Script Date: 2020/5/31 21:48:57 ******/
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
/****** Object:  Table [dbo].[Lx_Resource]    Script Date: 2020/5/31 21:48:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lx_Resource](
	[Lx_ResourceID] [int] IDENTITY(1,1) NOT NULL,
	[Lx_ResourceName] [nvarchar](50) NOT NULL,
	[Lx_ResourceSize] [nvarchar](50) NOT NULL,
	[Lx_ResourceCategoryID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Lx_ResourceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lx_ResourceAccount]    Script Date: 2020/5/31 21:48:57 ******/
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
/****** Object:  Table [dbo].[Lx_Roles]    Script Date: 2020/5/31 21:48:57 ******/
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
/****** Object:  Table [dbo].[Lx_RolesMenus]    Script Date: 2020/5/31 21:48:57 ******/
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
/****** Object:  Table [dbo].[Lx_Users]    Script Date: 2020/5/31 21:48:57 ******/
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
/****** Object:  Table [dbo].[Lx_UsersRoles]    Script Date: 2020/5/31 21:48:57 ******/
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
SET IDENTITY_INSERT [dbo].[Lx_Users] ON 

INSERT [dbo].[Lx_Users] ([Lx_UsersID], [Lx_UsersName], [Lx_UsersPhone], [Lx_UsersLoginName], [Lx_UsersLoginPWD], [Lx_UsersLoginEmail], [Lx_UsersRemark]) VALUES (1, N'李白', N'17673457151', N'libai', N'111Auto', N'chenyigg123@163.com', N'爬')
INSERT [dbo].[Lx_Users] ([Lx_UsersID], [Lx_UsersName], [Lx_UsersPhone], [Lx_UsersLoginName], [Lx_UsersLoginPWD], [Lx_UsersLoginEmail], [Lx_UsersRemark]) VALUES (7, N'DefaultUser', NULL, N'曜', N'3ab8be03f9315c0ee23eb5fbe439d653', N'chenyigg123@163.com', NULL)
INSERT [dbo].[Lx_Users] ([Lx_UsersID], [Lx_UsersName], [Lx_UsersPhone], [Lx_UsersLoginName], [Lx_UsersLoginPWD], [Lx_UsersLoginEmail], [Lx_UsersRemark]) VALUES (8, N'DefaultUser', NULL, N'杜甫', N'3ab8be03f9315c0ee23eb5fbe439d653', N'chenyigg123@163.com', NULL)
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
ALTER TABLE [dbo].[Lx_UsersRoles]  WITH CHECK ADD FOREIGN KEY([Lx_RolesID])
REFERENCES [dbo].[Lx_Roles] ([Lx_RolesID])
GO
ALTER TABLE [dbo].[Lx_UsersRoles]  WITH CHECK ADD  CONSTRAINT [FK__Lx_UsersR__Lx_Us__3D5E1FD2] FOREIGN KEY([Lx_UsersID])
REFERENCES [dbo].[Lx_Users] ([Lx_UsersID])
GO
ALTER TABLE [dbo].[Lx_UsersRoles] CHECK CONSTRAINT [FK__Lx_UsersR__Lx_Us__3D5E1FD2]
GO
ALTER TABLE [dbo].[Lx_Resource]  WITH CHECK ADD CHECK  (([Lx_ResourceCategoryID]=(1) OR [Lx_ResourceCategoryID]=(2) OR [Lx_ResourceCategoryID]=(3) OR [Lx_ResourceCategoryID]=(4) OR [Lx_ResourceCategoryID]=(5) OR [Lx_ResourceCategoryID]=(6)))
GO
USE [master]
GO
ALTER DATABASE [LinXi_CloudStorage] SET  READ_WRITE 
GO
