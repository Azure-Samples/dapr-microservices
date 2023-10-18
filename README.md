# Azure Samples: Dapr Microservices

Welcome to the Azure Samples repository for Dapr Microservices! This repository contains a collection of sample microservices built to showcase best practices and patterns for developing distributed systems using [Dapr](https://dapr.io/).

## Overview

Dapr is an open-source, event-driven runtime that simplifies building microservices and distributed applications. These sample microservices demonstrate how to leverage Dapr's capabilities to address common concerns such as state management, pub/sub, service-to-service communication, and more.

## Getting Started

To get started with these Dapr microservices, follow the steps below:

1. **Prerequisites:**

   - Install [Dapr CLI](https://docs.dapr.io/getting-started/install-dapr-cli/).
   - Install [Docker](https://www.docker.com/get-started) for running Dapr and dependencies.
   - Make sure you have a compatible version of [.NET](https://dotnet.microsoft.com/download/dotnet) installed if you plan to run .NET-based microservices.

2. **Clone the Repository:**

   ```bash
   git clone https://github.com/Azure-Samples/Dapr-Microservices.git
   cd Dapr-Microservices
   ```

3. **Run Microservices:**

   Navigate to the specific microservice's directory and use the Dapr CLI to run them. For example, to run the Inventory Management microservice:

   ```bash
   cd inventory-management
   dapr run --app-id inventory-service --app-port 5050 -- dotnet run
   ```

   Each microservice may have specific instructions provided in its directory.

4. **Explore and Test:**

   Explore the sample microservices, their endpoints, and communication patterns. Use the provided API documentation or README files in each microservice directory to learn how to interact with them.

5. **Learn from Samples:**

   These samples showcase how to use Dapr's features like Pub/Sub, state management, service invocation, and more. Examine the code, configuration, and documentation to understand how to implement Dapr in your own microservices.

## Microservices

### Inventory Management

- **Description:** Manages product inventory and handles product orders.
- **Features:** Pub/Sub for ordering and returning materials, inventory management.
- **API:** [API Documentation](inventory-management/README.md)

## Contributing

See [CONTRIBUTING.md](CONTRIBUTING.md) for more information about contribution guidelines.