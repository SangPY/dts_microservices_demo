using dts_users_service.Dto;
using dts_users_service.Models;

namespace dts_users_service.Repositories
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Students>> GetAllStudentsAsync();

        Task<Students?> GetStudentByIdAsync(int id);

        Task<Students> AddStudentAsync(Students students);

        Task<Students> UpdateStudentAsync(Students students);

        Task<bool> DeleteStudentAsync(Students students);

        Task<bool> ExistsByEmailAsync(string email);
    }
}
