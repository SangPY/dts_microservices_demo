using dts_users_service.Dto;
using dts_users_service.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace dts_users_service.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StudentDto>> GetAllStudentsAsync()
        {
            return await _context.Students
                .Select(x => new StudentDto
                {
                    Id = x.Id,
                    StudentName = x.StudentName,
                    Age = x.Age,
                    Email = x.Email,
                    Class = x.Class
                })
                .ToListAsync();
        }

        public async Task<StudentDto?> GetStudentByIdAsync(int id)
        {
            return await _context.Students
                .Where(x => x.Id == id)
                .Select(x => new StudentDto
                {
                    Id = x.Id,
                    StudentName = x.StudentName,
                    Age = x.Age,
                    Email = x.Email,
                    Class = x.Class
                })
                .FirstOrDefaultAsync();
        }

        public async Task<StudentDto> AddStudentAsync(CreateStudentDto dto)
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

            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();

            return new StudentDto
            {
                Id = student.Id,
                StudentName = student.StudentName,
                Age = student.Age,
                Email = student.Email,
                Class = student.Class
            };
        }

        public async Task<StudentDto?> UpdateStudentAsync(UpdateStudentDto dto)
        {
            var student = await _context.Students.FindAsync(dto.Id);

            if (student == null)
                return null;

            student.StudentName = dto.StudentName;
            student.Age = dto.Age;
            student.DOB = dto.DOB;
            student.Email = dto.Email;
            student.FatherName = dto.FatherName;
            student.MotherName = dto.MotherName;
            student.Class = dto.Class;
            student.City = dto.City;

            await _context.SaveChangesAsync();

            return new StudentDto
            {
                Id = student.Id,
                StudentName = student.StudentName,
                Age = student.Age,
                Email = student.Email,
                Class = student.Class
            };
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
                return false;

            _context.Students.Remove(student);

            await _context.SaveChangesAsync();

            return true;
        
    }
    }
}
