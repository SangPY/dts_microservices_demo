using dts_users_service.Models;

namespace dts_users_service.Repositories
{
    public interface IStudentRepository
    {
        List<Students> GetAllStudents();

        Students? GetStudentById(int id);

        Students AddStudent(Students student);

        Students UpdateStudent(Students student);

        bool DeleteStudent(int id);
    }
}
