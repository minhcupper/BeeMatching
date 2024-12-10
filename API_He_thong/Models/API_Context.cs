using Microsoft.EntityFrameworkCore;

namespace API_He_thong.Models
{
   
        public class API_Context : DbContext
        {
            public API_Context(DbContextOptions<API_Context> options) : base(options) { }

            public DbSet<NguoiDung> NguoiDung { get; set; }
            public DbSet<NguoiTimViec> NguoiTimViec { get; set; }
            public DbSet<DoanhNghiep> DoanhNghiep { get; set; }
            public DbSet<KyNangNguoiXinViec> KyNangNguoiXinViec { get; set; }
            public DbSet<KyNangCongViec> KyNangCongViec { get; set; }
           public DbSet<KinhNghiemCongViec> KinhNghiemCongViec { get; set; }
           public DbSet<KinhnghiemNguoiTimViec> KinhnghiemNguoiTimViec { get; set; }
            public DbSet<DanhMucKyNang> DanhMucKyNang { get; set; }
            public DbSet<CongViec> CongViec { get; set; }
            public DbSet<UngTuyen> UngTuyen { get; set; }
            public DbSet<DanhGia> DanhGia { get; set; }
            public DbSet<ThongBao> ThongBao { get; set; }
            public DbSet<districts> districts { get; set; } // Adjusted class name to be singular
            public DbSet<wards> wards { get; set; }         // Adjusted class name to be singular
            public DbSet<provinces> provinces { get; set; } // Adjusted class name to be singular

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                // Foreign key relationships
                ConfigureNguoiTimViec(modelBuilder);
                ConfigureDoanhNghiep(modelBuilder);
                ConfigureKyNangNguoiXinViec(modelBuilder);
                ConfigureCongViec(modelBuilder);
                ConfigureUngTuyen(modelBuilder);
                ConfigureDanhGia(modelBuilder);
                ConfigureThongBao(modelBuilder);
                ConfigureKyNangCongViec(modelBuilder);
            modelBuilder.Entity<CongViec>()
        .Property(c => c.LuongHangThang)
        .HasPrecision(18, 4); // Adjust precision and scale based on your data needs
        }

            private void ConfigureNguoiTimViec(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<NguoiTimViec>()
                    .HasOne(ntv => ntv.NguoiDung)
                    .WithOne(nd => nd.NguoiTimViec)
                    .HasForeignKey<NguoiTimViec>(ntv => ntv.NguoiDungId)
                    .OnDelete(DeleteBehavior.Cascade);
            }

            private void ConfigureDoanhNghiep(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<DoanhNghiep>()
                    .HasOne(dn => dn.NguoiDung)
                    .WithOne(nd => nd.DoanhNghiep)
                    .HasForeignKey<DoanhNghiep>(dn => dn.NguoiDungId)
                    .OnDelete(DeleteBehavior.Cascade);
            }

            private void ConfigureKyNangNguoiXinViec(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<KyNangNguoiXinViec>()
                    .HasOne(kn => kn.NguoiTimViec)
                    .WithMany(ntv => ntv.KyNangNguoiXinViecs)
                    .HasForeignKey(kn => kn.NguoiTimViecId)
                    .OnDelete(DeleteBehavior.Restrict);

                modelBuilder.Entity<KyNangNguoiXinViec>()
                    .HasOne(kn => kn.DanhMucKyNang)
                    .WithMany(dmkn => dmkn.KyNangNguoiXinViecs)
                    .HasForeignKey(kn => kn.DanhMucKyNangId)
                    .OnDelete(DeleteBehavior.NoAction);
            }
        private void ConfigureKinhnghiemNguoiXinViec(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<KinhnghiemNguoiTimViec>()
                .HasOne(kn => kn.NguoiTimViec)
                .WithMany(ntv => ntv.KinhnghiemNguoiTimViecs)
                .HasForeignKey(kn => kn.NguoiTimViecId)
                .OnDelete(DeleteBehavior.Restrict);
        }
        private void ConfigureKinhnghiemcongviec(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<KinhNghiemCongViec>()
                .HasOne(kn => kn.CongViec)
                .WithMany(ntv => ntv.KinhNghiemCongViecs)
                .HasForeignKey(kn => kn.CongViecId)
                .OnDelete(DeleteBehavior.Restrict);
        }
        private void ConfigureKyNangCongViec(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<KyNangCongViec>()
                .HasOne(ckv => ckv.CongViec)
                .WithMany(cv => cv.KyNangCongViecs)
                .HasForeignKey(ckv => ckv.CongViecId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<KyNangCongViec>()
                .HasOne(ckv => ckv.DanhMucKyNang)
                .WithMany(dmkn => dmkn.KyNangCongViecs)
                .HasForeignKey(ckv => ckv.DanhMucKyNangId)
                .OnDelete(DeleteBehavior.NoAction);
        }
        private void ConfigureCongViec(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<CongViec>()
                    .HasOne(cv => cv.DoanhNghiep)
                    .WithMany(dn => dn.CongViecs)
                    .HasForeignKey(cv => cv.DoanhNghiepId)
                    .OnDelete(DeleteBehavior.Cascade);
            }

            private void ConfigureUngTuyen(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<UngTuyen>()
                    .HasOne(ut => ut.CongViec)
                    .WithMany(cv => cv.UngTuyens)
                    .HasForeignKey(ut => ut.CongViecId)
                    .OnDelete(DeleteBehavior.NoAction);

                modelBuilder.Entity<UngTuyen>()
                    .HasOne(ut => ut.NguoiTimViec)
                    .WithMany(ntv => ntv.UngTuyens)
                    .HasForeignKey(ut => ut.NguoiTimViecId)
                    .OnDelete(DeleteBehavior.Cascade);
            }

            private void ConfigureDanhGia(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<DanhGia>()
                    .HasOne(dg => dg.UngTuyen)
                    .WithOne(ut => ut.DanhGia)
                    .OnDelete(DeleteBehavior.Cascade);
           
        }

            private void ConfigureThongBao(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<ThongBao>()
                    .HasOne(tb => tb.NguoiDung)
                    .WithMany(nd => nd.ThongBaos)
                    .HasForeignKey(tb => tb.NguoiDungId)
                    .OnDelete(DeleteBehavior.Cascade);
            }

           
        }
    }
