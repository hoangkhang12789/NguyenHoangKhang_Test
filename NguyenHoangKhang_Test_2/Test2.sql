--Tạo database và các table
CREATE DATABASE NguyenHoangKhang_Test_2;

Go

USE NguyenHoangKhang_Test_2;

Go

CREATE TABLE HocVien (
    MaHV INT PRIMARY KEY IDENTITY(1, 1),
    TenHV VARCHAR(255),
    Lop VARCHAR(3)
);

Go

CREATE TABLE MonHoc (
    MaMH INT PRIMARY KEY IDENTITY(1, 1),
    TenMH VARCHAR(255)
);

Go

CREATE TABLE BangDiem (
    MaHP INT PRIMARY KEY IDENTITY(1, 1),
    MaHV INT,
    MaMH INT,
    Diem FLOAT,
    HeSo INT,
    NamHoc INT,
    FOREIGN KEY (MaHV) REFERENCES HocVien(MaHV),
    FOREIGN KEY (MaMH) REFERENCES MonHoc(MaMH)
);

--Tạo dữ liệu mẫu

INSERT INTO HocVien (TenHV, Lop)
VALUES
    ('Student 1', '7A1'),
    ('Student 2', '7A1'),
    ('Student 3', '7A2'),
    ('Student 4', '7A2'),
    ('Student 5', '7B1');

INSERT INTO MonHoc (TenMH)
VALUES
    ('Toan'),
    ('Ngu Van'),
    ('Ly'),
    ('Hoa'),
    ('Sinh');

INSERT INTO BangDiem (MaHV, MaMH, Diem, HeSo, NamHoc)
VALUES
    (1, 1, 4.0, 2, 2023),
    (2, 1, 7.0, 2, 2023),
    (3, 1, 6.5, 2, 2023),
    (1, 2, 3.0, 2, 2023),
    (2, 2, 7.5, 2, 2023),
    (3, 2, 8.0, 2, 2023),
    (1, 3, 7.0, 2, 2023),
    (2, 3, 8.0, 2, 2023),
    (3, 3, 6.5, 2, 2023),
    (1, 4, 5.0, 2, 2023),
    (2, 4, 9.0, 2, 2023),
    (3, 4, 8.0, 2, 2023),
    (1, 5, 3.5, 2, 2023),
    (2, 5, 7.0, 2, 2023),
    (3, 5, 7.5, 2, 2023);

 --Câu b lấy danh sách Danh sách các học viên phải học lại
 SELECT
    HV.MaHV,
    HV.TenHV,
    MH.TenMH,
    SUM(BD.Diem * BD.HeSo) / SUM(BD.HeSo) AS DiemTBMon
FROM HocVien HV
JOIN BangDiem BD ON HV.MaHV = BD.MaHV
JOIN MonHoc MH ON BD.MaMH = MH.MaMH
WHERE BD.NamHoc = 2023
GROUP BY HV.MaHV, HV.TenHV, MH.TenMH
HAVING SUM(BD.Diem * BD.HeSo) / SUM(BD.HeSo) < 5.0;

 --Câu c Tìm ra người có điểm trung bình năm học (DiemTBNH) 2023 cao nhất của lớp 7A1
 SELECT TOP 1
    HV.MaHV,
    HV.TenHV,
    SUM(BD.Diem * BD.HeSo) / SUM(BD.HeSo) AS DiemTBNH,
    BD.NamHoc
FROM HocVien HV
JOIN BangDiem BD ON HV.MaHV = BD.MaHV
WHERE HV.Lop = '7A1' AND BD.NamHoc = 2023
GROUP BY HV.MaHV, HV.TenHV, BD.NamHoc
ORDER BY DiemTBNH DESC;