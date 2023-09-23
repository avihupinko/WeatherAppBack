using WheaterForcast.Models;

namespace WheaterForcast.Interfaces
{
    public interface IFrocastService
    {

        Task<string> ForcastTitle(string query);

        Task<ForcastLogicModel> GetForcastAsync(string query, int days = 3);
    }
}
