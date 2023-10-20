CREATE DATABASE TRUE_CONCEPT
GO
USE TRUE_CONCEPT
GO

CREATE TABLE Account
(
	ID INT PRIMARY KEY IDENTITY,
	UserName VARCHAR(30),
	Password VARCHAR(20),
	Decentralization NVARCHAR(10) DEFAULT N'Client',
	Status VARCHAR(10) DEFAULT 'ON'
)	
GO

CREATE TABLE Users
(
	IDCustomer INT FOREIGN KEY REFERENCES Account(ID),
	FullName NVARCHAR(50),
	Address NVARCHAR(300),
	Email VARCHAR(50),
	NumberPhone VARCHAR(20),
	PRIMARY KEY(IDCustomer)
)
GO

CREATE TABLE Category
(
	IDCategory INT PRIMARY KEY IDENTITY,
	NameCategory NVARCHAR(100),
	ImgUrl NVARCHAR(1000)
)
GO

CREATE TABLE Product
(
	ID INT PRIMARY KEY IDENTITY,
	NameProduct NVARCHAR(200),
	PriceOld FLOAT,
	NewPrice FLOAT,
	Unit NVARCHAR(20),
	Quantity FLOAT,
	Description NVARCHAR(MAX),
	Img_Url NVARCHAR(1000),
	Status NVARCHAR(20),
	IDCategory INT FOREIGN KEY REFERENCES Category(IDCategory),
	VAT FLOAT,
)
GO
CREATE TABLE ItemCart
(
	ID INT FOREIGN KEY REFERENCES Account(ID),
	IDProduct INT FOREIGN KEY REFERENCES Product(ID),
	Quantity FLOAT,
	PRIMARY KEY(ID,IDProduct)
)
GO
CREATE TABLE Orders
(
	IDOrder INT PRIMARY KEY IDENTITY,
	IDCustomer INT FOREIGN KEY REFERENCES Users(IDCustomer),
	OrderDate DATETIME DEFAULT GETDATE(),
	TongTien FLOAT,
	Reduce FLOAT,
	ThanhTien FLOAT
)
GO
CREATE TABLE OrderDetails
(
	IDOrder INT FOREIGN KEY REFERENCES Orders(IDOrder),
	IDProduct INT FOREIGN KEY REFERENCES Product(ID),
	Quantity FLOAT,
	Price FLOAT,
	VAT FLOAT,
	TotalMoney FLOAT,
	PRIMARY KEY(IDOrder,IDProduct)
)
GO
CREATE TABLE BaoCaoNhap
(
	MaPhieuNhap INT PRIMARY KEY IDENTITY,
	NgayNhap DATE DEFAULT GETDATE(),
	TongTien FLOAT,
	GiamGia FLOAT,
	ThanhTien FLOAT,
)
GO
CREATE TABLE ChiTietPhieuNhap
(
	MaPhieuNhap INT FOREIGN KEY REFERENCES BaoCaoNhap(MaPhieuNhap),
	IDProduct INT FOREIGN KEY REFERENCES Product(ID),
	Quantity FLOAT,
	VAT FLOAT,
	Price FLOAT,
	TONGTIEN FLOAT,
	PRIMARY KEY (MaPhieuNhap,IDProduct)
)
go
CREATE TABLE ThueDuAn
(
	MaDuAn INT PRIMARY KEY IDENTITY,
	IDCustomer INT FOREIGN KEY REFERENCES Users(IDCustomer),
	AddressCustomer NVARCHAR(1000),
	NgayThueDuAn DATE DEFAULT GETDATE(),
	NgayDuKienHoanThanh DATE,
	NganSachDuKien FLOAT,
	YeuCauThietKe NVARCHAR(500),
	KieuThietKe NVARCHAR(200),
	MoTaThem NVARCHAR(1000),
	PhiPhatSinh FLOAT,
	TongTien FLOAT
)
GO
CREATE TABLE ChiTietDuAn
(
	MaDuAn INT FOREIGN KEY REFERENCES ThueDuAn(MaDuAn),
	MaSP INT FOREIGN KEY REFERENCES Product(ID),
	SoLuong FLOAT,
	PRIMARY KEY (MaDuAn,MaSP)
)
GO
CREATE TABLE Project
(
	ID INT PRIMARY KEY IDENTITY,
	NameProject NVARCHAR(200),
	MoTaDuAn NVARCHAR(MAX),
	KinhPhi FLOAT
)

------------------INDEX----------------------
CREATE INDEX Category_Index ON Category(IDCategory)
GO


------------------THÊM DỮ LIỆU --------------------------------
INSERT INTO Category(NameCategory)
	VALUES (N'Sofa'),
			(N'Ghế thư giãn'),
			(N'Bàn ăn'),
			(N'Ghế ăn'),
			(N'Giường ngủ'),
			(N'Bàn làm việc')