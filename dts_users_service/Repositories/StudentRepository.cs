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

        public async Task<IEnumerable<Students>> GetAllStudentsAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Students?> GetStudentByIdAsync(int id)
        {
            return await _context.Students.FindAsync(id);
        }

        public async Task<Students> AddStudentAsync(Students students)
        {
            await _context.Students.AddAsync(students);
            await _context.SaveChangesAsync();
            return students;
        }

        public async Task<Students?> UpdateStudentAsync(Students students)
        {
            var existingStudent = await _context.Students.FindAsync(students.Id);

            if (existingStudent == null)
                return null;

            _context.Entry(existingStudent).CurrentValues.SetValues(students);
            await _context.SaveChangesAsync();

            return existingStudent;
        }

        public async Task<bool> DeleteStudentAsync(Students students)
        {
            var existingStudent = await _context.Students.FindAsync(students.Id);

            if (existingStudent == null)
                return false;

            _context.Students.Remove(existingStudent);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _context.Students.AnyAsync(x => x.Email == email);
        }
    }
}

