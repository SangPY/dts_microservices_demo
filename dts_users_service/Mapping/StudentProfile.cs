using AutoMapper;
using dts_users_service.Dto;
using dts_users_service.Models;

namespace dts_users_service.Mapping
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            // Entity -> DTO
            CreateMap<Students, StudentDto>();

            // DTO -> Entity
            CreateMap<CreateStudentDto, Students>();

            // Update DTO -> Entity
            CreateMap<UpdateStudentDto, Students>();
        }
    }
}
