using Microsoft.EntityFrameworkCore;

namespace API_He_thong.Models
{
    public class API_Context : DbContext
    {
        public API_Context(DbContextOptions<API_Context> options) : base(options) { }

        public DbSet<NguoiDung> NguoiDung { get; set; }
        public DbSet<TaiKhoanNguoiDung> TaiKhoanNguoiDung { get; set; }
        public DbSet<NguoiTimViec> NguoiTimViec { get; set; }
        public DbSet<DoanhNghiep> DoanhNghiep { get; set; }
        public DbSet<KyNang> KyNang { get; set; }
        public DbSet<DanhMucKyNang> danhMucKyNang { get; set; }
        public DbSet<CongViec> CongViec { get; set; }
        public DbSet<UngTuyen> UngTuyen { get; set; }
        public DbSet<DanhGia> DanhGia { get; set; }
        public DbSet<ThongBao> ThongBao { get; set; }
        public DbSet<districts> districts { get; set; }
        public DbSet<wards> wards{ get; set; }
        public DbSet<provinces> provinces { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Khóa ngoại cho NguoiTimViec -> NguoiDung
            modelBuilder.Entity<NguoiTimViec>()
                .HasOne(ntv => ntv.NguoiDung)
                .WithOne(nd => nd.NguoiTimViec)
                .HasForeignKey<NguoiTimViec>(ntv => ntv.nguoi_dung_id)
                .OnDelete(DeleteBehavior.Cascade);  // Xóa cascade

            // Khóa ngoại cho DoanhNghiep -> NguoiDung
            modelBuilder.Entity<DoanhNghiep>()
                .HasOne(dn => dn.NguoiDung)
                .WithOne(nd => nd.DoanhNghiep)
                .HasForeignKey<DoanhNghiep>(dn => dn.nguoi_dung_id)
                .OnDelete(DeleteBehavior.Cascade);  // Xóa cascade

            // Khóa ngoại cho KyNang -> NguoiTimViec
            modelBuilder.Entity<KyNang>()
                .HasOne(kn => kn.NguoiTimViec)
                .WithMany(ntv => ntv.KyNangs)
                .HasForeignKey(kn => kn.nguoi_tim_viec_id)
                .OnDelete(DeleteBehavior.Restrict); // Giữ Restrict hoặc có thể thay đổi thành NoAction

            // Khóa ngoại cho KyNang -> DanhMucKyNang
            modelBuilder.Entity<KyNang>()
                .HasOne(kn => kn.DanhMucKyNang)
                .WithMany(dmkn => dmkn.KyNangs)
                .HasForeignKey(kn => kn.danh_muc_ky_nang_id)
                .OnDelete(DeleteBehavior.NoAction); // Không xóa khi khóa ngoại bị xóa

            // Khóa ngoại cho CongViec -> DoanhNghiep
            modelBuilder.Entity<CongViec>()
                .HasOne(cv => cv.DoanhNghiep)
                .WithMany(dn => dn.CongViecs)
                .HasForeignKey(cv => cv.doanh_nghiep_id)
                .OnDelete(DeleteBehavior.Cascade); // Xóa cascade

            // Khóa ngoại cho UngTuyen -> CongViec
            modelBuilder.Entity<UngTuyen>()
                .HasOne(ut => ut.CongViec)
                .WithMany(cv => cv.UngTuyens)
                .HasForeignKey(ut => ut.cong_viec_id)
                .OnDelete(DeleteBehavior.NoAction); // Không xóa

            // Khóa ngoại cho UngTuyen -> NguoiTimViec
            modelBuilder.Entity<UngTuyen>()
                .HasOne(ut => ut.NguoiTimViec)
                .WithMany(ntv => ntv.UngTuyens)
                .HasForeignKey(ut => ut.nguoi_tim_viec_id)
                .OnDelete(DeleteBehavior.Cascade); // Giữ xóa cascade ở đây

            // Khóa ngoại cho DanhGia -> UngTuyen
            modelBuilder.Entity<DanhGia>()
                .HasOne(dg => dg.UngTuyen)
                .WithMany(ut => ut.DanhGias)
                .HasForeignKey(dg => dg.ung_tuyen_id)
                .OnDelete(DeleteBehavior.Cascade); // Giữ xóa cascade

            // Khóa ngoại cho ThongBao -> NguoiDung
            modelBuilder.Entity<ThongBao>()
                .HasOne(tb => tb.NguoiDung)
                .WithMany(nd => nd.ThongBaos)
                .HasForeignKey(tb => tb.nguoi_dung_id)
                .OnDelete(DeleteBehavior.Cascade); // Giữ xóa cascade
        }
    }
}