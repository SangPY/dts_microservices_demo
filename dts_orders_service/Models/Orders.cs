using System.ComponentModel.DataAnnotations.Schema;

namespace dts_orders_service.Models
{
    [Table("Orders")]
    public class Orders
    {
        public int Id { get; set; }

        public int quantity { get; set; }

        public int itemId { get; set; }
    }
}
