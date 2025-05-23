﻿
using DataAccessLayer.DTO;

namespace BuisnessLogicLayer.IServiceContracts
{
    public interface IStudentService
    {
        IEnumerable<StudentDto> GetAll();
        IEnumerable<StudentDto> GetPassedStudents();

        Double GetAverage();

        StudentDto? GetStudentById(int id);

        StudentDto? AddNewStudent(StudentDto student);

        StudentDto? UpdateStudent(int Id, StudentDto student);

        bool DeleteStudent(int Id);

    }
}
