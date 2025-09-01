using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Tech_task_back_end.Application.DTOs;
using Tech_task_back_end.Application.Functions.Packages;
using Tech_task_back_end.Domain.Enums;

namespace Tech_task_back_end.Api;

[Route("api/Packages")]
[ApiController]
public class PackageController(IPackageRequestHandler packageRequestHandler, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PackageListDto>> GetPackages([FromQuery] int start,
        [FromQuery] int count, [FromQuery] string? trackingNumber, [FromQuery] Status? status)
    {
        var packages = await packageRequestHandler.GetPackagesAsync(start, count, trackingNumber, status);

        return Ok(packages);
    }

    [HttpGet("{trackingNumber}")]
    public async Task<ActionResult<PackageDetailsDto>> GetPackage(string trackingNumber)
    {
        var package = await packageRequestHandler.GetPackageDetailsAsync(trackingNumber);

        if (package == null)
        {
            return BadRequest();
        }

        return Ok(package);
    }

    [HttpGet("Types")]
    public async Task<ActionResult<IEnumerable<StatusDto>>> GetPackageTypes()
    {
        var statusTypes = Enum.GetValues<Status>();
        var mapped = mapper.Map<IEnumerable<StatusDto>>(statusTypes);
        return Ok(mapped);
    }

    [HttpPost]
    public async Task<ActionResult<string>> PostPackage(PackageCreateDto newPackage)
    {
        var result = await packageRequestHandler.AddPackageAsync(newPackage);
        if (result == null)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    [HttpPatch("{trackingNumber}")]
    public async Task<IActionResult> UpdatePackage(string trackingNumber, Status status)
    {
        var result = await packageRequestHandler.UpdatePackageStatusAsync(trackingNumber, status);

        if (!result.IsSuccess)
        {
            System.Diagnostics.Debug.WriteLine($"Failed to update package {trackingNumber}: {result.Message}");
            return BadRequest();
        }

        return Ok();
    }

    [HttpGet("{trackingNumber}/history")]
    public async Task<ActionResult<IEnumerable<StatusUpdateDto>>> GetPackageHistory(string trackingNumber)
    {
        var result = await packageRequestHandler.GetStatusHistoryAsync(trackingNumber);

        if (result == null)
        {
            return BadRequest();
        }
            
        return Ok(result);
    }
}