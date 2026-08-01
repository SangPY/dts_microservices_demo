using System.ComponentModel.DataAnnotations.Schema;

namespace dts_catelogy_service.Models
{
    [Table("Catalog")]
    public class Cataloge
    {
        public int id { get; set; }

        public string name { get; set; }

        public decimal price { get; set; }
    }
}
