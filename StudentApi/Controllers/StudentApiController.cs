using BuisnessLogicLayer.IServiceContracts;
using DataAccessLayer.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.AccessControl;

namespace StudentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentApiController : ControllerBase
    {

        private readonly IStudentService _studentService;

        public StudentApiController(IStudentService studentService)
        {
            _studentService = studentService;
        }


        [HttpGet]
        [Route("[action]")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<StudentDto>> GetAllStudents()
        {
            var studenst = _studentService.GetAll();

            if(studenst.Count() == 0)
            {
                return NotFound("No Students Found");
            }
            return Ok(studenst);
        }

        [HttpGet]
        [Route("[action]")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<StudentDto>> GetPassedStudents()
        {
            var studenst = _studentService.GetPassedStudents();

            if (studenst.Count() == 0)
            {
                return NotFound("No Students Passed Found");
            }
            return Ok(studenst);
        }
    }
}
