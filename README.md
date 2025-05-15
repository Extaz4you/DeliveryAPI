## **Web Api для обработки доставок**

### Описание

Данное Web API, разработанное с использованием ASP.NET Core, предназначено для управления процессами доставки.
API включает методы для добавления, изменения, удаления и редактирования заказов, а также для передачи их на следующую стадию.
Кроме того, предусмотрена возможность создания базы данных, если она еще не инициализирована.
Это API полностью подготовлено к развертыванию в Docker с поддержкой Microsoft SQL Server.

### Основные функции API

- AddNewDelivery(Delivery delivery) _Метод для добавления доставки.Входящий аргумент объект доставки._
- ChangeDelivery(Delivery delivery) _Метод для изменения доставки.Входящий аргумент объект доставки._
- NextStage(int id) _Метод для передечи на стадию Исполнение.Входящий аргумент ID доставки._
- RemoveNewDelivery(int id) _Метод для удаления доставки. Входящий аргумент ID доставки._
- SearchDeliveriesByText(string text) _Метод для поиска доставок по всем полям. Входящий аргумент текст._
- ShowAllDeliveries() _Метод для отображения всех доставок._
- CreateDb(context, logger) _Метод для создания БД, если она еще не создана._

### **Руководство по запуску**
**Перед запускам у вас должен быть установлен [Docker](https://www.docker.com/)**
- Скачать ахив
- Создать файл .env

**В файл поместить слеющие строки**

DB_USER=sa

DB_PASSWORD=Str0ngP@ssword!.

DB_NAME=DeliveryAPI

DB_PORT=1433

ASPNETCORE_ENVIRONMENT=Development

DefaultConnection=Server=sqlserver;TrustServerCertificate=True;Database=DevileryDb;User Id=sa;Password=Str0ngP@ssword!.

 
- Открыть консоль и прописать docker-compose up -d --build
- Дождаться загрузки, убедиться что оба контейнера работают. Перейти по http://localhost:3333/swagger и вызвать метод CreateDb()
  
