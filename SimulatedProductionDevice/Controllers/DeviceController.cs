using Dapr;
using Dapr.Client;
using SimulatedProductionDevice.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SimulatedProductionDevice.Controllers;

[ApiController]
[Route("[controller]")]
public class DeviceController : ControllerBase
{
    private readonly ILogger<DeviceController> _logger;

    private readonly DaprClient _daprClient;

    public const string PubSubName = "/device/#commands/";

    public DeviceController(ILogger<DeviceController> logger, DaprClient daprClient)
    {
        _logger = logger;
        _daprClient = daprClient;
    }

    [HttpPost(Name = "StartProcess")]
    public async Task<ActionResult<ProcessDto>> Post([FromBody] ProcessDto processDto)
    {
        await _daprClient.PublishEventAsync($"{PubSubName}/{processDto.DeviceId!}", "start", processDto);

        // wait randomly between 1 and 5 seconds
        await Task.Delay(new Random().Next(1000, 5000));
        
        if (processDto.Command!.Equals("beer"))
        {
            await _daprClient.PublishEventAsync($"{PubSubName}/{processDto.DeviceId!}", "failed", processDto);
        }
        else
        {
            await _daprClient.PublishEventAsync($"{PubSubName}/{processDto.DeviceId!}", "completed", processDto);
        }

        return processDto;
    }
}
