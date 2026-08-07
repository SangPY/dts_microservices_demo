using System.ComponentModel.DataAnnotations.Schema;

namespace dts_rabbitmq.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string ProductName { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public double Price { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
