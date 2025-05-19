
using DataAccessLayer.DTO;

namespace BuisnessLogicLayer.IServiceContracts
{
    public interface IStudentService
    {
        IEnumerable<StudentDto> GetAll();
    }
}
