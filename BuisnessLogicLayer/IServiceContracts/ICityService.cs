using DataAccessLayer.Model;

namespace BuisnessLogicLayer.IServiceContracts
{
    public interface ICityService
    {
        IEnumerable<City> GetAllCities();
    }
}
