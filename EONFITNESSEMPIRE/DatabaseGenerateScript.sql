USE [master]
GO
/****** Object:  Database [DbGym]    Script Date: 2/4/2018 11:55:56 PM ******/
CREATE DATABASE [DbGym]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DbGym', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\DbGym.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'DbGym_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\DbGym_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [DbGym] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DbGym].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DbGym] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DbGym] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DbGym] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DbGym] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DbGym] SET ARITHABORT OFF 
GO
ALTER DATABASE [DbGym] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DbGym] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DbGym] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DbGym] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DbGym] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DbGym] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DbGym] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DbGym] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DbGym] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DbGym] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DbGym] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DbGym] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DbGym] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DbGym] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DbGym] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DbGym] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DbGym] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DbGym] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [DbGym] SET  MULTI_USER 
GO
ALTER DATABASE [DbGym] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DbGym] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DbGym] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DbGym] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [DbGym] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'DbGym', N'ON'
GO
USE [DbGym]
GO
/****** Object:  Table [dbo].[Andd]    Script Date: 2/4/2018 11:55:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Andd](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[GymName] [nvarchar](50) NULL,
	[ApplicationEndDate] [datetime] NULL,
	[Flag] [nvarchar](5) NULL,
	[Description] [nvarchar](50) NULL,
 CONSTRAINT [PK_Andd] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Confirm_MemberRegistration]    Script Date: 2/4/2018 11:55:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Confirm_MemberRegistration](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MemberName] [nvarchar](40) NULL,
	[Email] [nvarchar](30) NULL,
	[Password] [nvarchar](50) NULL,
	[DOB] [date] NULL,
	[DOJ] [datetime] NULL,
	[Gender] [nvarchar](10) NULL,
	[Address] [nvarchar](200) NULL,
	[City] [nvarchar](50) NULL,
	[State] [nvarchar](50) NULL,
	[ZipCode] [nvarchar](15) NULL,
	[Employe] [nvarchar](50) NULL,
	[EmergencyContactName] [nvarchar](50) NULL,
	[EmergencyContactNumber] [nvarchar](20) NULL,
	[MobileNumber] [nvarchar](20) NULL,
	[Package_ID] [int] NULL,
	[Payment_Type_ID] [int] NULL,
	[Installment_Method] [nvarchar](50) NULL,
	[ProfilePic] [nvarchar](80) NULL,
	[ConfirmFlag] [nvarchar](10) NULL,
	[Flag] [nvarchar](5) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_Confirm_MemberRegistration] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GymClose]    Script Date: 2/4/2018 11:55:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GymClose](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Mobile] [nvarchar](15) NULL,
	[OnDate] [date] NULL,
	[TotalDays] [int] NULL,
	[flag] [nvarchar](5) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[Description] [nvarchar](300) NULL,
 CONSTRAINT [PK_GymClose] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MemberRegistration]    Script Date: 2/4/2018 11:55:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MemberRegistration](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MemberName] [nvarchar](40) NULL,
	[Email] [nvarchar](30) NULL,
	[Password] [nvarchar](50) NULL,
	[DOJ] [date] NULL,
	[DOB] [date] NULL,
	[Gender] [nvarchar](10) NULL,
	[Address] [nvarchar](200) NULL,
	[City] [nvarchar](50) NULL,
	[State] [nvarchar](50) NULL,
	[ZipCode] [nvarchar](20) NULL,
	[Employe] [nvarchar](50) NULL,
	[EmergencyContactName] [nvarchar](50) NULL,
	[EmergencyContactNumber] [nvarchar](50) NULL,
	[MobileNumber] [nvarchar](20) NULL,
	[Package_ID] [int] NULL,
	[Payment_Type_ID] [int] NULL,
	[Installment_Method] [nvarchar](50) NULL,
	[ProfilePic] [nvarchar](80) NULL,
	[PackageStartDate] [date] NULL,
	[PackageEndDate] [date] NULL,
	[MembershipID] [nchar](15) NULL,
	[Flag] [nvarchar](10) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_MemberRegistration] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PackageDetails]    Script Date: 2/4/2018 11:55:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PackageDetails](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Pkg_Month] [nvarchar](20) NULL,
	[NumberOfMonth] [int] NULL,
	[Pkg_Ammount] [nvarchar](10) NULL,
	[Flag] [nvarchar](5) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [nvarchar](20) NULL,
	[Pkg_Description] [nvarchar](200) NULL,
 CONSTRAINT [PK_PackageDetails] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PaymentDetails]    Script Date: 2/4/2018 11:55:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentDetails](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PaymentType] [nvarchar](10) NULL,
	[PaymentAmmount] [nvarchar](10) NULL,
	[Description] [nvarchar](100) NULL,
	[Flag] [nchar](5) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [nvarchar](20) NULL,
 CONSTRAINT [PK_PaymentDetails] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PhysicalAssessment]    Script Date: 2/4/2018 11:55:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhysicalAssessment](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MemberRegistration_PK_ID] [int] NULL,
	[Name] [nvarchar](20) NULL,
	[Mobile] [nvarchar](20) NULL,
	[Date] [datetime] NULL,
	[Height] [nvarchar](10) NULL,
	[Weight] [nvarchar](10) NULL,
	[Biceps] [nvarchar](10) NULL,
	[Triceps] [nvarchar](10) NULL,
	[Shoulder] [nvarchar](10) NULL,
	[Chest] [nvarchar](10) NULL,
	[Wrist] [nvarchar](10) NULL,
	[Thighs] [nvarchar](10) NULL,
	[Fourarms] [nvarchar](10) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [nvarchar](20) NULL,
 CONSTRAINT [PK_PhysicalAssessment] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TransactionDetail]    Script Date: 2/4/2018 11:55:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransactionDetail](
	[TransactionDetailsID] [int] IDENTITY(1,1) NOT NULL,
	[MemberRegistration_PK_ID] [int] NULL,
	[PackageStartDate] [datetime] NULL,
	[PackageEndDate] [datetime] NULL,
	[PackageDetails_PK_ID] [int] NULL,
	[Payment_Date] [datetime] NULL,
	[MobileNumber] [nvarchar](15) NULL,
	[PaymentType] [nvarchar](15) NULL,
	[Paid_Amount] [decimal](18, 2) NULL,
	[Due_Amount] [decimal](18, 2) NULL,
	[Discount_Amount] [decimal](18, 0) NULL,
	[NextPaymentDate] [datetime] NULL,
	[Advance_Amount] [decimal](18, 2) NULL,
	[TotalPackagePayment] [decimal](18, 2) NULL,
	[Description] [nvarchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [nvarchar](20) NULL,
 CONSTRAINT [PK_TransactionDetails] PRIMARY KEY CLUSTERED 
(
	[TransactionDetailsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TransactionPayment]    Script Date: 2/4/2018 11:55:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransactionPayment](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MemberRegistration_PK_ID] [int] NULL,
	[PaymentDetails_PK_ID] [int] NULL,
	[Payment_Date] [datetime] NULL,
	[MobileNumber] [nvarchar](15) NULL,
	[PaymentType] [nvarchar](15) NULL,
	[Paid_Amount] [nvarchar](10) NULL,
	[Due_Amount] [nvarchar](10) NULL,
	[Total_Payment] [nvarchar](10) NULL,
	[Description] [nvarchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [nvarchar](20) NULL,
 CONSTRAINT [PK_TransactionPayment] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserLogin]    Script Date: 2/4/2018 11:55:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLogin](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[Mobile] [nvarchar](50) NULL,
	[UserRole] [nvarchar](5) NULL,
	[MemberRegistration_PK_ID] [int] NULL,
	[Flag] [nvarchar](5) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_UserLogin] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Confirm_MemberRegistration]  WITH CHECK ADD  CONSTRAINT [FK_Confirm_MemberRegistration_PackageDetails] FOREIGN KEY([Package_ID])
REFERENCES [dbo].[PackageDetails] ([ID])
GO
ALTER TABLE [dbo].[Confirm_MemberRegistration] CHECK CONSTRAINT [FK_Confirm_MemberRegistration_PackageDetails]
GO
ALTER TABLE [dbo].[Confirm_MemberRegistration]  WITH CHECK ADD  CONSTRAINT [FK_Confirm_MemberRegistration_PaymentDetails] FOREIGN KEY([Payment_Type_ID])
REFERENCES [dbo].[PaymentDetails] ([ID])
GO
ALTER TABLE [dbo].[Confirm_MemberRegistration] CHECK CONSTRAINT [FK_Confirm_MemberRegistration_PaymentDetails]
GO
ALTER TABLE [dbo].[MemberRegistration]  WITH CHECK ADD  CONSTRAINT [FK_MemberRegistration_PackageDetails] FOREIGN KEY([Package_ID])
REFERENCES [dbo].[PackageDetails] ([ID])
GO
ALTER TABLE [dbo].[MemberRegistration] CHECK CONSTRAINT [FK_MemberRegistration_PackageDetails]
GO
ALTER TABLE [dbo].[MemberRegistration]  WITH CHECK ADD  CONSTRAINT [FK_MemberRegistration_PaymentDetails] FOREIGN KEY([Payment_Type_ID])
REFERENCES [dbo].[PaymentDetails] ([ID])
GO
ALTER TABLE [dbo].[MemberRegistration] CHECK CONSTRAINT [FK_MemberRegistration_PaymentDetails]
GO
ALTER TABLE [dbo].[PhysicalAssessment]  WITH CHECK ADD  CONSTRAINT [FK_PhysicalAssessment_MemberRegistration] FOREIGN KEY([MemberRegistration_PK_ID])
REFERENCES [dbo].[MemberRegistration] ([ID])
GO
ALTER TABLE [dbo].[PhysicalAssessment] CHECK CONSTRAINT [FK_PhysicalAssessment_MemberRegistration]
GO
ALTER TABLE [dbo].[TransactionDetail]  WITH CHECK ADD  CONSTRAINT [FK_TransactionDetails_MemberRegistration] FOREIGN KEY([MemberRegistration_PK_ID])
REFERENCES [dbo].[MemberRegistration] ([ID])
GO
ALTER TABLE [dbo].[TransactionDetail] CHECK CONSTRAINT [FK_TransactionDetails_MemberRegistration]
GO
ALTER TABLE [dbo].[TransactionDetail]  WITH CHECK ADD  CONSTRAINT [FK_TransactionDetails_PackageDetails] FOREIGN KEY([PackageDetails_PK_ID])
REFERENCES [dbo].[PackageDetails] ([ID])
GO
ALTER TABLE [dbo].[TransactionDetail] CHECK CONSTRAINT [FK_TransactionDetails_PackageDetails]
GO
ALTER TABLE [dbo].[TransactionPayment]  WITH CHECK ADD  CONSTRAINT [FK_TransactionPayment_MemberRegistration] FOREIGN KEY([MemberRegistration_PK_ID])
REFERENCES [dbo].[MemberRegistration] ([ID])
GO
ALTER TABLE [dbo].[TransactionPayment] CHECK CONSTRAINT [FK_TransactionPayment_MemberRegistration]
GO
ALTER TABLE [dbo].[TransactionPayment]  WITH CHECK ADD  CONSTRAINT [FK_TransactionPayment_PaymentDetails] FOREIGN KEY([PaymentDetails_PK_ID])
REFERENCES [dbo].[PaymentDetails] ([ID])
GO
ALTER TABLE [dbo].[TransactionPayment] CHECK CONSTRAINT [FK_TransactionPayment_PaymentDetails]
GO
ALTER TABLE [dbo].[UserLogin]  WITH CHECK ADD  CONSTRAINT [FK_UserLogin_MemberRegistration] FOREIGN KEY([MemberRegistration_PK_ID])
REFERENCES [dbo].[MemberRegistration] ([ID])
GO
ALTER TABLE [dbo].[UserLogin] CHECK CONSTRAINT [FK_UserLogin_MemberRegistration]
GO
USE [master]
GO
ALTER DATABASE [DbGym] SET  READ_WRITE 
GO
