using System.ComponentModel.DataAnnotations;

namespace dts_users_service.Dto
{
    public class UpdateStudentDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string StudentName { get; set; } = string.Empty;

        public int? Age { get; set; }

        public DateTime? DOB { get; set; }

        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string FatherName { get; set; } = string.Empty;

        public string MotherName { get; set; } = string.Empty;

        public string Class { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;
    }
}
