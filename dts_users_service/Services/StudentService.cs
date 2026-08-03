using dts_users_service.Dto;
using dts_users_service.Repositories;

namespace dts_users_service.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repository;

        public StudentService(IStudentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<StudentDto>> GetAllStudentsAsync()
        {
            return await _repository.GetAllStudentsAsync();
        }

        public async Task<StudentDto?> GetStudentByIdAsync(int id)
        {
            return await _repository.GetStudentByIdAsync(id);
        }

        public async Task<StudentDto> CreateStudentAsync(CreateStudentDto dto)
        {
            // Business Logic sẽ viết ở đây
            var existed = await _repository.ExistsByEmailAsync(dto.Email);

            if (existed)
                throw new Exception("Email already exists.");

            return await _repository.AddStudentAsync(dto);
        }

        public async Task<StudentDto?> UpdateStudentAsync(UpdateStudentDto dto)
        {
            return await _repository.UpdateStudentAsync(dto);
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            return await _repository.DeleteStudentAsync(id);
        }
    }
}
