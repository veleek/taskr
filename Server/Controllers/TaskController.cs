using Microsoft.AspNetCore.Mvc;
using taskr.Shared;

namespace taskr.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class TaskController : ControllerBase
{
    private readonly Supabase.Client _client;
    private readonly ILogger<TaskController> _logger;

    public TaskController(Supabase.Client client, ILogger<TaskController> logger)
    {
        _client = client;
        _logger = logger;
    }

    [HttpGet]
    public async Task<string> Get()
    {
        return "ABC123";
    }
}
