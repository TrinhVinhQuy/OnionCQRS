using OnionCQRS.Domain.Common;

namespace OnionCQRS.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }
    }
}
