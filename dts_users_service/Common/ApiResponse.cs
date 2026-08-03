namespace dts_users_service.Common
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }

        public string Message { get; set; } = string.Empty;

        public T? Data { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public string TraceId { get; set; } = string.Empty;
    }
}
