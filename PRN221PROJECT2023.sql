USE [master]
GO

/*******************************************************************************
   Drop database if it exists
********************************************************************************/
IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'PRN221PROJECT')
BEGIN
	ALTER DATABASE [PRN221PROJECT] SET OFFLINE WITH ROLLBACK IMMEDIATE;
	ALTER DATABASE [PRN221PROJECT] SET ONLINE;
	DROP DATABASE [PRN221PROJECT];
END

GO

CREATE DATABASE [PRN221PROJECT]

GO

USE [PRN221PROJECT]
GO
create table tblKhachHang(
MakH varchar(10) primary key ,
TenKH nvarchar(50),
GT bit,
Diachi nvarchar(50),
NgaySinh smalldatetime,
active bit 
)

GO
create table tblHoadon(
MaHD numeric(18,0) primary key identity(1,1),
MaKH varchar(10) foreign key references tblKhachHang(MaKH) ,
NgayHD smalldatetime
)
GO

create table tblMatHang(
MaHang varchar(10) primary key,
TenHang nvarchar(50),
DVT nvarchar(50),
Gia real,
active bit 
)
GO

create table tblChiTietHD(
MaChiTietHD numeric(18,0) primary key identity(1,1),
MaHD numeric(18,0) foreign key references tblHoadon(MaHD),
MaHang varchar(10) foreign key references tblMatHang(MaHang),
Soluong int
)
GO

create table tblUser(
Username varchar(50),
Pass int
)
GO

insert into tblUser values('hieu', 123)
insert into tblUser values('admin', 123)
insert into tblUser values('tadinh_tien', 12345678)

Go
insert into tblKhachHang values('1', N'Chu Đức Tâm', 1, N'Sài Gòn', '12/12/1994',1)
insert into tblKhachHang values('2', N'Văn Mai Hương', 0, 'HN', '12/12/1985',1)
insert into tblKhachHang values('3', N'Hoang Thanh Chung', 1, 'hn', '12/12/1994',1)
insert into tblKhachHang values('4', N'Nguyễn Hà', 1, 'ND', '01/01/1990',1)
insert into tblKhachHang values('KH01', N'Hoàng Thanh Trang', 0, N'Gia Lâm, Hà Nội', '12/21/1969',1)
insert into tblKhachHang values('KH02', N'Nguyễn Vĩ Nhung', 1, N'Vĩnh Long', '07/22/1999',1)

GO
insert into tblMatHang values('K01', N'Bàn phím', N'Chiếc', 200000,1)
insert into tblMatHang values('K02', N'Ban phim ao', N'Chiếc', 120000,1)
insert into tblMatHang values('M01', N'Chuột quang', N'Con', 120000,1)
insert into tblMatHang values('M02', N'Man hinh hong', N'Cai', 2000000,1)
insert into tblMatHang values('R01', N'Bàn phím T1', N'Thanh', 200000,1)

Go
insert into tblHoadon values('1', '10/9/2022')
insert into tblHoadon values('1', '10/10/2022')
insert into tblHoadon values('2', '10/9/2022')
insert into tblHoadon values('2', '10/9/2022')

Go
insert into tblChiTietHD values(1, 'K01', 5)
insert into tblChiTietHD values(1, 'K02', 3)
insert into tblChiTietHD values(1, 'R01', 1)

GO

