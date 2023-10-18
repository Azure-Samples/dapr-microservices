# Simulated Production Device Microservice

The Simulated Production Device Microservice is a sample microservice designed to simulate a production line device equipment using Dapr. This microservice communicates with other services through Dapr's Pub/Sub feature, allowing you to model and test various production processes.

## Overview

This microservice simulates a production device that can start different processes. It demonstrates how Dapr's Pub/Sub functionality can be used to publish events related to device actions and their outcomes. It also showcases how to introduce random delays to simulate real-world scenarios.

## Getting Started

To get started with the Simulated Production Device Microservice, follow the steps below:

1. **Prerequisites:**

   - Install [Dapr CLI](https://docs.dapr.io/getting-started/install-dapr-cli/).
   - Make sure you have a compatible version of [.NET](https://dotnet.microsoft.com/download/dotnet) installed if you plan to run .NET-based microservices.

2. **Clone the Repository:**

   ```bash
   git clone https://github.com/YourOrganization/Simulated-Production-Device.git
   cd Simulated-Production-Device
   ```

3. **Run the Microservice:**

   Use the Dapr CLI to run the microservice:

   ```bash
   dapr run --app-id simulated-device --app-port 5050 -- dotnet run
   ```

4. **Interact with the Device:**

   You can interact with the simulated device by making POST requests to the device's API endpoint to start different processes. The device will randomly delay between 1 and 5 seconds to simulate processing.

   Example POST request to start a process:

   ```http
   POST http://localhost:5050/device
   Content-Type: application/json

   {
       "deviceId": "device123",
       "command": "coffee"
   }
   ```

   The device may respond with "completed" or "failed" events based on the command provided. For now if the command is "beer", will publish an "failed" event, otherwise 
   will publish a "completed" event.

## API Endpoints

### DeviceController

- `POST /device`: Start a production process on the simulated device. It publishes events related to the process using Dapr's Pub/Sub feature.