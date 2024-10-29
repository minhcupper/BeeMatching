using API_He_thong.Models;
using Microsoft.EntityFrameworkCore;

namespace API_He_thong.DATA
{
    public class PlaceService : IPlace
    {
        private readonly API_Context context;

        public PlaceService(API_Context context)
        {
            this.context = context;
        }

        public async Task<List<districts>> Getdictrics()
        {
            return await context.districts.ToListAsync();
        }

        public async Task<List<provinces>> Getprovinces()
        {
            return await context.provinces.ToListAsync();
        }

        public async Task<List<wards>> Getwards()
        {
            return await context.wards.ToListAsync();
        }
    }
}
