-- Tạo database
CREATE DATABASE CuaHangSuaChua;
GO

USE CuaHangSuaChua;
GO

-- Tạo bảng Khách hàng
CREATE TABLE KhachHang (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Ten NVARCHAR(100),
    SoDienThoai NVARCHAR(15),
    DiaChi NVARCHAR(255),
    Email NVARCHAR(100),
    DiemTichLuy FLOAT DEFAULT 0
);
GO

-- Tạo bảng Thiết bị
CREATE TABLE ThietBi (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    TenThietBi NVARCHAR(100),
    LoaiThietBi NVARCHAR(100),
    MoTaLoi NVARCHAR(MAX),
    TinhTrang NVARCHAR(50),
    NgayBaoHanh DATE,
    KhachHangID INT NULL,  -- Cột khóa ngoại NULLABLE
    FOREIGN KEY (KhachHangID) REFERENCES KhachHang(ID) ON DELETE SET NULL
);
GO

-- Tạo bảng Nhân viên
CREATE TABLE NhanVien (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Ten NVARCHAR(100),
    SoDienThoai NVARCHAR(15),
    VaiTro NVARCHAR(50),
    NangSuatLamViec FLOAT DEFAULT 0
);
GO

-- Tạo bảng Linh kiện
CREATE TABLE LinhKien (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    TenLinhKien NVARCHAR(100),
    SoLuongTon INT,
    DonGia FLOAT
);
GO

-- Tạo bảng Hóa đơn với ON DELETE SET NULL
CREATE TABLE HoaDon (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    SoHoaDon NVARCHAR(100),
    NgayTao DATE,
    TongChiPhi FLOAT,
    TrangThaiThanhToan NVARCHAR(50),
    ThietBiID INT NULL,  -- Cột khóa ngoại NULLABLE
    KhachHangID INT NULL,  -- Cột khóa ngoại NULLABLE
    FOREIGN KEY (ThietBiID) REFERENCES ThietBi(ID) ON DELETE SET NULL,
    FOREIGN KEY (KhachHangID) REFERENCES KhachHang(ID) ON DELETE SET NULL
);
GO

-- Tạo bảng Lịch sử sửa chữa
CREATE TABLE LichSuSuaChua (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    NgaySua DATE,
    MoTaSuaChua NVARCHAR(MAX),
    TinhTrangThietBi NVARCHAR(50),
    NhanVienID INT NULL,  -- Cột khóa ngoại NULLABLE
    ThietBiID INT NULL,  -- Cột khóa ngoại NULLABLE
    FOREIGN KEY (NhanVienID) REFERENCES NhanVien(ID) ON DELETE SET NULL,
    FOREIGN KEY (ThietBiID) REFERENCES ThietBi(ID) ON DELETE SET NULL
);
GO

-- Tạo bảng Thông báo
CREATE TABLE ThongBao (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    NoiDung NVARCHAR(MAX),
    NgayGui DATE,
    KhachHangID INT NULL,  -- Cột khóa ngoại NULLABLE
    FOREIGN KEY (KhachHangID) REFERENCES KhachHang(ID) ON DELETE SET NULL
);
GO

-- Tạo bảng Báo cáo
CREATE TABLE BaoCao (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    LoaiBaoCao NVARCHAR(50),
    NoiDung NVARCHAR(MAX),
    NgayTao DATE
);
GO

-- Tạo bảng Quản lý Linh kiện được sử dụng cho thiết bị (không dùng SET NULL)
CREATE TABLE LinhKien_ThietBi (
    LinhKienID INT,
    ThietBiID INT,
    SoLuong INT,
    PRIMARY KEY (LinhKienID, ThietBiID),
    FOREIGN KEY (LinhKienID) REFERENCES LinhKien(ID) ON DELETE NO ACTION,  -- Sử dụng NO ACTION để tránh lỗi
    FOREIGN KEY (ThietBiID) REFERENCES ThietBi(ID) ON DELETE NO ACTION  -- Sử dụng NO ACTION
);
GO
