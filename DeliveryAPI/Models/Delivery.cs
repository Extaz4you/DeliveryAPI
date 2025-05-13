using System.ComponentModel.DataAnnotations;

namespace DeliveryAPI.Models
{
    public class Delivery
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public string CargoDescription { get; set; }
        public string DeliveryAddress { get; set; }
        public DateTime DeliveryTime { get; set; }
        public string Status { get; set; }
        public string? CancellationReason { get; set; }
    }
}
