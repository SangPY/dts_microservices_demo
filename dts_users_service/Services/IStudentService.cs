using dts_users_service.Dto;

namespace dts_users_service.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentDto>> GetAllStudentsAsync();

        Task<StudentDto?> GetStudentByIdAsync(int id);

        Task<StudentDto> CreateStudentAsync(CreateStudentDto dto);

        Task<StudentDto?> UpdateStudentAsync(UpdateStudentDto dto);

        Task<bool> DeleteStudentAsync(int id);
    }
}
