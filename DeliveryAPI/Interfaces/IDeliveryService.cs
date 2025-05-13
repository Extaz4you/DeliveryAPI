using DeliveryAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryAPI.Interfaces
{
    public interface IDeliveryService
    {
        public Task<bool> AddNewDelivery(Delivery delivery);
        public Task<List<Delivery>> ShowAllDeliveries();
        public Task<bool> ChangeDelivery(Delivery delivery);
        public Task<bool> RemoveNewDelivery(int id);
        public Task<bool> NextStage(int id);
        public Task<List<Delivery>> SearchDeliveriesByText(string text);
    }
}
