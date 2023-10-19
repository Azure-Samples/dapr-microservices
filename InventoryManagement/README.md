# Inventory Management Microservice with Dapr

This project is an Inventory Management microservice built using C# .NET and Dapr, a distributed application runtime. It provides a RESTful API for managing product inventory and handling product orders through Dapr's Pub/Sub capabilities. It uses an sqlite database.

## Getting Started

### Prerequisites

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Dapr CLI](https://docs.dapr.io/getting-started/install-dapr-cli/)
- Docker (for running Dapr and dependencies)

### Installation

1. Clone this repository.

2. Build and run the microservice using Dapr:

```bash
dapr run --app-id inventory-service --app-port 5050 -- dotnet run
```

3. Your microservice should now be running and listening on `http://localhost:5050`.

## Overview

This microservice is designed to manage product inventory and handle product orders through Dapr's Pub/Sub functionality. It consists of two main controllers: `ProductsController` for managing individual products and `ProductsOrderController` for handling product orders.

## Dapr

[Dapr](https://dapr.io/) is a runtime that simplifies the development of microservices by providing a set of building blocks for common concerns, such as state management, pub/sub, and service-to-service communication. This microservice utilizes Dapr for pub/sub and other Dapr features.

## Pub/Sub

This microservice uses Dapr's Pub/Sub feature to enable communication between different parts of the system. Pub/Sub allows events to be published and subscribed to by different services, enabling decoupled and scalable communication.

### Supported Pub/Sub Topics

- **Request Materials**: Use the `RequestMaterials` endpoint to request materials, and this topic is triggered when an order is placed.
- **Return Materials**: Use the `ReturnMaterials` endpoint to return materials, and this topic is triggered when materials are returned.

## API Endpoints

### ProductsController

- `POST /products`: Create a new product in the inventory.
- `PUT /products/{sku}`: Update an existing product by SKU.
- `DELETE /products/{sku}`: Delete a product by SKU.

### ProductsOrderController

- `POST /productsorder`: Request materials by placing an order.
- `PUT /productsorder`: Return materials to the inventory.

## Samples

### Create a Product

To create a new product in the inventory, make a POST request to `/products`. Provide the product details in the request body.

```http
POST http://localhost:5050/products
Content-Type: application/json

{
    "sku": "ABC123",
    "name": "Product Name",
    "qty": 100
}
```

### Place an Order

To place an order and request materials, make a POST request to `/productsorder/request`. Provide the order details in the request body.

```http
POST http://localhost:5050/productsorder
Content-Type: application/json

{
    "id": "order123",
    "orderDetails": {
        "ABC123": 50,
        "XYZ789": 30
    }
}
```

### Return Materials

To return materials to the inventory, make a PUT request to `/productsorder` with the `inventoryRequestId` parameter.

```http
PUT http://localhost:5050/productsorder?inventoryRequestId=your_request_id
```
