namespace dts_users_service.Common
{
    public class ErrorResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; } = string.Empty;

        public List<string> Errors { get; set; } = new();

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public string TraceId { get; set; } = string.Empty;
    }
}
