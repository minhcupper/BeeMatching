﻿using API_He_thong.Models;
using Microsoft.EntityFrameworkCore;

namespace API_He_thong.DATA
{
    public class CongViecService :ICongViec
    {
        private readonly API_Context context;

        public CongViecService(API_Context context)
        {
            this.context = context;
        }

      /*  public async Task<List<CongViec>> GetJobByDangCongViec(string dangCongViec)
        {
            var ListCongViec = await context.CongViec.ToListAsync();
            var dangCongViecHasSelectedList = new List<CongViec>();
            foreach(var item in  ListCongViec)
            {
                if(item.DangCongViec.ToLower() == dangCongViec.ToLower())
                {
                    dangCongViecHasSelectedList.Add(item);
                }
            }
            return dangCongViecHasSelectedList;
        }*/

        public async Task<bool> DeleteJob(int id)
        {
            var dl = await context.CongViec.FindAsync(id);
            if (dl != null)
            {
                // Xóa người dùng
                context.CongViec.Remove(dl);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<CongViec> GetIdJob(int id)
        {
            var dl = await context.CongViec.FindAsync(id);
            if (dl != null)
            {
                return dl;
            }
            return dl; // Or throw an exception, depending on your error handling preference
        }

        public async Task<List<CongViec>> Getjob()
        {
            return await context.CongViec.ToListAsync();
        }

        public async Task<bool> PostJob(CongViec user)
        {
            await context.CongViec.AddAsync(user);
            var result = await context.SaveChangesAsync();
            return result > 0; // True if at least one record was added
        }

        public async Task<bool> PutJob(int id, CongViec user)
        {
            var existingUser = await context.CongViec.FirstOrDefaultAsync(x => x.CongViecId == id);
            if (existingUser != null)
            {
                // Update fields
               // existingUser.KyNangCongViecs = user.KyNangCongViecs;
                existingUser.NgayDang = user.NgayDang;
                existingUser.UngTuyens = user.UngTuyens;
                existingUser.MoTa = user.MoTa;
                existingUser.DoanhNghiep = user.DoanhNghiep;
                existingUser.TrangThai=user.TrangThai;
                existingUser.ViTri=user.ViTri;
                existingUser.WardId = user.WardId;
                existingUser.LuongHangThang = user.LuongHangThang;
                existingUser.HanNopHoSo = user.HanNopHoSo;
                existingUser.TieuDe = user.TieuDe;
                existingUser.ViTri = user.ViTri;
                existingUser.ProvinceId = user.ProvinceId   ;
                existingUser.DistrictId = user.DistrictId;
                existingUser.SoLuong=user.SoLuong;
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
