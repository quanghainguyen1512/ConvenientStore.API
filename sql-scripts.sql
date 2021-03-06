USE [master]
GO
/****** Object:  Database [convenient-store]    Script Date: 18-Jun-18 4:10:56 PM ******/
CREATE DATABASE [convenient-store]
GO
ALTER DATABASE [convenient-store] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [convenient-store].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [convenient-store] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [convenient-store] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [convenient-store] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [convenient-store] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [convenient-store] SET ARITHABORT OFF 
GO
ALTER DATABASE [convenient-store] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [convenient-store] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [convenient-store] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [convenient-store] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [convenient-store] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [convenient-store] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [convenient-store] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [convenient-store] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [convenient-store] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [convenient-store] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [convenient-store] SET ALLOW_SNAPSHOT_ISOLATION ON 
GO
ALTER DATABASE [convenient-store] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [convenient-store] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [convenient-store] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [convenient-store] SET  MULTI_USER 
GO
ALTER DATABASE [convenient-store] SET DB_CHAINING OFF 
GO
ALTER DATABASE [convenient-store] SET ENCRYPTION ON
GO
ALTER DATABASE [convenient-store] SET QUERY_STORE = ON
GO
ALTER DATABASE [convenient-store] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 7), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 10, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO)
GO
USE [convenient-store]
GO
ALTER DATABASE SCOPED CONFIGURATION SET ELEVATE_ONLINE = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET ELEVATE_RESUMABLE = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET ISOLATE_SECURITY_POLICY_CARDINALITY = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET OPTIMIZE_FOR_AD_HOC_WORKLOADS = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET XTP_PROCEDURE_EXECUTION_STATISTICS = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET XTP_QUERY_EXECUTION_STATISTICS = OFF;
GO
USE [convenient-store]
GO
/****** Object:  Table [dbo].[account]    Script Date: 18-Jun-18 4:11:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[account](
	[AccountId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](20) NOT NULL,
	[Password] [nvarchar](20) NOT NULL,
	[RoleId] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[bill]    Script Date: 18-Jun-18 4:11:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bill](
	[BillId] [int] IDENTITY(1,1) NOT NULL,
	[StaffId] [int] NOT NULL,
	[CreatedDateTime] [datetime] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[bill_detail]    Script Date: 18-Jun-18 4:11:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bill_detail](
	[BillDetailId] [int] IDENTITY(1,1) NOT NULL,
	[BarCode] [nvarchar](15) NOT NULL,
	[BillId] [int] NOT NULL,
	[Quantity] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[category]    Script Date: 18-Jun-18 4:11:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[category](
	[CategoryId] [int] NOT NULL,
	[Name] [nvarchar](45) NOT NULL,
	[ParentCategoryId] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[customer]    Script Date: 18-Jun-18 4:11:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[customer](
	[CustomerId] [int] NOT NULL,
	[FirstName] [nvarchar](10) NOT NULL,
	[LastName] [nvarchar](10) NOT NULL,
	[DateOfBirth] [datetime] NULL,
	[PhoneNumber] [nvarchar](13) NULL,
	[Email] [nvarchar](20) NULL,
	[TypeId] [int] NOT NULL,
	[Gender] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[customer_type]    Script Date: 18-Jun-18 4:11:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[customer_type](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](20) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[delivery]    Script Date: 18-Jun-18 4:11:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[delivery](
	[DeliveryId] [int] IDENTITY(1,1) NOT NULL,
	[DeliveryDate] [datetime] NOT NULL,
	[Cost] [int] NULL,
	[SupplierId] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[export_monitor]    Script Date: 18-Jun-18 4:11:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[export_monitor](
	[BarCode] [nvarchar](15) NULL,
	[ExportedQuantity] [int] NULL,
	[ExportedDateTime] [datetime] NULL,
	[ProductId] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[has_permission]    Script Date: 18-Jun-18 4:11:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[has_permission](
	[RoleId] [int] NOT NULL,
	[PermissionId] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[order_action]    Script Date: 18-Jun-18 4:11:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[order_action](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[OrderDateTime] [datetime] NOT NULL,
	[StaffId] [int] NULL,
	[SupplierId] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[order_detail]    Script Date: 18-Jun-18 4:11:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[order_detail](
	[OrderDetailId] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[OrderId] [int] NOT NULL,
	[ProductQuantity] [int] NOT NULL,
	[IsReceived] [bit] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[permission]    Script Date: 18-Jun-18 4:11:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[permission](
	[PermissionId] [int] NOT NULL,
	[Description] [nvarchar](100) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[product]    Script Date: 18-Jun-18 4:11:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product](
	[ProductId] [int] NOT NULL,
	[SupId] [int] NOT NULL,
	[CateId] [int] NOT NULL,
	[Name] [nvarchar](45) NOT NULL,
	[Unit] [nvarchar](10) NULL,
	[ImageUrl] [nvarchar](100) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[product_detail]    Script Date: 18-Jun-18 4:11:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product_detail](
	[BarCode] [nvarchar](15) NOT NULL,
	[QuantityOnStore] [int] NOT NULL,
	[QuantityInRepository] [int] NOT NULL,
	[Price] [int] NOT NULL,
	[ExpirationDate] [date] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[role]    Script Date: 18-Jun-18 4:11:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[role](
	[RoleId] [int] NOT NULL,
	[Name] [nvarchar](45) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[shipment]    Script Date: 18-Jun-18 4:11:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[shipment](
	[ShipmentId] [int] IDENTITY(1,1) NOT NULL,
	[DeliveryId] [int] NULL,
	[OrderDetailId] [int] NULL,
	[BarCode] [nvarchar](15) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[staff]    Script Date: 18-Jun-18 4:11:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[staff](
	[StaffId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](10) NOT NULL,
	[LastName] [nvarchar](10) NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[Gender] [bit] NOT NULL,
	[PhoneNumber] [nvarchar](13) NOT NULL,
	[IdentityNumber] [nvarchar](12) NOT NULL,
	[AccountId] [int] NULL,
	[ImageUrl] [varchar](100) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[supplier]    Script Date: 18-Jun-18 4:11:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[supplier](
	[SupplierId] [int] IDENTITY(1,1) NOT NULL,
	[SupplierName] [nvarchar](45) NOT NULL,
	[Address] [nvarchar](45) NULL,
	[PhoneNumber] [nvarchar](13) NOT NULL,
	[Email] [nvarchar](40) NULL
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[USP_ExportFromRepo]    Script Date: 18-Jun-18 4:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[USP_ExportFromRepo]
  @quantity /*parameter name*/ INT /*datatype_for_param1*/ = 0, /*default_value_for_param1*/
  @barcode /*parameter name*/ VARCHAR(15) /*datatype_for_param1*/ = '' /*default_value_for_param2*/
-- add more stored procedure parameters here
AS
  UPDATE dbo.product_detail
  SET QuantityOnStore = QuantityOnStore + @quantity, 
      QuantityInRepository = QuantityInRepository - @quantity
  WHERE BarCode = @barcode
  DECLARE @proId INT
  SELECT @proId = od.ProductId FROM shipment AS sh
  INNER JOIN order_detail AS od ON od.OrderDetailId = sh.OrderDetailId
  WHERE sh.BarCode = @barcode
  INSERT INTO dbo.export_monitor VALUES (@barcode, @quantity, GETDATE(), @proId)
GO
/****** Object:  StoredProcedure [dbo].[USP_GetOneProductDetail]    Script Date: 18-Jun-18 4:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GetOneProductDetail]
  @barcode nvarchar(15) = ''
AS
  SELECT pd.BarCode, pd.ExpirationDate, pd.Price, pd.QuantityOnStore, pd.QuantityInRepository,
          p.ProductId, p.Name, p.ImageUrl, p.Unit,
          s.SupplierId, s.SupplierName,
          c.CategoryId, c.Name,
          e.ExportedDateTime, e.ExportedQuantity
  FROM product_detail AS pd
    INNER JOIN shipment AS sh ON sh.BarCode = pd.BarCode
    INNER JOIN order_detail AS od ON od.OrderDetailId = sh.ShipmentId
    INNER JOIN product AS p ON p.ProductId = od.ProductId
    INNER JOIN category AS c ON c.CategoryId = p.ProductId
    INNER JOIN supplier AS s ON s.SupplierId = p.SupId
    INNER JOIN export_monitor AS e ON e.BarCode = pd.BarCode
  WHERE pd.BarCode LIKE @barcode
GO
/****** Object:  StoredProcedure [dbo].[USP_GetProducDetailsWithPagination]    Script Date: 18-Jun-18 4:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[USP_GetProducDetailsWithPagination]
  @page /*parameter name*/ int /*datatype_for_param1*/ = 1, /*default_value_for_param1*/
  @size /*parameter name*/ int /*datatype_for_param1*/ = 10 /*default_value_for_param2*/,
  @searchQuery nvarchar(100),
  @totalCount int OUTPUT
AS
  DECLARE @firstRow INT = (1 + @size * (@page - 1));
  DECLARE @lastRow INT = @firstRow + @size - 1;
  DECLARE @temp TABLE (
	RowNum INT,
	BarCode varchar(15),
	QuantityOnStore INT,
	QuantityInRepository INT,
	Price INT, 
	Name nvarchar(45),
	Unit nvarchar(10),
	ImageUrl nvarchar(100)
  );
  INSERT INTO @temp
	SELECT ROW_NUMBER() OVER (ORDER BY p.Name) AS RowNum, 
		pd.BarCode, pd.QuantityOnStore, pd.QuantityInRepository, pd.Price, p.Name, p.Unit, p.ImageUrl
    FROM product_detail AS pd
    INNER JOIN shipment AS sh ON sh.BarCode = pd.BarCode
    INNER JOIN order_detail AS od ON od.OrderDetailId = sh.OrderDetailId
    INNER JOIN product AS p ON p.ProductId = od.ProductId
	WHERE @searchQuery IS NULL 
            OR @searchQuery = '' 
			OR pd.BarCode LIKE '%' + @searchQuery + '%'
			OR p.Name LIKE '%' + @searchQuery + '%'
            AND pd.QuantityInRepository <> 0

  SELECT R.BarCode, R.QuantityOnStore, R.QuantityInRepository, R.Price, R.Name, R.Unit, R.ImageUrl
  FROM @temp AS  R
  WHERE RowNum >= @firstRow AND RowNum < @lastRow
  ORDER BY R.Name

  SELECT @totalCount = COUNT(*) FROM @temp
GO
/****** Object:  StoredProcedure [dbo].[USP_ImExReportByDay]    Script Date: 18-Jun-18 4:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_ImExReportByDay]
    @date DATE,
    @productId INT = -1,
    @printTables BIT = 1,
    @imp INT OUTPUT,
    @exp INT OUTPUT
AS
    IF @printTables = 1
        BEGIN
            SELECT o.OrderId, od.ProductQuantity, p.Name FROM order_detail AS od
            INNER JOIN order_action AS o ON o.OrderId = od.OrderId
            INNER JOIN product AS p ON p.ProductId = od.ProductId
            WHERE od.IsReceived = 1 AND DATEDIFF(D, o.OrderDateTime, @date) = 0 AND p.ProductId = @productId
            
            SELECT e.BarCode, p.Name, e.ExportedQuantity, e.ExportedDateTime 
            FROM export_monitor AS e
            INNER JOIN product AS p ON p.ProductId = e.ProductId
            WHERE DATEDIFF(D, ExportedDateTime, @date) = 0 AND e.ProductId = @productId
        END

    SELECT @exp = SUM(e.ExportedQuantity) FROM export_monitor AS e
    WHERE DATEDIFF(D, ExportedDateTime, @date) = 0 AND e.ProductId = @productId
    IF @exp IS NULL
        SELECT @exp = 0
    SELECT @imp = SUM(od.ProductQuantity) FROM order_detail AS od
    INNER JOIN order_action AS o ON o.OrderId = od.OrderId
    INNER JOIN product AS p ON p.ProductId = od.ProductId
    WHERE od.IsReceived = 1 AND DATEDIFF(D, o.OrderDateTime, @date) = 0 AND p.ProductId = @productId
    IF @imp IS NULL
        SELECT @imp = 0
GO
/****** Object:  StoredProcedure [dbo].[USP_ImExReportByMonth]    Script Date: 18-Jun-18 4:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Create the stored procedure in the specified schema
CREATE PROCEDURE [dbo].[USP_ImExReportByMonth]
    @date DATE,
    @productId INT = -1,
    @printTables BIT = 1,
    @imp INT OUTPUT,
    @exp INT OUTPUT
AS
    DECLARE @day DATE,
            @impPDay INT,
            @expPDay INT
    DECLARE @result TABLE 
    (
        _day INT IDENTITY(1,1) NOT NULL,
        impPerDay INT,
        expPerDay INT
    );
    SELECT @day = DATEADD(d, -1 * DATEPART(D, @date) + 1, @date)
    WHILE DATEDIFF(M, @day, @date) = 0
    BEGIN
      EXEC USP_ImExReportByDay @day, @productId, 0, @impPDay OUTPUT, @expPDay OUTPUT
      INSERT INTO @result (impPerDay, expPerDay)
      VALUES (@impPDay, @expPDay)
      SELECT @day = DATEADD(D, 1, @day)
    END

    SELECT @imp = SUM(r.impPerDay), @exp = SUM(r.expPerDay) FROM @result AS r
    IF @printTables = 1
        SELECT * FROM @result
GO
/****** Object:  StoredProcedure [dbo].[USP_ImExReportByYear]    Script Date: 18-Jun-18 4:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Create the stored procedure in the specified schema
CREATE PROCEDURE [dbo].[USP_ImExReportByYear]
    @date DATE,
    @productId INT = -1,
    @printTables BIT = 1,
    @imp INT OUTPUT,
    @exp INT OUTPUT
AS
    DECLARE @month DATE,
        @impPMonth INT,
        @expPMonth INT
    DECLARE @result TABLE 
    (
        _month INT IDENTITY(1,1) NOT NULL,
        impPerMonth INT,
        expPerMonth INT
    );
    SELECT @month = DATEADD(M, -1 * DATEPART(M, @date) + 1, @date)
    WHILE (DATEDIFF(YEAR, @date, @month)) = 0
        BEGIN
            EXEC USP_ImExReportByMonth @month, @productId, 0, @impPMonth OUTPUT, @expPMonth OUTPUT
            INSERT INTO @result (impPerMonth, expPerMonth)
            VALUES (@impPMonth, @expPMonth)
            SELECT @month = DATEADD(M, 1, @month)
        END
    SELECT @imp = SUM(r.impPerMonth), @exp = SUM(r.expPerMonth) FROM @result AS r
    IF @printTables = 1
        SELECT * FROM @result
GO
/****** Object:  StoredProcedure [dbo].[USP_RevenueByDay]    Script Date: 18-Jun-18 4:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_RevenueByDay]
  @date /*parameter name*/ DATE /*datatype_for_param1*/ = '2018-01-01' /*default_value_for_param1*/,
  @printTables BIT = 1,
  @in INT OUTPUT,
  @out INT OUTPUT,
  @revenue INT OUTPUT
-- add more stored procedure parameters here
AS
  DECLARE @billTable TABLE (
          BillId int,
          CreatedDateTime DATETIME,
          SumPerBill int
        );
    
  INSERT INTO @billTable
    SELECT b.BillId, b.CreatedDateTime, 
            SUM(bd.Quantity * pd.Price) AS SumPerBill
    FROM bill AS b
    INNER JOIN bill_detail AS bd ON b.BillId = bd.BillId
    INNER JOIN product_detail AS pd ON pd.BarCode = bd.BarCode
    WHERE DATEDIFF(D, b.CreatedDateTime, @date) = 0
    GROUP BY b.BillId, b.CreatedDateTime
  
  IF @printTables = 1
    BEGIN
        SELECT * FROM @billTable
        SELECT d.DeliveryId, d.DeliveryDate, d.Cost FROM delivery as d 
        WHERE DATEDIFF(D, DeliveryDate, @date) = 0
    END

  SELECT @out = SUM(Cost) FROM delivery
  WHERE DATEDIFF(D, DeliveryDate, @date) = 0
  IF @out IS NULL
    SELECT @out = 0
  SELECT @in = SUM(SumPerBill) FROM @billTable
  IF @in IS NULL 
    SELECT @in = 0
  SELECT @revenue = @in - @out
GO
/****** Object:  StoredProcedure [dbo].[USP_RevenueByMonth]    Script Date: 18-Jun-18 4:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[USP_RevenueByMonth]
  @date date,
  @printTables BIT = 1,
  @in int = 0 OUTPUT,
  @out int = 0 OUTPUT,
  @revenue int = 0 OUTPUT
AS
  DECLARE @day DATE,
          @inPDay INT,
          @outPDay INT,
          @revPDay INT

  DECLARE @result TABLE 
  (
    _day INT IDENTITY(1,1) NOT NULL,
    inPerDay INT,
    outPerDay INT,
    revPerDay INT
  );
  SELECT @day = DATEADD(d, -1 * DATEPART(D, @date) + 1, @date)
  WHILE DATEDIFF(M, @day, @date) = 0
    BEGIN
      EXEC USP_RevenueByDay @day, 0, @inPDay OUTPUT, @outPDay OUTPUT, @revPDay OUTPUT
      INSERT INTO @result (inPerDay, outPerDay, revPerDay)
      VALUES (@inPDay, @outPDay, @revPDay)
      SELECT @day = DATEADD(D, 1, @day)
    END

  SELECT @in = SUM(r.inPerDay), @out = SUM(r.outPerDay), @revenue = SUM(r.revPerDay) 
  FROM @result as r
  IF @printTables = 1
    SELECT * FROM @result
GO
/****** Object:  StoredProcedure [dbo].[USP_RevenueByYear]    Script Date: 18-Jun-18 4:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_RevenueByYear]
  @date date,
  @printTables BIT = 1,
  @in int = 0 OUTPUT,
  @out int = 0 OUTPUT,
  @revenue int = 0 OUTPUT
AS
    DECLARE @month DATE,
        @inPMonth INT,
        @outPMonth INT,
        @revPMonth INT
    DECLARE @result TABLE 
    (
        _month INT IDENTITY(1,1) NOT NULL,
        inPerMonth INT,
        outPerMonth INT,
        revPerMonth INT
    );
    SELECT @month = DATEADD(M, -1 * DATEPART(M, @date) + 1, @date)
    WHILE (DATEDIFF(YEAR, @date, @month)) = 0
        BEGIN
            EXEC USP_RevenueByMonth @month, 0, @inPMonth OUTPUT, @outPMonth OUTPUT, @revPMonth OUTPUT
            INSERT INTO @result (inPerMonth, outPerMonth, revPerMonth)
            VALUES (@inPMonth, @outPMonth, @revPMonth)
            SELECT @month = DATEADD(M, 1, @month)
        END
    SELECT @in = SUM(r.inPerMonth), @out = SUM(r.outPerMonth), @revenue = SUM(r.revPerMonth)
    FROM @result as r
    IF @printTables = 1
        SELECT * FROM @result
GO
USE [master]
GO
ALTER DATABASE [convenient-store] SET  READ_WRITE 
GO
