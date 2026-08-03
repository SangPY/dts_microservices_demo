namespace dts_users_service.Dto
{
    public class StudentDto
    {
        public int Id { get; set; }

        public string StudentName { get; set; } = string.Empty;

        public int? Age { get; set; }

        public string Email { get; set; } = string.Empty;

        public string Class { get; set; } = string.Empty;
    }
}
