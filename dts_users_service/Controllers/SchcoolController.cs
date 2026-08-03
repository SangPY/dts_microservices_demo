using dts_users_service.Dto;
using dts_users_service.Models;
using dts_users_service.Repositories;
using dts_users_service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace dts_users_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public SchoolController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("GetAllStudents")]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetAllStudents()
        {
            var students = await _studentService.GetAllStudentsAsync();

            return Ok(students);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDto?>> GetStudentById(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);

            if (student == null)
                return NotFound();

            return Ok(student);
        }


        [HttpPost("CreateStudent")]
        public async Task<ActionResult<StudentDto>> Create(CreateStudentDto dto)
        {
            var student = await _studentService.CreateStudentAsync(dto);

            return CreatedAtAction(nameof(GetStudentById), new { id = student.Id }, student);
        }

        [HttpPut("UpdateStudent")]
        public async Task<ActionResult<StudentDto?>> Update(UpdateStudentDto dto)
        {
            var student = await _studentService.UpdateStudentAsync(dto);

            if (student == null)
                return NotFound();

            return Ok(student);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteStudent(int id)
        {
            var result = await _studentService.DeleteStudentAsync(id);

            if (!result)
                return NotFound();

            return Ok("Deleted Successfully");
        }
    }
}
