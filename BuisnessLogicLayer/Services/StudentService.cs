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

        public StudentDto? AddNewStudent(StudentDto student)
        {
            var studentFromDb = _context.Students
                .FirstOrDefault(s => s.Name == student.Name && s.Grade == student.Grade && s.Age == student.Age);

            if (studentFromDb is not null)
                return null;

            _context.Students.Add(student.ToStudent());
            Save();

            var dtoFromDb = _context.Students
                .FirstOrDefault(s => s.Name == student.Name && s.Grade == student.Grade && s.Age == student.Age);
            return dtoFromDb?.ToStudentDto();
        }

        public bool DeleteStudent(int Id)
        {
            var studentFromDb = _context.Students.FirstOrDefault(s => s.Id == Id);

            if(studentFromDb is null) 
                return false;
            _context.Remove(studentFromDb);
            Save();
            return true;
        }

        public IEnumerable<StudentDto> GetAll()
        {
            var dtoStudents = _context.Students.Select(s => s.ToStudentDto());
            return dtoStudents;
        }

        public double GetAverage()
        {
            return _context.Students.Average(s => s.Grade);
        }

        public IEnumerable<StudentDto> GetPassedStudents()
        {
            var passedStudents = _context.Students.Where(s => s.Grade >= 50)
                .Select(s => s.ToStudentDto());

            return passedStudents;
        }

        public StudentDto? GetStudentById(int id)
        {
            var student = _context.Students.FirstOrDefault(s => s.Id == id);
            return student is null ? null : student.ToStudentDto();
        }

        public StudentDto? UpdateStudent(int Id, StudentDto student)
        {
            var studentFromDb = _context.Students.FirstOrDefault(s => s.Id == Id);
            if(studentFromDb is null)
            {
                return null;
            }

            studentFromDb.Name = student.Name;
            studentFromDb.Age = student.Age;
            studentFromDb.Grade = student.Grade;
            _context.Update(studentFromDb);
            Save();
            return studentFromDb.ToStudentDto();
        }

        private void Save()
        {
            _context.SaveChanges();
        }
    }
}
