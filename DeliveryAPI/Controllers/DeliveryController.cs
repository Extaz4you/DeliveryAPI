using DeliveryAPI.Interfaces;
using DeliveryAPI.Models;
using DeliveryAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeliveryController : Controller
    {
        private IDeliveryService deliveryService;
        public DeliveryController(IDeliveryService service)
        {
            deliveryService = service;
        }
        [HttpPost("AddNewDelivery")]
        public ActionResult AddNewDelivery(Delivery delivery)
        {
            if (deliveryService.AddNewDelivery(delivery))
            {
                return Ok(delivery.Id);
            }
            else
            {
                return BadRequest("Ошибка при добавлении");
            }
        }

        [HttpGet("ShowAllDeliveries")]
        public ActionResult<List<Delivery>> ShowAllDeliveries()
        {
            var deliveries = deliveryService.ShowAllDeliveries();
            return Ok(deliveries);
        }

        [HttpPut("ChangeDelivery")]
        public ActionResult ChangeDelivery(Delivery delivery)
        {
            if (deliveryService.ChangeDelivery(delivery))
            {
                return Ok(delivery.Id);
            }
            else
            {
                return BadRequest("Ошибка при обновлении");
            }
        }

        [HttpDelete("RemoveNewDelivery/{id}")]
        public ActionResult RemoveNewDelivery(int id)
        {
            if (deliveryService.RemoveNewDelivery(id))
            {
                return Ok();
            }
            else
            {
                return NotFound($"Заявка не существует");
            }
        }

        [HttpPost("NextStage/{id}")]
        public ActionResult NextStage(int id)
        {
            if (deliveryService.NextStage(id))
            {
                return Ok();
            }
            else
            {
                return NotFound($"Заявка не существует");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<List<Delivery>> GetDeliveryByText(string text)
        {
            var delivery = deliveryService.SearchDeliveriesByText(text);
            if (delivery == null)
            {
                return NotFound();
            }
            return Ok(delivery);
        }
    }

}

