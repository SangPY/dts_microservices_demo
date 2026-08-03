using dts_users_service.Dto;
using dts_users_service.Models;
using dts_users_service.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace dts_users_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;

        public SchoolController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet("GetAllStudents")]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetAllStudents()
        {
            var students = await _studentRepository.GetAllStudentsAsync();

            return Ok(students);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDto?>> GetStudentById(int id)
        {
            var student = await _studentRepository.GetStudentByIdAsync(id);

            if (student == null)
                return NotFound();

            return Ok(student);
        }


        [HttpPost("CreateStudent")]
        public async Task<ActionResult<StudentDto>> Create(CreateStudentDto dto)
        {
            var student = await _studentRepository.AddStudentAsync(dto);

            return CreatedAtAction(nameof(GetStudentById), new { id = student.Id }, student);
        }

        [HttpPut("UpdateStudent")]
        public async Task<ActionResult<StudentDto?>> Update(UpdateStudentDto dto)
        {
            var student = await _studentRepository.UpdateStudentAsync(dto);

            if (student == null)
                return NotFound();

            return Ok(student);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteStudent(int id)
        {
            var result = await _studentRepository.DeleteStudentAsync(id);

            if (!result)
                return NotFound();

            return Ok("Deleted Successfully");
        }
    }
}
