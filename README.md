# GeekBurger
GeekBurger is a demo project for study purpose, contempling the activities 1 and 2 of homework. The project was developed with microsoft technologies like .NET, Visual Studio Tool and Azure Cloud Services.

## **Product Api Microservice (Activity 1)**
---

### **Introduction**
The ProductApi is a microservice responsible for managing products. It's possible execute read and write operations.

### **Demo screenshots**
- Service running on Azure Web app resource:</br>![Azure Web App overview screenshot](/docs/ProductApi-AzureWebApp.png)

- Listing all products by endpoint:</br>![Postman request and response screenshot](/docs/ProductApi-GetAll-Response.png)

- Sending messages to a service bus queue:</br>![Service bus queue overview screenshot](/docs/ProductApi-SendMessages-ServiceBusQueue.png)

### **Branch with application code**
Branch: https://github.com/fiap344514/GeekBurger/tree/feature/activity1_products-microservice

Commit: https://github.com/fiap344514/GeekBurger/commit/aaeb3a8533594997d40a4d63c850f750a1830e49

## **Catalog Sync Consumer Microservice (Activity 2)**
---

### **Introduction**
The CatalogSync is a microservice responsible for listening product changes events and update these informations in your context. Supported events are product addition, update, and deletion.

### **Demo screenshots**
- Messages ready to consumes in the queue:</br>![Service bus queue messages](/docs/ServiceBus-MessagesInQueue.png)

- Service consuming the messages and printing them in console:</br>![Service bus queue messages](/docs/Consumer-ServiceBus-ReceivingMessages-Log.png)

### **Branch with application code**
Branch: https://github.com/fiap344514/GeekBurger/tree/feature/activity2_sb-consumer-microservice

Commit: https://github.com/fiap344514/GeekBurger/commit/17b06149289381f1b1e14bb42324a3ed4778b39f
