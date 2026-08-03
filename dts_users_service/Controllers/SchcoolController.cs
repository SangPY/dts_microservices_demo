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
        public IActionResult GetAllStudents()
        {
            var students = _studentRepository.GetAllStudents();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public IActionResult GetStudentById(int id)
        {
            var student = _studentRepository.GetStudentById(id);

            if (student == null)
                return NotFound();

            return Ok(student);
        }

        [HttpPost]
        public IActionResult AddStudent(Students student)
        {
            var result = _studentRepository.AddStudent(student);

            return Ok(result);
        }

        [HttpPost("CreateStudent")]
        public IActionResult Create(CreateStudentDto dto)
        {
            var student = new Students
            {
                StudentName = dto.StudentName,
                Age = dto.Age,
                DOB = dto.DOB,
                Email = dto.Email,
                FatherName = dto.FatherName,
                MotherName = dto.MotherName,
                Class = dto.Class,
                City = dto.City
            };

            _studentRepository.AddStudent(student);

            return Ok(student);
        }

        [HttpPut("UpdateStudent")]
        public IActionResult Update(UpdateStudentDto dto)
        {
            var student = _studentRepository.GetStudentById(dto.Id);

            if (student == null)
                return NotFound();

            student.StudentName = dto.StudentName;
            student.Age = dto.Age;
            student.DOB = dto.DOB;
            student.Email = dto.Email;
            student.FatherName = dto.FatherName;
            student.MotherName = dto.MotherName;
            student.Class = dto.Class;
            student.City = dto.City;

            _studentRepository.UpdateStudent(student);

            return Ok(student);
        }

        [HttpPut]
        public IActionResult UpdateStudent(Students student)
        {
            var result = _studentRepository.UpdateStudent(student);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            var result = _studentRepository.DeleteStudent(id);

            if (!result)
                return NotFound();

            return Ok("Deleted Successfully");
        }
    }
}
