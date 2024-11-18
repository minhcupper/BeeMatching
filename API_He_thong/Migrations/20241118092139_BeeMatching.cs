using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_He_thong.Migrations
{
    public partial class BeeMatching : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DanhMucKyNang",
                columns: table => new
                {
                    DanhMucKyNangId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDanhMuc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhMucKyNang", x => x.DanhMucKyNangId);
                });

            migrationBuilder.CreateTable(
                name: "districts",
                columns: table => new
                {
                    code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    name_en = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    full_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    full_name_en = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    code_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    administrative_unit_id = table.Column<int>(type: "int", nullable: true),
                    administrative_region_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_districts", x => x.code);
                });

            migrationBuilder.CreateTable(
                name: "NguoiDung",
                columns: table => new
                {
                    nguoi_dung_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ten_dang_nhap = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    mat_khau = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Roles = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiDung", x => x.nguoi_dung_id);
                });

            migrationBuilder.CreateTable(
                name: "provinces",
                columns: table => new
                {
                    code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    name_en = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    full_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    full_name_en = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    code_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    administrative_unit_id = table.Column<int>(type: "int", nullable: true),
                    administrative_region_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_provinces", x => x.code);
                });

            migrationBuilder.CreateTable(
                name: "wards",
                columns: table => new
                {
                    code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    name_en = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    full_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    full_name_en = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    code_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    administrative_unit_id = table.Column<int>(type: "int", nullable: true),
                    administrative_region_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wards", x => x.code);
                });

            migrationBuilder.CreateTable(
                name: "ThongBao",
                columns: table => new
                {
                    ThongBaoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NguoiDungId = table.Column<int>(type: "int", nullable: false),
                    TieuDe = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    NgayGui = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongBao", x => x.ThongBaoId);
                    table.ForeignKey(
                        name: "FK_ThongBao_NguoiDung_NguoiDungId",
                        column: x => x.NguoiDungId,
                        principalTable: "NguoiDung",
                        principalColumn: "nguoi_dung_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoanhNghiep",
                columns: table => new
                {
                    DoanhNghiepId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HinhAnh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NguoiDungId = table.Column<int>(type: "int", nullable: false),
                    TenCongTy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DistrictId = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    WardId = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    ProvinceId = table.Column<string>(type: "nvarchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoanhNghiep", x => x.DoanhNghiepId);
                    table.ForeignKey(
                        name: "FK_DoanhNghiep_districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "districts",
                        principalColumn: "code");
                    table.ForeignKey(
                        name: "FK_DoanhNghiep_NguoiDung_NguoiDungId",
                        column: x => x.NguoiDungId,
                        principalTable: "NguoiDung",
                        principalColumn: "nguoi_dung_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoanhNghiep_provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "provinces",
                        principalColumn: "code");
                    table.ForeignKey(
                        name: "FK_DoanhNghiep_wards_WardId",
                        column: x => x.WardId,
                        principalTable: "wards",
                        principalColumn: "code");
                });

            migrationBuilder.CreateTable(
                name: "NguoiTimViec",
                columns: table => new
                {
                    NguoiTimViecId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HinhAnh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NguoiDungId = table.Column<int>(type: "int", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    NgonNgu = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    KinhNghiem = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    ho_ten = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ngay_sinh = table.Column<DateTime>(type: "datetime2", nullable: true),
                    gioi_tinh = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    so_dien_thoai = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    dia_chi_nha = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    DistrictId = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    WardId = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    ProvinceId = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    trang_thai = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    ngay_tao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HoatDongNgoaiKhoa = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiTimViec", x => x.NguoiTimViecId);
                    table.ForeignKey(
                        name: "FK_NguoiTimViec_districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "districts",
                        principalColumn: "code");
                    table.ForeignKey(
                        name: "FK_NguoiTimViec_NguoiDung_NguoiDungId",
                        column: x => x.NguoiDungId,
                        principalTable: "NguoiDung",
                        principalColumn: "nguoi_dung_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NguoiTimViec_provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "provinces",
                        principalColumn: "code");
                    table.ForeignKey(
                        name: "FK_NguoiTimViec_wards_WardId",
                        column: x => x.WardId,
                        principalTable: "wards",
                        principalColumn: "code");
                });

            migrationBuilder.CreateTable(
                name: "CongViec",
                columns: table => new
                {
                    CongViecId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoanhNghiepId = table.Column<int>(type: "int", nullable: false),
                    TieuDe = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LuongHangThang = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ViTri = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgayDang = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HanNopHoSo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DistrictId = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    WardId = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    ProvinceId = table.Column<string>(type: "nvarchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CongViec", x => x.CongViecId);
                    table.ForeignKey(
                        name: "FK_CongViec_districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "districts",
                        principalColumn: "code");
                    table.ForeignKey(
                        name: "FK_CongViec_DoanhNghiep_DoanhNghiepId",
                        column: x => x.DoanhNghiepId,
                        principalTable: "DoanhNghiep",
                        principalColumn: "DoanhNghiepId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CongViec_provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "provinces",
                        principalColumn: "code");
                    table.ForeignKey(
                        name: "FK_CongViec_wards_WardId",
                        column: x => x.WardId,
                        principalTable: "wards",
                        principalColumn: "code");
                });

            migrationBuilder.CreateTable(
                name: "KinhnghiemNguoiTimViec",
                columns: table => new
                {
                    KinhNghiemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NguoiTimViecId = table.Column<int>(type: "int", nullable: false),
                    TenKinhNghiem = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KinhnghiemNguoiTimViec", x => x.KinhNghiemId);
                    table.ForeignKey(
                        name: "FK_KinhnghiemNguoiTimViec_NguoiTimViec_NguoiTimViecId",
                        column: x => x.NguoiTimViecId,
                        principalTable: "NguoiTimViec",
                        principalColumn: "NguoiTimViecId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KyNangNguoiXinViec",
                columns: table => new
                {
                    KyNangId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenKyNang = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NguoiTimViecId = table.Column<int>(type: "int", nullable: false),
                    DanhMucKyNangId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KyNangNguoiXinViec", x => x.KyNangId);
                    table.ForeignKey(
                        name: "FK_KyNangNguoiXinViec_DanhMucKyNang_DanhMucKyNangId",
                        column: x => x.DanhMucKyNangId,
                        principalTable: "DanhMucKyNang",
                        principalColumn: "DanhMucKyNangId");
                    table.ForeignKey(
                        name: "FK_KyNangNguoiXinViec_NguoiTimViec_NguoiTimViecId",
                        column: x => x.NguoiTimViecId,
                        principalTable: "NguoiTimViec",
                        principalColumn: "NguoiTimViecId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KinhNghiemCongViec",
                columns: table => new
                {
                    KinhNghiemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CongViecId = table.Column<int>(type: "int", nullable: false),
                    TenKinhNghiem = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KinhNghiemCongViec", x => x.KinhNghiemId);
                    table.ForeignKey(
                        name: "FK_KinhNghiemCongViec_CongViec_CongViecId",
                        column: x => x.CongViecId,
                        principalTable: "CongViec",
                        principalColumn: "CongViecId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KyNangCongViec",
                columns: table => new
                {
                    KyNangId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CongViecId = table.Column<int>(type: "int", nullable: false),
                    TenKyNang = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DanhMucKyNangId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KyNangCongViec", x => x.KyNangId);
                    table.ForeignKey(
                        name: "FK_KyNangCongViec_CongViec_CongViecId",
                        column: x => x.CongViecId,
                        principalTable: "CongViec",
                        principalColumn: "CongViecId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KyNangCongViec_DanhMucKyNang_DanhMucKyNangId",
                        column: x => x.DanhMucKyNangId,
                        principalTable: "DanhMucKyNang",
                        principalColumn: "DanhMucKyNangId");
                });

            migrationBuilder.CreateTable(
                name: "UngTuyen",
                columns: table => new
                {
                    UngTuyenId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CongViecId = table.Column<int>(type: "int", nullable: false),
                    NguoiTimViecId = table.Column<int>(type: "int", nullable: false),
                    NgayUngTuyen = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeXuat = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TrangThai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ChapNhanCongViec = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UngTuyen", x => x.UngTuyenId);
                    table.ForeignKey(
                        name: "FK_UngTuyen_CongViec_CongViecId",
                        column: x => x.CongViecId,
                        principalTable: "CongViec",
                        principalColumn: "CongViecId");
                    table.ForeignKey(
                        name: "FK_UngTuyen_NguoiTimViec_NguoiTimViecId",
                        column: x => x.NguoiTimViecId,
                        principalTable: "NguoiTimViec",
                        principalColumn: "NguoiTimViecId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DanhGia",
                columns: table => new
                {
                    DanhGiaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UngTuyenId = table.Column<int>(type: "int", nullable: false),
                    NoiDungDanhGia = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DiemDanhGia = table.Column<int>(type: "int", nullable: false),
                    NgayDanhGia = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhGia", x => x.DanhGiaId);
                    table.ForeignKey(
                        name: "FK_DanhGia_UngTuyen_UngTuyenId",
                        column: x => x.UngTuyenId,
                        principalTable: "UngTuyen",
                        principalColumn: "UngTuyenId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CongViec_DistrictId",
                table: "CongViec",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_CongViec_DoanhNghiepId",
                table: "CongViec",
                column: "DoanhNghiepId");

            migrationBuilder.CreateIndex(
                name: "IX_CongViec_ProvinceId",
                table: "CongViec",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_CongViec_WardId",
                table: "CongViec",
                column: "WardId");

            migrationBuilder.CreateIndex(
                name: "IX_DanhGia_UngTuyenId",
                table: "DanhGia",
                column: "UngTuyenId");

            migrationBuilder.CreateIndex(
                name: "IX_DoanhNghiep_DistrictId",
                table: "DoanhNghiep",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_DoanhNghiep_NguoiDungId",
                table: "DoanhNghiep",
                column: "NguoiDungId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoanhNghiep_ProvinceId",
                table: "DoanhNghiep",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_DoanhNghiep_WardId",
                table: "DoanhNghiep",
                column: "WardId");

            migrationBuilder.CreateIndex(
                name: "IX_KinhNghiemCongViec_CongViecId",
                table: "KinhNghiemCongViec",
                column: "CongViecId");

            migrationBuilder.CreateIndex(
                name: "IX_KinhnghiemNguoiTimViec_NguoiTimViecId",
                table: "KinhnghiemNguoiTimViec",
                column: "NguoiTimViecId");

            migrationBuilder.CreateIndex(
                name: "IX_KyNangCongViec_CongViecId",
                table: "KyNangCongViec",
                column: "CongViecId");

            migrationBuilder.CreateIndex(
                name: "IX_KyNangCongViec_DanhMucKyNangId",
                table: "KyNangCongViec",
                column: "DanhMucKyNangId");

            migrationBuilder.CreateIndex(
                name: "IX_KyNangNguoiXinViec_DanhMucKyNangId",
                table: "KyNangNguoiXinViec",
                column: "DanhMucKyNangId");

            migrationBuilder.CreateIndex(
                name: "IX_KyNangNguoiXinViec_NguoiTimViecId",
                table: "KyNangNguoiXinViec",
                column: "NguoiTimViecId");

            migrationBuilder.CreateIndex(
                name: "IX_NguoiTimViec_DistrictId",
                table: "NguoiTimViec",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_NguoiTimViec_NguoiDungId",
                table: "NguoiTimViec",
                column: "NguoiDungId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NguoiTimViec_ProvinceId",
                table: "NguoiTimViec",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_NguoiTimViec_WardId",
                table: "NguoiTimViec",
                column: "WardId");

            migrationBuilder.CreateIndex(
                name: "IX_ThongBao_NguoiDungId",
                table: "ThongBao",
                column: "NguoiDungId");

            migrationBuilder.CreateIndex(
                name: "IX_UngTuyen_CongViecId",
                table: "UngTuyen",
                column: "CongViecId");

            migrationBuilder.CreateIndex(
                name: "IX_UngTuyen_NguoiTimViecId",
                table: "UngTuyen",
                column: "NguoiTimViecId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DanhGia");

            migrationBuilder.DropTable(
                name: "KinhNghiemCongViec");

            migrationBuilder.DropTable(
                name: "KinhnghiemNguoiTimViec");

            migrationBuilder.DropTable(
                name: "KyNangCongViec");

            migrationBuilder.DropTable(
                name: "KyNangNguoiXinViec");

            migrationBuilder.DropTable(
                name: "ThongBao");

            migrationBuilder.DropTable(
                name: "UngTuyen");

            migrationBuilder.DropTable(
                name: "DanhMucKyNang");

            migrationBuilder.DropTable(
                name: "CongViec");

            migrationBuilder.DropTable(
                name: "NguoiTimViec");

            migrationBuilder.DropTable(
                name: "DoanhNghiep");

            migrationBuilder.DropTable(
                name: "districts");

            migrationBuilder.DropTable(
                name: "NguoiDung");

            migrationBuilder.DropTable(
                name: "provinces");

            migrationBuilder.DropTable(
                name: "wards");
        }
    }
}
