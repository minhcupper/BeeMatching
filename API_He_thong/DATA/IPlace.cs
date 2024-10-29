using API_He_thong.Models;

namespace API_He_thong.DATA
{
    public interface IPlace
    {
        Task<List<provinces>> Getprovinces();
        Task<List<wards>> Getwards();
        Task<List<districts>> Getdictrics();
    }
}
