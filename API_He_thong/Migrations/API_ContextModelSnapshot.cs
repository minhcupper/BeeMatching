﻿// <auto-generated />
using System;
using API_He_thong.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API_He_thong.Migrations
{
    [DbContext(typeof(API_Context))]
    partial class API_ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.35")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("API_He_thong.Models.CongViec", b =>
                {
                    b.Property<int>("CongViecId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CongViecId"), 1L, 1);

                    b.Property<string>("DistrictId")
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("DoanhNghiepId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("HanNopHoSo")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("LuongHangThang")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("MoTa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("NgayDang")
                        .HasColumnType("datetime2");

                    b.Property<string>("ProvinceId")
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("TieuDe")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("TrangThai")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ViTri")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("WardId")
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("CongViecId");

                    b.HasIndex("DistrictId");

                    b.HasIndex("DoanhNghiepId");

                    b.HasIndex("ProvinceId");

                    b.HasIndex("WardId");

                    b.ToTable("CongViec");
                });

            modelBuilder.Entity("API_He_thong.Models.DanhGia", b =>
                {
                    b.Property<int>("DanhGiaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DanhGiaId"), 1L, 1);

                    b.Property<int>("DiemDanhGia")
                        .HasColumnType("int");

                    b.Property<DateTime>("NgayDanhGia")
                        .HasColumnType("datetime2");

                    b.Property<string>("NoiDungDanhGia")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("UngTuyenId")
                        .HasColumnType("int");

                    b.HasKey("DanhGiaId");

                    b.HasIndex("UngTuyenId");

                    b.ToTable("DanhGia");
                });

            modelBuilder.Entity("API_He_thong.Models.DanhMucKyNang", b =>
                {
                    b.Property<int>("DanhMucKyNangId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DanhMucKyNangId"), 1L, 1);

                    b.Property<string>("MoTa")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("TenDanhMuc")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("DanhMucKyNangId");

                    b.ToTable("danhMucKyNang");
                });

            modelBuilder.Entity("API_He_thong.Models.districts", b =>
                {
                    b.Property<string>("code")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int?>("administrative_unit_Id")
                        .HasColumnType("int");

                    b.Property<string>("code_name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("full_name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("full_name_en")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("name_en")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("province_code")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("code");

                    b.ToTable("districts");
                });

            modelBuilder.Entity("API_He_thong.Models.DoanhNghiep", b =>
                {
                    b.Property<int>("DoanhNghiepId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DoanhNghiepId"), 1L, 1);

                    b.Property<string>("DiaChi")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("DistrictId")
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("HinhAnh")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MoTa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NguoiDungId")
                        .HasColumnType("int");

                    b.Property<string>("ProvinceId")
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("TenCongTy")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("WardId")
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("DoanhNghiepId");

                    b.HasIndex("DistrictId");

                    b.HasIndex("NguoiDungId")
                        .IsUnique();

                    b.HasIndex("ProvinceId");

                    b.HasIndex("WardId");

                    b.ToTable("DoanhNghiep");
                });

            modelBuilder.Entity("API_He_thong.Models.KyNangCongViec", b =>
                {
                    b.Property<int>("KyNangId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CongViecId")
                        .HasColumnType("int");

                    b.Property<int>("DanhMucKyNangId")
                        .HasColumnType("int");

                    b.Property<string>("MoTa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenKyNang")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("KyNangId");

                    b.HasIndex("CongViecId");

                    b.ToTable("KyNangCongViec");
                });

            modelBuilder.Entity("API_He_thong.Models.KyNangNguoiXinViec", b =>
                {
                    b.Property<int>("KyNangId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("KyNangId"), 1L, 1);

                    b.Property<int>("DanhMucKyNangId")
                        .HasColumnType("int");

                    b.Property<string>("MoTa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NguoiTimViecId")
                        .HasColumnType("int");

                    b.Property<string>("TenKyNang")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("KyNangId");

                    b.HasIndex("DanhMucKyNangId");

                    b.HasIndex("NguoiTimViecId");

                    b.ToTable("KyNangNguoiXinViec");
                });

            modelBuilder.Entity("API_He_thong.Models.NguoiDung", b =>
                {
                    b.Property<int>("nguoi_dung_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("nguoi_dung_id"), 1L, 1);

                    b.Property<string>("DistrictId")
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("PhanQuyenId")
                        .HasColumnType("int");

                    b.Property<string>("ProvinceId")
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("WardId")
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("dia_chi_nha")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("gioi_tinh")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("hinh_anh")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ho_ten")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("mat_khau")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTime?>("ngay_sinh")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ngay_tao")
                        .HasColumnType("datetime2");

                    b.Property<string>("so_dien_thoai")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("ten_dang_nhap")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("trang_thai")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("nguoi_dung_id");

                    b.HasIndex("DistrictId");

                    b.HasIndex("PhanQuyenId");

                    b.HasIndex("ProvinceId");

                    b.HasIndex("WardId");

                    b.ToTable("NguoiDung");
                });

            modelBuilder.Entity("API_He_thong.Models.NguoiTimViec", b =>
                {
                    b.Property<int>("NguoiTimViecId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NguoiTimViecId"), 1L, 1);

                    b.Property<string>("HinhAnh")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HoatDongNgoaiKhoa")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("KinhNghiem")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("MoTa")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("NgonNgu")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("NguoiDungId")
                        .HasColumnType("int");

                    b.HasKey("NguoiTimViecId");

                    b.HasIndex("NguoiDungId")
                        .IsUnique();

                    b.ToTable("NguoiTimViec");
                });

            modelBuilder.Entity("API_He_thong.Models.PhanQuyen", b =>
                {
                    b.Property<int>("PhanQuyenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PhanQuyenId"), 1L, 1);

                    b.Property<string>("LoaiNguoiDung")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("PhanQuyenId");

                    b.ToTable("PhanQuyen");
                });

            modelBuilder.Entity("API_He_thong.Models.provinces", b =>
                {
                    b.Property<string>("code")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int?>("administrative_region_Id")
                        .HasColumnType("int");

                    b.Property<int?>("administrative_unit_id")
                        .HasColumnType("int");

                    b.Property<string>("code_name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("full_name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("full_name_en")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("name_en")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("code");

                    b.ToTable("provinces");
                });


            modelBuilder.Entity("API_He_thong.Models.TaiKhoanNguoiDung", b =>
                {
                    b.Property<int>("tai_khoan_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("tai_khoan_id"), 1L, 1);

                    b.Property<string>("Roles")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("mat_khau")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ten_dang_nhap")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("tai_khoan_id");

                    b.ToTable("TaiKhoanNguoiDung");
                });


            modelBuilder.Entity("API_He_thong.Models.ThongBao", b =>
                {
                    b.Property<int>("ThongBaoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ThongBaoId"), 1L, 1);

                    b.Property<DateTime>("NgayGui")
                        .HasColumnType("datetime2");

                    b.Property<int>("NguoiDungId")
                        .HasColumnType("int");

                    b.Property<string>("NoiDung")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("TieuDe")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("TrangThai")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("ThongBaoId");

                    b.HasIndex("NguoiDungId");

                    b.ToTable("ThongBao");
                });

            modelBuilder.Entity("API_He_thong.Models.UngTuyen", b =>
                {
                    b.Property<int>("UngTuyenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UngTuyenId"), 1L, 1);

                    b.Property<bool>("ChapNhanCongViec")
                        .HasColumnType("bit");

                    b.Property<int>("CongViecId")
                        .HasColumnType("int");

                    b.Property<string>("DeXuat")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("NgayUngTuyen")
                        .HasColumnType("datetime2");

                    b.Property<int>("NguoiTimViecId")
                        .HasColumnType("int");

                    b.Property<string>("TrangThai")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("UngTuyenId");

                    b.HasIndex("CongViecId");

                    b.HasIndex("NguoiTimViecId");

                    b.ToTable("UngTuyen");
                });

            modelBuilder.Entity("API_He_thong.Models.wards", b =>
                {
                    b.Property<string>("code")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int?>("administrative_unit_id")
                        .HasColumnType("int");

                    b.Property<string>("code_name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("district_code")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("full_name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("full_name_en")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("name_en")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("code");

                    b.ToTable("wards");
                });

            modelBuilder.Entity("API_He_thong.Models.CongViec", b =>
                {
                    b.HasOne("API_He_thong.Models.districts", "Districts")
                        .WithMany("CongViecs")
                        .HasForeignKey("DistrictId");

                    b.HasOne("API_He_thong.Models.DoanhNghiep", "DoanhNghiep")
                        .WithMany("CongViecs")
                        .HasForeignKey("DoanhNghiepId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API_He_thong.Models.provinces", "Provinces")
                        .WithMany("CongViecs")
                        .HasForeignKey("ProvinceId");

                    b.HasOne("API_He_thong.Models.wards", "Wards")
                        .WithMany("CongViecs")
                        .HasForeignKey("WardId");

                    b.Navigation("Districts");

                    b.Navigation("DoanhNghiep");

                    b.Navigation("Provinces");

                    b.Navigation("Wards");
                });

            modelBuilder.Entity("API_He_thong.Models.DanhGia", b =>
                {
                    b.HasOne("API_He_thong.Models.UngTuyen", "UngTuyen")
                        .WithMany("DanhGias")
                        .HasForeignKey("UngTuyenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UngTuyen");
                });

            modelBuilder.Entity("API_He_thong.Models.DoanhNghiep", b =>
                {
                    b.HasOne("API_He_thong.Models.districts", "Districts")
                        .WithMany("doanhNghieps")
                        .HasForeignKey("DistrictId");

                    b.HasOne("API_He_thong.Models.NguoiDung", "NguoiDung")
                        .WithOne("DoanhNghiep")
                        .HasForeignKey("API_He_thong.Models.DoanhNghiep", "NguoiDungId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API_He_thong.Models.provinces", "Provinces")
                        .WithMany("doanhNghieps")
                        .HasForeignKey("ProvinceId");

                    b.HasOne("API_He_thong.Models.wards", "Wards")
                        .WithMany("doanhNghieps")
                        .HasForeignKey("WardId");

                    b.Navigation("Districts");

                    b.Navigation("NguoiDung");

                    b.Navigation("Provinces");

                    b.Navigation("Wards");
                });

            modelBuilder.Entity("API_He_thong.Models.KyNangCongViec", b =>
                {
                    b.HasOne("API_He_thong.Models.CongViec", "CongViec")
                        .WithMany("KyNangCongViecs")
                        .HasForeignKey("CongViecId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API_He_thong.Models.DanhMucKyNang", "DanhMucKyNang")
                        .WithMany("KyNangCongViecs")
                        .HasForeignKey("KyNangId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("CongViec");

                    b.Navigation("DanhMucKyNang");
                });

            modelBuilder.Entity("API_He_thong.Models.KyNangNguoiXinViec", b =>
                {
                    b.HasOne("API_He_thong.Models.DanhMucKyNang", "DanhMucKyNang")
                        .WithMany("KyNangNguoiXinViecs")
                        .HasForeignKey("DanhMucKyNangId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("API_He_thong.Models.NguoiTimViec", "NguoiTimViec")
                        .WithMany("KyNangNguoiXinViecs")
                        .HasForeignKey("NguoiTimViecId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("DanhMucKyNang");

                    b.Navigation("NguoiTimViec");
                });

            modelBuilder.Entity("API_He_thong.Models.NguoiDung", b =>
                {
                    b.HasOne("API_He_thong.Models.districts", "Districts")
                        .WithMany("NguoiDungs")
                        .HasForeignKey("DistrictId");

                    b.HasOne("API_He_thong.Models.PhanQuyen", "PhanQuyen")
                        .WithMany("NguoiDungs")
                        .HasForeignKey("PhanQuyenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API_He_thong.Models.provinces", "Provinces")
                        .WithMany("NguoiDungs")
                        .HasForeignKey("ProvinceId");

                    b.HasOne("API_He_thong.Models.wards", "Wards")
                        .WithMany("NguoiDungs")
                        .HasForeignKey("WardId");

                    b.Navigation("Districts");

                    b.Navigation("PhanQuyen");

                    b.Navigation("Provinces");

                    b.Navigation("Wards");
                });

            modelBuilder.Entity("API_He_thong.Models.NguoiTimViec", b =>
                {
                    b.HasOne("API_He_thong.Models.NguoiDung", "NguoiDung")
                        .WithOne("NguoiTimViec")
                        .HasForeignKey("API_He_thong.Models.NguoiTimViec", "NguoiDungId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NguoiDung");
                });

            modelBuilder.Entity("API_He_thong.Models.ThongBao", b =>
                {
                    b.HasOne("API_He_thong.Models.NguoiDung", "NguoiDung")
                        .WithMany("ThongBaos")
                        .HasForeignKey("NguoiDungId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NguoiDung");
                });

            modelBuilder.Entity("API_He_thong.Models.UngTuyen", b =>
                {
                    b.HasOne("API_He_thong.Models.CongViec", "CongViec")
                        .WithMany("UngTuyens")
                        .HasForeignKey("CongViecId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("API_He_thong.Models.NguoiTimViec", "NguoiTimViec")
                        .WithMany("UngTuyens")
                        .HasForeignKey("NguoiTimViecId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CongViec");

                    b.Navigation("NguoiTimViec");
                });

            modelBuilder.Entity("API_He_thong.Models.CongViec", b =>
                {
                    b.Navigation("KyNangCongViecs");

                    b.Navigation("UngTuyens");
                });

            modelBuilder.Entity("API_He_thong.Models.DanhMucKyNang", b =>
                {
                    b.Navigation("KyNangCongViecs");

                    b.Navigation("KyNangNguoiXinViecs");
                });

            modelBuilder.Entity("API_He_thong.Models.districts", b =>
                {
                    b.Navigation("CongViecs");

                    b.Navigation("NguoiDungs");

                    b.Navigation("doanhNghieps");
                });

            modelBuilder.Entity("API_He_thong.Models.DoanhNghiep", b =>
                {
                    b.Navigation("CongViecs");
                });

            modelBuilder.Entity("API_He_thong.Models.NguoiDung", b =>
                {
                    b.Navigation("DoanhNghiep")
                        .IsRequired();

                    b.Navigation("NguoiTimViec")
                        .IsRequired();

                    b.Navigation("ThongBaos");
                });

            modelBuilder.Entity("API_He_thong.Models.NguoiTimViec", b =>
                {
                    b.Navigation("KyNangNguoiXinViecs");

                    b.Navigation("UngTuyens");
                });

            modelBuilder.Entity("API_He_thong.Models.PhanQuyen", b =>
                {
                    b.Navigation("NguoiDungs");
                });

            modelBuilder.Entity("API_He_thong.Models.provinces", b =>
                {
                    b.Navigation("CongViecs");

                    b.Navigation("NguoiDungs");

                    b.Navigation("doanhNghieps");
                });

            modelBuilder.Entity("API_He_thong.Models.UngTuyen", b =>
                {
                    b.Navigation("DanhGias");
                });

            modelBuilder.Entity("API_He_thong.Models.wards", b =>
                {
                    b.Navigation("CongViecs");

                    b.Navigation("NguoiDungs");

                    b.Navigation("doanhNghieps");
                });
#pragma warning restore 612, 618
        }
    }
}
