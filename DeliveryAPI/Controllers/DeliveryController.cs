using DeliveryAPI.Data;
using DeliveryAPI.Interfaces;
using DeliveryAPI.Models;
using DeliveryAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        /// <summary>
        /// Добавляет новую доставку.
        /// </summary>
        /// <param name="delivery">Объект доставки для добавления.</param>
        /// <returns>Идентификатор добавленной доставки.</returns>
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
        /// <summary>
        /// Получает все доставки.
        /// </summary>
        /// <returns>Список всех доставок.</returns>
        [HttpGet("ShowAllDeliveries")]
        public async Task<ActionResult<List<Delivery>>> ShowAllDeliveries()
        {
            var deliveries = await deliveryService.ShowAllDeliveries();
            return Ok(deliveries);
        }

        /// <summary>
        /// Обновляет данные конкретной доставки.
        /// </summary>
        /// <param name="delivery">Объект доставки с обновленными данными.</param>
        /// <returns>Идентификатор обновленной доставки.</returns>
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

        /// <summary>
        /// Удаляет конкретную доставку по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор доставки для удаления.</param>
        /// <returns>Результат операции удаления.</returns>
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

        /// <summary>
        /// Переходит к следующей стадии обработки доставки.
        /// </summary>
        /// <param name="id">Идентификатор доставки.</param>
        /// <returns>Результат операции.</returns>
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

        /// <summary>
        /// Получает доставку по текстовому запросу.
        /// </summary>
        /// <param name="text">Текст для поиска по доставкам.</param>
        /// <returns>Список найденных доставок.</returns>
        [HttpGet("GetDeliveryByText/{text}")]
        public async Task<ActionResult<List<Delivery>>> GetDeliveryByText(string text)
        {
            var delivery = await deliveryService.SearchDeliveriesByText(text);
            if (delivery == null)
            {
                return NotFound();
            }
            return Ok(delivery);
        }
        /// <summary>
        /// Создает базу данных, если она еще не существует.
        /// </summary>
        /// <param name="context">Контекст базы данных.</param>
        /// <param name="logger">Логгер для записи информации.</param>
        [HttpGet]
        public void CreateDb([FromServices] DeliveryContext context, [FromServices] ILogger<DeliveryController> logger)
        {
            logger.LogInformation(context.Database.GetConnectionString());
            context.Database.EnsureCreated();
        }
    }

}

