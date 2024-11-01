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
                    b.Property<int>("cong_viec_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("cong_viec_id"), 1L, 1);

                    b.Property<string>("dia_diem_lam_viec")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("doanh_nghiep_id")
                        .HasColumnType("int");

                    b.Property<DateTime?>("han_nop_ho_so")
                        .HasColumnType("datetime2");

                    b.Property<string>("ky_nang_yeu_cau")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("luong_hang_thang")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("mo_ta")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ngay_dang")
                        .HasColumnType("datetime2");

                    b.Property<string>("tieu_de")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("trang_thai")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("vi_tri")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("cong_viec_id");

                    b.HasIndex("doanh_nghiep_id");

                    b.ToTable("CongViec");
                });

            modelBuilder.Entity("API_He_thong.Models.DanhGia", b =>
                {
                    b.Property<int>("danh_gia_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("danh_gia_id"), 1L, 1);

                    b.Property<int>("diem_danh_gia")
                        .HasColumnType("int");

                    b.Property<DateTime>("ngay_danh_gia")
                        .HasColumnType("datetime2");

                    b.Property<string>("noi_dung_danh_gia")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("ung_tuyen_id")
                        .HasColumnType("int");

                    b.HasKey("danh_gia_id");

                    b.HasIndex("ung_tuyen_id");

                    b.ToTable("DanhGia");
                });

            modelBuilder.Entity("API_He_thong.Models.DanhMucKyNang", b =>
                {
                    b.Property<int>("danh_muc_ky_nang_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("danh_muc_ky_nang_id"), 1L, 1);

                    b.Property<string>("mo_ta")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ten_danh_muc")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("danh_muc_ky_nang_id");

                    b.ToTable("DanhMucKyNang");
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
                    b.Property<int>("doanh_nghiep_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("doanh_nghiep_id"), 1L, 1);

                    b.Property<string>("dia_chi")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("mo_ta")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("nguoi_dung_id")
                        .HasColumnType("int");

                    b.Property<string>("ten_cong_ty")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("doanh_nghiep_id");

                    b.HasIndex("nguoi_dung_id")
                        .IsUnique();

                    b.ToTable("DoanhNghiep");
                });

            modelBuilder.Entity("API_He_thong.Models.KyNang", b =>
                {
                    b.Property<int>("ky_nang_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ky_nang_id"), 1L, 1);

                    b.Property<int>("danh_muc_ky_nang_id")
                        .HasColumnType("int");

                    b.Property<string>("mo_ta")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("nguoi_tim_viec_id")
                        .HasColumnType("int");

                    b.Property<string>("ten_ky_nang")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ky_nang_id");

                    b.HasIndex("danh_muc_ky_nang_id");

                    b.HasIndex("nguoi_tim_viec_id");

                    b.ToTable("KyNang");
                });

            modelBuilder.Entity("API_He_thong.Models.NguoiDung", b =>
                {
                    b.Property<int>("nguoi_dung_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("nguoi_dung_id"), 1L, 1);

                    b.Property<string>("dia_chi")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("gioi_tinh")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("ho_ten")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("loai_nguoi_dung")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("mat_khau")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTime?>("ngay_sinh")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ngay_tao")
                        .HasColumnType("datetime2");

                    b.Property<string>("so_dien_thoai")
                        .IsRequired()
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

                    b.ToTable("NguoiDung");
                });

            modelBuilder.Entity("API_He_thong.Models.NguoiTimViec", b =>
                {
                    b.Property<int>("nguoi_tim_viec_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("nguoi_tim_viec_id"), 1L, 1);

                    b.Property<string>("hoat_dong_ngoai_khoa")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("kinh_nghiem")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("mo_ta")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("ngon_ngu")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("nguoi_dung_id")
                        .HasColumnType("int");

                    b.HasKey("nguoi_tim_viec_id");

                    b.HasIndex("nguoi_dung_id")
                        .IsUnique();

                    b.ToTable("NguoiTimViec");
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
                    b.Property<int>("thong_bao_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("thong_bao_id"), 1L, 1);

                    b.Property<DateTime>("ngay_gui")
                        .HasColumnType("datetime2");

                    b.Property<int>("nguoi_dung_id")
                        .HasColumnType("int");

                    b.Property<string>("noi_dung")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("tieu_de")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("trang_thai")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("thong_bao_id");

                    b.HasIndex("nguoi_dung_id");

                    b.ToTable("ThongBao");
                });

            modelBuilder.Entity("API_He_thong.Models.UngTuyen", b =>
                {
                    b.Property<int>("ung_tuyen_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ung_tuyen_id"), 1L, 1);

                    b.Property<bool>("chap_nhan_cong_viec")
                        .HasColumnType("bit");

                    b.Property<int>("cong_viec_id")
                        .HasColumnType("int");

                    b.Property<string>("de_xuat")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ngay_ung_tuyen")
                        .HasColumnType("datetime2");

                    b.Property<int>("nguoi_tim_viec_id")
                        .HasColumnType("int");

                    b.Property<string>("trang_thai")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ung_tuyen_id");

                    b.HasIndex("cong_viec_id");

                    b.HasIndex("nguoi_tim_viec_id");

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
                    b.HasOne("API_He_thong.Models.DoanhNghiep", "DoanhNghiep")
                        .WithMany("CongViecs")
                        .HasForeignKey("doanh_nghiep_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DoanhNghiep");
                });

            modelBuilder.Entity("API_He_thong.Models.DanhGia", b =>
                {
                    b.HasOne("API_He_thong.Models.UngTuyen", "UngTuyen")
                        .WithMany("DanhGias")
                        .HasForeignKey("ung_tuyen_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UngTuyen");
                });

            modelBuilder.Entity("API_He_thong.Models.DoanhNghiep", b =>
                {
                    b.HasOne("API_He_thong.Models.NguoiDung", "NguoiDung")
                        .WithOne("DoanhNghiep")
                        .HasForeignKey("API_He_thong.Models.DoanhNghiep", "nguoi_dung_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NguoiDung");
                });

            modelBuilder.Entity("API_He_thong.Models.KyNang", b =>
                {
                    b.HasOne("API_He_thong.Models.DanhMucKyNang", "DanhMucKyNang")
                        .WithMany("KyNangs")
                        .HasForeignKey("danh_muc_ky_nang_id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("API_He_thong.Models.NguoiTimViec", "NguoiTimViec")
                        .WithMany("KyNangs")
                        .HasForeignKey("nguoi_tim_viec_id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("DanhMucKyNang");

                    b.Navigation("NguoiTimViec");
                });

            modelBuilder.Entity("API_He_thong.Models.NguoiTimViec", b =>
                {
                    b.HasOne("API_He_thong.Models.NguoiDung", "NguoiDung")
                        .WithOne("NguoiTimViec")
                        .HasForeignKey("API_He_thong.Models.NguoiTimViec", "nguoi_dung_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NguoiDung");
                });

            modelBuilder.Entity("API_He_thong.Models.ThongBao", b =>
                {
                    b.HasOne("API_He_thong.Models.NguoiDung", "NguoiDung")
                        .WithMany("ThongBaos")
                        .HasForeignKey("nguoi_dung_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NguoiDung");
                });

            modelBuilder.Entity("API_He_thong.Models.UngTuyen", b =>
                {
                    b.HasOne("API_He_thong.Models.CongViec", "CongViec")
                        .WithMany("UngTuyens")
                        .HasForeignKey("cong_viec_id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("API_He_thong.Models.NguoiTimViec", "NguoiTimViec")
                        .WithMany("UngTuyens")
                        .HasForeignKey("nguoi_tim_viec_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CongViec");

                    b.Navigation("NguoiTimViec");
                });

            modelBuilder.Entity("API_He_thong.Models.CongViec", b =>
                {
                    b.Navigation("UngTuyens");
                });

            modelBuilder.Entity("API_He_thong.Models.DanhMucKyNang", b =>
                {
                    b.Navigation("KyNangs");
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
                    b.Navigation("KyNangs");

                    b.Navigation("UngTuyens");
                });

            modelBuilder.Entity("API_He_thong.Models.UngTuyen", b =>
                {
                    b.Navigation("DanhGias");
                });
#pragma warning restore 612, 618
        }
    }
}
