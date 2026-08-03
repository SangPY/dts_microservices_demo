using dts_users_service.Models;

namespace dts_users_service.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Students> GetAllStudents()
        {
            return _context.Students.ToList();
        }

        public Students? GetStudentById(int id)
        {
            return _context.Students.FirstOrDefault(x => x.Id == id);
        }

        public Students AddStudent(Students student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();

            return student;
        }

        public Students UpdateStudent(Students student)
        {
            _context.Students.Update(student);
            _context.SaveChanges();

            return student;
        }

        public bool DeleteStudent(int id)
        {
            var student = _context.Students.Find(id);

            if (student == null)
                return false;

            _context.Students.Remove(student);
            _context.SaveChanges();

            return true;
        }
    }
}
