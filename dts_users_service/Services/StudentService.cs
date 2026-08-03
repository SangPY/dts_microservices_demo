using AutoMapper;
using dts_users_service.Dto;
using dts_users_service.Models;
using dts_users_service.Repositories;
using FluentValidation;

namespace dts_users_service.Services
{
    public class StudentService : IStudentService
    {
        private readonly IMapper _mapper;
        private readonly IStudentRepository _repository;
        private readonly IValidator<CreateStudentDto> _validator;
        private readonly ILogger<StudentService> _logger;


        public StudentService(IMapper mapper, IStudentRepository repository, IValidator<CreateStudentDto> validator, ILogger<StudentService> logger)
        {
            _mapper = mapper;
            _repository = repository;
            _validator = validator;
            _logger = logger;
        }

        public async Task<IEnumerable<StudentDto>> GetAllStudentsAsync()
        {
            var students = await _repository.GetAllStudentsAsync();
            return _mapper.Map<IEnumerable<StudentDto>>(students);
        }

        public async Task<StudentDto?> GetStudentByIdAsync(int id)
        {
            var student = await _repository.GetStudentByIdAsync(id);

            if (student == null)
                return null;

            return _mapper.Map<StudentDto?>(student);
        }

        public async Task<StudentDto> CreateStudentAsync(CreateStudentDto dto)
        {
            // Business Logic sẽ viết ở đây
            //var existed = await _repository.ExistsByEmailAsync(dto.Email);

            //if (existed)
            //    throw new Exception("Email already exists.");
            _logger.LogInformation(
                "Creating student {StudentName}",
                dto.StudentName);

            var validation = await _validator.ValidateAsync(dto);

            if (!validation.IsValid)
                throw new ValidationException(validation.Errors);

            var student = _mapper.Map<Students>(dto);

            student = await _repository.AddStudentAsync(student);

            _logger.LogInformation(
                "Student {StudentId} created successfully",
                student.Id);

            return _mapper.Map<StudentDto>(student);
        }

        public async Task<StudentDto?> UpdateStudentAsync(UpdateStudentDto dto)
        {
            _logger.LogInformation(
                "Updating student {StudentId}",
                dto.Id);

            var student = await _repository.GetStudentByIdAsync(dto.Id);

            if (student == null)
                throw new KeyNotFoundException("Student not found.");

            _mapper.Map(dto, student);

            await _repository.UpdateStudentAsync(student);

            _logger.LogInformation(
                "Student {StudentId} updated successfully",
                student.Id);

            return _mapper.Map<StudentDto>(student);
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            var student = await _repository.GetStudentByIdAsync(id);

            if (student == null)
                return false;

            await _repository.DeleteStudentAsync(student);

            _logger.LogInformation(
                "Deleting student {StudentId}",
                id);

            return true;
        }
    }
}
