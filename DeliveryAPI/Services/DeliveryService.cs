using DeliveryAPI.Data;
using DeliveryAPI.Interfaces;
using DeliveryAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DeliveryAPI.Services
{
    public class DeliveryService : IDeliveryService
    {
        private DeliveryContext context;

        public DeliveryService(DeliveryContext deliveryContext)
        {
            context = deliveryContext;
        }

        public bool AddNewDelivery(Delivery delivery)
        {
            if (delivery == null) return false;
            context.Deliveries.Add(delivery);
            context.SaveChanges();
            return true;
        }

        public bool ChangeDelivery(Delivery delivery)
        {
            if (delivery == null) return false;
            if (delivery.Status != "Новая") return false;
            var deliveryForEdit = context.Deliveries.Find(delivery.Id);
            if (deliveryForEdit == null) return false;
            deliveryForEdit.DeliveryTime = delivery.DeliveryTime;
            deliveryForEdit.DeliveryAddress = delivery.DeliveryAddress;
            deliveryForEdit.CargoDescription = delivery.CargoDescription;
            deliveryForEdit.ClientName = delivery.ClientName;
            deliveryForEdit.CancellationReason = delivery.CancellationReason;
            deliveryForEdit.Status = delivery.Status;
            return true;
        }

        public bool NextStage(int id)
        {
            var deliveryForEdit = context.Deliveries.Find(id);
            if (deliveryForEdit == null) return false;
            deliveryForEdit.Status = "Передано на выполнение";
            context.SaveChanges();
            return true;
        }

        public bool RemoveNewDelivery(int id)
        {
            var delivery = context.Deliveries.FirstOrDefault(x => x.Id == id);
            if (delivery == null) return false;
            context.Deliveries.Remove(delivery);
            context.SaveChanges();
            return true;
        }

        public List<Delivery> SearchDeliveriesByText(string text)
        {
            var allDeliveries = context.Deliveries.ToList();
            if (!allDeliveries.Any()) return new List<Delivery>();
            if (!string.IsNullOrEmpty(text))
            {
                var findedRows = allDeliveries.Where(d =>
                d.Status.Contains(text, StringComparison.OrdinalIgnoreCase) ||
                d.CargoDescription.Contains(text, StringComparison.OrdinalIgnoreCase) ||
                d.CancellationReason.Contains(text, StringComparison.OrdinalIgnoreCase) ||
                d.ClientName.Contains(text, StringComparison.OrdinalIgnoreCase) ||
                d.DeliveryAddress.Contains(text, StringComparison.OrdinalIgnoreCase)).ToList();
                if(findedRows.Any()) return findedRows;
                else return new List<Delivery>();
            }
            return allDeliveries;
        }

        public List<Delivery> ShowAllDeliveries()
        {
            var allDeliveries = context.Deliveries.ToList();
            if (allDeliveries.Any()) return allDeliveries;
            else return new List<Delivery>();
        }
    }
}
