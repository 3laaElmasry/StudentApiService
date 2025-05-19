using BuisnessLogicLayer.IServiceContracts;
using DataAccessLayer.Data;
using DataAccessLayer.DTO;
using StudentApi.Models;

namespace BuisnessLogicLayer.Services
{
    public class StudentService : IStudentService
    {

        private readonly ApplicationDbContext _context;

        public StudentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<StudentDto> GetAll()
        {
            var dtoStudents = _context.Students.Select(s => s.ToStudentDto());
            return dtoStudents;
        }
    }
}
