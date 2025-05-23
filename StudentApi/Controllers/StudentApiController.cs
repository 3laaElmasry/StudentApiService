using BuisnessLogicLayer.IServiceContracts;
using DataAccessLayer.DTO;
using Microsoft.AspNetCore.Mvc;


namespace StudentApi.Controllers
{
    
    public class StudentApiController : CutstomControllerBase
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

        [HttpGet]
        [Route("[action]")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<double> GetAverage()
        {
            double res = _studentService.GetAverage();
            if(res <= 0)
            {
                return NotFound("No Students Found");

            }
            return Ok(res);
        }


        [HttpGet]
        [HttpGet("{id}", Name = "GetStudentById")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        

        public ActionResult<StudentDto> GetStudentById(int id)
        {
            if(id <= 0)
            {
                return BadRequest($"The Id {id} was not Accepted.");
            }
            var student = _studentService.GetStudentById(id);
            if (student is null)
            {
                return NotFound($"Student With Id {id} Is Not Found.");
            }
            return Ok(student);
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult<StudentDto> AddNewStudent(StudentDto newStudent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid student data provided.");
            }

            var studentDto =  _studentService.AddNewStudent(newStudent);
            if (studentDto == null)
            {
                return Conflict("A student with the same details already exists.");
            }

            // Assuming you have a GetStudentById action with route name "GetStudentById"
            return CreatedAtRoute("GetStudentById", new { id = studentDto.Id }, studentDto);
        }


        [HttpPut]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<StudentDto> Update(int Id, StudentDto newStudentData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid student data provided.");
            }

            if(Id <= 0)
            {
                return BadRequest($"The Id {Id} is not accepted.");
            }

            var studentAfterUpadted = _studentService.UpdateStudent(Id, newStudentData);

            if(studentAfterUpadted is null)
            {
                return NotFound($"The Student With Id {Id} is not Found");
            }

            
            return Ok(studentAfterUpadted);
        }


        [HttpDelete]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult DeleteStudent(int Id)
        {
            if (Id <= 0)
            {
                return BadRequest($"The Id {Id} is not accepted.");
            }

            if (_studentService.DeleteStudent(Id))
            {
                return Ok($"Student With Id {Id} was been deleted.");
            }
            else
            {
                return NotFound($"The Student With Id {Id} is not Found");
            }
        }
    }
}
