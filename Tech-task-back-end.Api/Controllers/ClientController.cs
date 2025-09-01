using Microsoft.AspNetCore.Mvc;
using Tech_task_back_end.Application.DTOs;
using Tech_task_back_end.Application.Functions.Clients;
using Tech_task_back_end.Domain.Entities;

namespace Tech_task_back_end.Api;

[Route("api/Clients")]
[ApiController]
public class ClientController(IClientRequestHandler clientRequestHandler) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Client>>> GetClients()
    {
        return Ok(await clientRequestHandler.GetClientsAsync());
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateClient(ClientCreateDto client)
    {
        var newId = await clientRequestHandler.AddClientAsync(client);

        if (newId < 0)
        {
            return BadRequest();
        }
        
        return Ok(newId);
    }
}