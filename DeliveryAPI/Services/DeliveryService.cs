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

        public async Task<bool> AddNewDelivery(Delivery delivery)
        {
            if (delivery == null) return false;
            await context.Deliveries.AddAsync(delivery);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ChangeDelivery(Delivery delivery)
        {
            if (delivery == null) return false;
            if (delivery.Status != "Новая") return false;
            var deliveryForEdit = await context.Deliveries.FindAsync(delivery.Id);
            if (deliveryForEdit == null) return false;
            deliveryForEdit.DeliveryTime = delivery.DeliveryTime;
            deliveryForEdit.DeliveryAddress = delivery.DeliveryAddress;
            deliveryForEdit.CargoDescription = delivery.CargoDescription;
            deliveryForEdit.ClientName = delivery.ClientName;
            deliveryForEdit.CancellationReason = delivery.CancellationReason;
            deliveryForEdit.Status = delivery.Status;
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> NextStage(int id)
        {
            var deliveryForEdit = await context.Deliveries.FindAsync(id);
            if (deliveryForEdit == null) return false;
            deliveryForEdit.Status = "Передано на выполнение";
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveNewDelivery(int id)
        {
            var delivery = await context.Deliveries.FirstOrDefaultAsync(x => x.Id == id);
            if (delivery == null) return false;
            context.Deliveries.Remove(delivery);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Delivery>> SearchDeliveriesByText(string text)
        {
            var deliveriesQuery = context.Deliveries.AsQueryable();

            if (!string.IsNullOrEmpty(text)) return new List<Delivery>();

            deliveriesQuery = deliveriesQuery.Where(d =>
                d.Status.Contains(text, StringComparison.OrdinalIgnoreCase) ||
                d.CargoDescription.Contains(text, StringComparison.OrdinalIgnoreCase) ||
                d.CancellationReason.Contains(text, StringComparison.OrdinalIgnoreCase) ||
                d.ClientName.Contains(text, StringComparison.OrdinalIgnoreCase) ||
                d.DeliveryAddress.Contains(text, StringComparison.OrdinalIgnoreCase));


            return await deliveriesQuery.ToListAsync();
        }

        public async Task<List<Delivery>> ShowAllDeliveries()
        {
            return await context.Deliveries.ToListAsync();
        }
    }
}
