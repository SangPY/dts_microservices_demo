using dts_users_service.Dto;
using dts_users_service.Models;

namespace dts_users_service.Repositories
{
    public interface IStudentRepository
    {
        Task<IEnumerable<StudentDto>> GetAllStudentsAsync();

        Task<StudentDto?> GetStudentByIdAsync(int id);

        Task<StudentDto> AddStudentAsync(CreateStudentDto dto);

        Task<StudentDto> UpdateStudentAsync(UpdateStudentDto dto);

        Task<bool> DeleteStudentAsync(int id);

        Task<bool> ExistsByEmailAsync(string email);
    }
}
