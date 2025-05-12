using DeliveryAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryAPI.Interfaces
{
    public interface IDeliveryService
    {
        public bool AddNewDelivery(Delivery delivery);
        public List<Delivery> ShowAllDeliveries();
        public bool ChangeDelivery(Delivery delivery);
        public bool RemoveNewDelivery(int id);
        public bool NextStage(int id);
        public List<Delivery> SearchDeliveriesByText(string text);
    }
}
