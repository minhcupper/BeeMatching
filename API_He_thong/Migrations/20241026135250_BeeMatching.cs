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
                    danh_muc_ky_nang_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ten_danh_muc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    mo_ta = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhMucKyNang", x => x.danh_muc_ky_nang_id);
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
                    province_code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    administrative_unit_Id = table.Column<int>(type: "int", nullable: true)
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
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    mat_khau = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ho_ten = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ngay_sinh = table.Column<DateTime>(type: "datetime2", nullable: true),
                    gioi_tinh = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    so_dien_thoai = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    dia_chi = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    loai_nguoi_dung = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    trang_thai = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    ngay_tao = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                name: "TaiKhoanNguoiDung",
                columns: table => new
                {
                    tai_khoan_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ten_dang_nhap = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    mat_khau = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaiKhoanNguoiDung", x => x.tai_khoan_id);
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
                    district_code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    administrative_unit_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wards", x => x.code);
                });

            migrationBuilder.CreateTable(
                name: "DoanhNghiep",
                columns: table => new
                {
                    doanh_nghiep_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nguoi_dung_id = table.Column<int>(type: "int", nullable: false),
                    ten_cong_ty = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    mo_ta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dia_chi = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoanhNghiep", x => x.doanh_nghiep_id);
                    table.ForeignKey(
                        name: "FK_DoanhNghiep_NguoiDung_nguoi_dung_id",
                        column: x => x.nguoi_dung_id,
                        principalTable: "NguoiDung",
                        principalColumn: "nguoi_dung_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NguoiTimViec",
                columns: table => new
                {
                    nguoi_tim_viec_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nguoi_dung_id = table.Column<int>(type: "int", nullable: false),
                    mo_ta = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ngon_ngu = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    kinh_nghiem = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    hoat_dong_ngoai_khoa = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiTimViec", x => x.nguoi_tim_viec_id);
                    table.ForeignKey(
                        name: "FK_NguoiTimViec_NguoiDung_nguoi_dung_id",
                        column: x => x.nguoi_dung_id,
                        principalTable: "NguoiDung",
                        principalColumn: "nguoi_dung_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThongBao",
                columns: table => new
                {
                    thong_bao_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nguoi_dung_id = table.Column<int>(type: "int", nullable: false),
                    tieu_de = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    noi_dung = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    trang_thai = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    ngay_gui = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongBao", x => x.thong_bao_id);
                    table.ForeignKey(
                        name: "FK_ThongBao_NguoiDung_nguoi_dung_id",
                        column: x => x.nguoi_dung_id,
                        principalTable: "NguoiDung",
                        principalColumn: "nguoi_dung_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CongViec",
                columns: table => new
                {
                    cong_viec_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    doanh_nghiep_id = table.Column<int>(type: "int", nullable: false),
                    tieu_de = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    mo_ta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ky_nang_yeu_cau = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    luong_hang_thang = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    vi_tri = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    dia_diem_lam_viec = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    trang_thai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ngay_dang = table.Column<DateTime>(type: "datetime2", nullable: false),
                    han_nop_ho_so = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CongViec", x => x.cong_viec_id);
                    table.ForeignKey(
                        name: "FK_CongViec_DoanhNghiep_doanh_nghiep_id",
                        column: x => x.doanh_nghiep_id,
                        principalTable: "DoanhNghiep",
                        principalColumn: "doanh_nghiep_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KyNang",
                columns: table => new
                {
                    ky_nang_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nguoi_tim_viec_id = table.Column<int>(type: "int", nullable: false),
                    danh_muc_ky_nang_id = table.Column<int>(type: "int", nullable: false),
                    ten_ky_nang = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    mo_ta = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KyNang", x => x.ky_nang_id);
                    table.ForeignKey(
                        name: "FK_KyNang_DanhMucKyNang_danh_muc_ky_nang_id",
                        column: x => x.danh_muc_ky_nang_id,
                        principalTable: "DanhMucKyNang",
                        principalColumn: "danh_muc_ky_nang_id");
                    table.ForeignKey(
                        name: "FK_KyNang_NguoiTimViec_nguoi_tim_viec_id",
                        column: x => x.nguoi_tim_viec_id,
                        principalTable: "NguoiTimViec",
                        principalColumn: "nguoi_tim_viec_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UngTuyen",
                columns: table => new
                {
                    ung_tuyen_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cong_viec_id = table.Column<int>(type: "int", nullable: false),
                    nguoi_tim_viec_id = table.Column<int>(type: "int", nullable: false),
                    ngay_ung_tuyen = table.Column<DateTime>(type: "datetime2", nullable: false),
                    de_xuat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    trang_thai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    chap_nhan_cong_viec = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UngTuyen", x => x.ung_tuyen_id);
                    table.ForeignKey(
                        name: "FK_UngTuyen_CongViec_cong_viec_id",
                        column: x => x.cong_viec_id,
                        principalTable: "CongViec",
                        principalColumn: "cong_viec_id");
                    table.ForeignKey(
                        name: "FK_UngTuyen_NguoiTimViec_nguoi_tim_viec_id",
                        column: x => x.nguoi_tim_viec_id,
                        principalTable: "NguoiTimViec",
                        principalColumn: "nguoi_tim_viec_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DanhGia",
                columns: table => new
                {
                    danh_gia_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ung_tuyen_id = table.Column<int>(type: "int", nullable: false),
                    noi_dung_danh_gia = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    diem_danh_gia = table.Column<int>(type: "int", nullable: false),
                    ngay_danh_gia = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhGia", x => x.danh_gia_id);
                    table.ForeignKey(
                        name: "FK_DanhGia_UngTuyen_ung_tuyen_id",
                        column: x => x.ung_tuyen_id,
                        principalTable: "UngTuyen",
                        principalColumn: "ung_tuyen_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CongViec_doanh_nghiep_id",
                table: "CongViec",
                column: "doanh_nghiep_id");

            migrationBuilder.CreateIndex(
                name: "IX_DanhGia_ung_tuyen_id",
                table: "DanhGia",
                column: "ung_tuyen_id");

            migrationBuilder.CreateIndex(
                name: "IX_DoanhNghiep_nguoi_dung_id",
                table: "DoanhNghiep",
                column: "nguoi_dung_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KyNang_danh_muc_ky_nang_id",
                table: "KyNang",
                column: "danh_muc_ky_nang_id");

            migrationBuilder.CreateIndex(
                name: "IX_KyNang_nguoi_tim_viec_id",
                table: "KyNang",
                column: "nguoi_tim_viec_id");

            migrationBuilder.CreateIndex(
                name: "IX_NguoiTimViec_nguoi_dung_id",
                table: "NguoiTimViec",
                column: "nguoi_dung_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ThongBao_nguoi_dung_id",
                table: "ThongBao",
                column: "nguoi_dung_id");

            migrationBuilder.CreateIndex(
                name: "IX_UngTuyen_cong_viec_id",
                table: "UngTuyen",
                column: "cong_viec_id");

            migrationBuilder.CreateIndex(
                name: "IX_UngTuyen_nguoi_tim_viec_id",
                table: "UngTuyen",
                column: "nguoi_tim_viec_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DanhGia");

            migrationBuilder.DropTable(
                name: "districts");

            migrationBuilder.DropTable(
                name: "KyNang");

            migrationBuilder.DropTable(
                name: "provinces");

            migrationBuilder.DropTable(
                name: "TaiKhoanNguoiDung");

            migrationBuilder.DropTable(
                name: "ThongBao");

            migrationBuilder.DropTable(
                name: "wards");

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
                name: "NguoiDung");
        }
    }
}
