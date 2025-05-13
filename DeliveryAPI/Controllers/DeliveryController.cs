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
        private ILogger<DeliveryController> logger;

        public DeliveryController(IDeliveryService service, ILogger<DeliveryController> deliveryLogger)
        {
            deliveryService = service;
            logger = deliveryLogger;
        }


        [HttpPost("AddNewDelivery")]
        public async Task<ActionResult> AddNewDelivery(Delivery delivery)
        {
            if (await deliveryService.AddNewDelivery(delivery))
            {
                return Ok(delivery.Id);
            }
            else
            {
                logger.LogInformation("Ошибка при добавлении данных");
                return BadRequest("Ошибка при добавлении");
            }
        }

        [HttpGet("ShowAllDeliveries")]
        public async Task<ActionResult<List<Delivery>>> ShowAllDeliveries()
        {
            var deliveries = await deliveryService.ShowAllDeliveries();
            return Ok(deliveries);
        }

        [HttpPut("ChangeDelivery")]
        public async Task<ActionResult> ChangeDelivery(Delivery delivery)
        {
            if (await deliveryService.ChangeDelivery(delivery))
            {
                return Ok(delivery.Id);
            }
            else
            {
                logger.LogInformation("Ошибка при обновлении данных");
                return BadRequest("Ошибка при обновлении");
            }
        }

        [HttpDelete("RemoveNewDelivery/{id}")]
        public async Task<ActionResult> RemoveNewDelivery(int id)
        {
            if (await deliveryService.RemoveNewDelivery(id))
            {
                return Ok();
            }
            else
            {
                logger.LogInformation("Попытка удалить несуществующую запись");
                return NotFound($"Заявка не существует");
            }
        }

        [HttpPost("NextStage/{id}")]
        public async Task<ActionResult> NextStage(int id)
        {
            if (await deliveryService.NextStage(id))
            {
                return Ok();
            }
            else
            {
                logger.LogInformation("Ошибка при изменении стадии");
                return NotFound($"Заявка не существует");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Delivery>>> GetDeliveryByText(string text)
        {
            var delivery = await deliveryService.SearchDeliveriesByText(text);
            if (delivery == null)
            {
                return NotFound();
            }
            return Ok(delivery);
        }
    }

}

