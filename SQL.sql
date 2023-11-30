
-------------  BẤM  F5 ĐỂ RUN CODE VỚI 1 LẦN NHẤN --------------------

CREATE DATABASE TRUE_CONCEPT
GO
USE TRUE_CONCEPT
GO

CREATE TABLE Account
(
	ID INT PRIMARY KEY IDENTITY,
	UserName VARCHAR(30) UNIQUE,
	Password VARCHAR(200) NOT NULL,
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
	NumberPhone VARCHAR(40) DEFAULT '(+84)',
	PRIMARY KEY(IDCustomer)
)
GO

CREATE TABLE Category
(
	IDCategory INT PRIMARY KEY IDENTITY,
	NameCategory NVARCHAR(100) NOT NULL,
)
GO

CREATE TABLE Product
(
	ID INT PRIMARY KEY IDENTITY,
	NameProduct NVARCHAR(200) NOT NULL,
	PriceOld FLOAT,
	NewPrice FLOAT,
	Unit NVARCHAR(20),
	CreateAt DATE DEFAULT GETDATE(),
	Quantity FLOAT NOT NULL,
	Description NVARCHAR(MAX),
	ImgDemo NVARCHAR(1000) NOT NULL,
	Status NVARCHAR(20) DEFAULT N'ON',
	IDCategory INT FOREIGN KEY REFERENCES Category(IDCategory),

)
GO

CREATE TABLE IMAGES
(
	ID INT PRIMARY KEY IDENTITY,
	IdProduct INT FOREIGN KEY REFERENCES Product(ID),
	Img_Url NVARCHAR(1000) NOT NULL, 
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
	InforShipment NVARCHAR(1000),
	OrderDate DATE DEFAULT GETDATE(),
	FeeShipment FLOAT,
	Status NVARCHAR(20) DEFAULT N'Chờ xác nhận',
	ThanhTien FLOAT DEFAULT 0
)
GO
CREATE TABLE OrderDetails
(
	IDOrder INT FOREIGN KEY REFERENCES Orders(IDOrder),
	IDProduct INT FOREIGN KEY REFERENCES Product(ID),
	Quantity FLOAT,
	Price FLOAT,
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

go


-------------------------- TRIGGER -------------------------
--Cập nhật thành tiền của Hóa đơn
--CREATE TRIGGER TRIG_UpdateMoney ON OrderDetails
--AFTER INSERT
--AS
--BEGIN
--	UPDATE Orders
--	SET ThanhTien += (SELECT TotalMoney FROM inserted where inserted.IDOrder = Orders.IDOrder)
--	WHERE  IDOrder = (SELECT inserted.IDOrder FROM inserted)
--END

--GO


CREATE TRIGGER TRIG_UpdateMoney_PhieuNhap ON CHITIETPHIEUNHAP
AFTER INSERT,UPDATE,DELETE
AS
BEGIN
	IF EXISTS (SELECT * FROM inserted)
	BEGIN
		DECLARE @IdPhieuNhap INT = (SELECT MaPhieuNhap from inserted)
		UPDATE BaoCaoNhap
		SET TongTien = (SELECT SUM(TONGTIEN) FROM ChiTietPhieuNhap WHERE MaPhieuNhap = @IdPhieuNhap)
		WHERE MaPhieuNhap = @IdPhieuNhap

		UPDATE BaoCaoNhap
		SET ThanhTien = TongTien - GiamGia	
	END
	ELSE IF EXISTS (SELECT * FROM deleted)
	BEGIN
		UPDATE BaoCaoNhap
		SET TongTien -= (SELECT TONGTIEN FROM deleted)
		WHERE MaPhieuNhap = (SELECT MaPhieuNhap FROM deleted)

		UPDATE BaoCaoNhap
		SET ThanhTien = TongTien - GiamGia 
	END
END
GO
------------------------ INDEX --------------------------------
CREATE INDEX Product_Index ON Product(ID,NameProduct,Status,IDCategory,CreateAt)
CREATE INDEX Orders_Index ON Orders(IDOrder,IDCustomer,OrderDate)
CREATE INDEX Category_Index ON Category(IDCategory)
CREATE INDEX User_Index ON Users(IDCustomer)
CREATE INDEX ThueDuAn_Index ON ThueDuAn(MaDuAn,IDCustomer)

GO

-------------------------   PROCEDURE-----------------------------------
CREATE PROC usp_CancelInvoice
@IDOrder INT , @IDUser INT
AS
BEGIN
	DELETE FROM OrderDetails WHERE IDOrder = @IDOrder
	DELETE FROM Orders WHERE  IDOrder =   @IDOrder AND IDCustomer = @IDUser
END



GO
-----------------------------THÊM DỮ LIỆU --------------------------------
INSERT INTO Category(NameCategory)
	VALUES (N'Sofa'),
			(N'Ghế thư giãn'),
			(N'Bàn ăn'),
			(N'Ghế ăn'),
			(N'Giường ngủ'),
			(N'Bàn làm việc')

GO
INSERT INTO Product (NameProduct,PriceOld,NewPrice,Unit,Quantity,Description,ImgDemo,IDCategory,CreateAt)
VALUES(N'Bàn giám đốc hiện đại 1m6 BTPM01', 5000000,3400000 ,N'Bàn',23 ,N'Kích thước mặt bàn: Dài 1600 – Rộng 800- Cao 750(mm),
Kích thước tủ phụ: Dài 1600 – Rộng 400 -Cao 650 (mm)
Chất liệu: Gỗ công nghiệp MFC dán melamin chống xước
Kiểu dáng: Mặt bàn thẳng, liền tủ phụ.
Độ mới: 100%',N'https://noithatphongphu.com/wp-content/uploads/2020/06/ban-truong-phong-1m6-BTPM01-1.jpg',6 ,GETDATE()),
		(N'Bàn giám đốc hiện đại 2m BGDM-04', 7200000,4200000 ,N'Bàn',6 ,N'Kích thước mặt bàn: Dài 2000 – Rộng 800- Cao 750(mm),
Chất liệu: Gỗ công nghiệp MFC dán melamin chống xước
Kiểu dáng: Mặt bàn thẳng.',N'https://noithatphongphu.com/wp-content/uploads/2021/11/ban-giam-doc-moi-2.jpg',6 ,GETDATE()),
		(N'Bàn giám đốc PU 1m8 BGD808',2400000 , 1950000,N'Bàn',53 ,N'Kích thước: Dài 1800 – Rộng 900- Cao 750(mm)
Chất liệu: Gỗ công nghiệp MDF sơn PU
Kiểu dáng: Mặt bàn lượn cong
Độ mới: 100%',N'https://noithatphongphu.com/wp-content/uploads/2019/10/ban-giam-doc-1m8-BGD808-final.jpg', 6,GETDATE()),
		(N'Bàn Ăn 1m6 Gỗ Cao Su', 1900000, 1680000,N'Cái', 23,N'Không có gì để nói',N'https://noithatvannam.com/wp-content/uploads/2022/05/banghean16-1.jpg',3,GETDATE())

	/*	(N'', , ,N'', ,N'',N'', , ),
		(N'', , ,N'', ,N'',N'', , ),
		(N'', , ,N'', ,N'',N'', , ),

		*/
GO
---Thêm tài khoản
INSERT INTO Account(UserName,Password)
		VALUES('abc','123'),
			('xyz','123'),
			('aloku','123'),
			('hello','123'),
			('phuc','123'),
			('hung','123')
go
--THÊM KHÁCH HÀNG
INSERT INTO Users(IDCustomer,FullName)
			VALUES(5,N'Nguyễn Văn Phúc')
INSERT INTO Users(IDCustomer,FullName)
			VALUES(6,N'Mai Duy Hưng')
INSERT INTO Users(IDCustomer,FullName)
			VALUES(2,N'Không có tên')
GO


--THÊM DỰ ÁN
INSERT INTO ThueDuAn(IDCustomer,AddressCustomer,NgayDuKienHoanThanh,NganSachDuKien,YeuCauThietKe,KieuThietKe,MoTaThem)
	VALUES(5,N'Thanh Hóa','2020-12-1',1231331,N'KHÔNG',N'Nội thất',N'Không mô tả'),
		(5,N'Thanh Hóa','2020-1-21',756332,N'KHÔNG',N'Bàn chủ tịt',N'Không mô tả'),
		(2,N'Hà Nội','1999-12-1',85632,N'KHÔNG',N'Nội thất',N'Không mô tả'),
		(6,N'Việt Nam','2020-12-1',6223423,N'KHÔNG',N'Nội thất',N'Không mô tả')

GO
--THÊM HÓA ĐƠN
INSERT INTO Orders(IDCustomer)
VALUES(5),(6),(2),(5),(5)
GO
INSERT INTO OrderDetails(IDOrder,IDProduct,Quantity,Price,TotalMoney)
VALUES(1,2,3,(SELECT NewPrice FROM Product where ID=3),62322)
INSERT INTO OrderDetails(IDOrder,IDProduct,Quantity,Price,TotalMoney)
VALUES(1,4,3,(SELECT NewPrice*3 FROM Product where ID=4),94345)
INSERT INTO OrderDetails(IDOrder,IDProduct,Quantity,Price,TotalMoney)
VALUES(1,3,3,(SELECT NewPrice*3 FROM Product where ID=4),94345)
INSERT INTO OrderDetails(IDOrder,IDProduct,Quantity,Price,TotalMoney)
VALUES(2,2,3,(SELECT NewPrice*3 FROM Product where ID=2),622432)
INSERT INTO OrderDetails(IDOrder,IDProduct,Quantity,Price,TotalMoney)
VALUES(3,2,3,(SELECT NewPrice*3 FROM Product where ID=2),3223)
INSERT INTO OrderDetails(IDOrder,IDProduct,Quantity,Price,TotalMoney)
VALUES(4,3,4,(SELECT NewPrice*4 FROM Product where ID=3),3223)
INSERT INTO OrderDetails(IDOrder,IDProduct,Quantity,Price,TotalMoney)
VALUES(5,4,12,(SELECT NewPrice*12 FROM Product where ID=4),3223)

select * from Orders

