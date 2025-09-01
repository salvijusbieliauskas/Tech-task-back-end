using MapsterMapper;
using Tech_task_back_end.Application.DTOs;
using Tech_task_back_end.Application.Functions.Packages;
using Tech_task_back_end.Application.Repositories;
using Tech_task_back_end.Domain.Entities;
using Tech_task_back_end.Domain.Enums;
using Tech_task_back_end.Domain.Helpers;
using Tech_task_back_end.Domain.Helpers.Wrappers;

namespace Tech_task_back_end.Application.Functions.Packagse;

public class PackageRequestHandler(
    IStatusUpdateRepository statusRepository,
    IPackageRepository packageRepository,
    IClientRepository clientRepository,
    IMapper mapper)
    : IPackageRequestHandler
{
    public async Task<string?> AddPackageAsync(PackageCreateDto packageDto)
    {
        var recipient = await clientRepository.GetByIdAsync(packageDto.RecipientId);
        var sender = await clientRepository.GetByIdAsync(packageDto.SenderId);

        if (sender == null || recipient == null)
        {
            return null;
        }

        if (recipient.Id == sender.Id)
        {
            return null;
        }

        var packages = await packageRepository.GetAllAsync();
        int id;
        if (packages.Any())
        {
            id = packages.MaxBy(a => a.Id).Id + 1;
        }
        else
        {
            id = 1;
        }

        string trackingNumber = TrackingNumber.Create(id);
        DateTime createdAt = DateTime.UtcNow;

        Package package = new Package(id, trackingNumber, Status.Created, recipient, sender,
            createdAt);

        var updated = await packageRepository.AddAsync(package);

        if (updated == null)
        {
            return null;
        }

        var statusUpdates = await statusRepository.GetAllAsync();

        int statusId;

        if (statusUpdates.Any())
        {
            statusId = statusUpdates.MaxBy(a => a.Id).Id + 1;
        }
        else
        {
            statusId = 1;
        }

        var statusLogSuccess =
            await statusRepository.AddAsync(new StatusUpdate(statusId, createdAt, updated, Status.Created));

        if (!statusLogSuccess)
        {
            System.Diagnostics.Debug.WriteLine("Could not register new package status");
        }

        return trackingNumber;
    }

    public async Task<Result> UpdatePackageStatusAsync(string trackingNumber, Status newStatus)
    {
        Package? package = await packageRepository.GetByTrackingNumberAsync(trackingNumber);

        if (package == null)
        {
            return Result.Err("Package not found");
        }

        if (!package.Status.GetAllowedTransitions().Contains(newStatus))
        {
            return Result.Err($"Transition from {package.Status} to {newStatus} is not valid");
        }

        var statuses = await statusRepository.GetAllAsync();

        int statusId;

        if (statuses.Any())
        {
            statusId = statuses.MaxBy(a => a.Id).Id + 1;
        }
        else
        {
            statusId = 1;
        }

        StatusUpdate update = new StatusUpdate(statusId, DateTime.UtcNow, package, newStatus);

        if (!await statusRepository.AddAsync(update))
        {
            return Result.Err($"Failed to update status");
        }

        package.Status = newStatus;

        if (!await packageRepository.UpdateAsync(package))
        {
            return Result.Err($"Failed to update package");
        }

        return Result.Ok();
    }

    public async Task<PackageListDto> GetPackagesAsync(int startFrom, int count, string? trackingNumber,
        Status? status)
    {
        if (startFrom < 0 || count <= 0)
        {
            return new PackageListDto([], 0);
        }

        var allPackages = await packageRepository.GetAllAsync();
        if (!string.IsNullOrWhiteSpace(trackingNumber))
        {
            allPackages = allPackages.Where(a =>
                a.TrackingNumber.Contains(trackingNumber, StringComparison.InvariantCultureIgnoreCase));
        }

        if (status != null)
        {
            allPackages = allPackages.Where(a => a.Status.Equals(status));
        }

        int totalCount = allPackages.Count();

        var packages = allPackages.Take(new Range(startFrom, startFrom + count))
            .Select(mapper.Map<PackagePreviewDto>);

        return new PackageListDto(packages, totalCount);
    }

    public async Task<int> GetPackageCountAsync()
    {
        var allPackages = await packageRepository.GetAllAsync();
        return allPackages.Count();
    }

    public async Task<PackageDetailsDto?> GetPackageDetailsAsync(string trackingNumber)
    {
        var package = await packageRepository.GetByTrackingNumberAsync(trackingNumber);

        if (package == null)
        {
            return null;
        }

        var mapped = mapper.Map<PackageDetailsDto>(package);

        var latestUpdate = (await statusRepository.GetAllAsync()).Where(a => a.Package.Id.Equals(package.Id))
            .OrderBy(a => a.Date).Last();
        
        mapped.LatestUpdate = mapper.Map<StatusUpdateDto>(latestUpdate);

        return mapped;
    }

    public async Task<IEnumerable<StatusUpdateDto>?> GetStatusHistoryAsync(string trackingNumber)
    {
        var statusUpdates = await statusRepository.GetAllAsync();
        var package = await packageRepository.GetByTrackingNumberAsync(trackingNumber);

        if (package == null)
        {
            return null;
        }

        return statusUpdates.Where(a => a.Package.Id.Equals(package.Id)).Select(mapper.Map<StatusUpdateDto>);
    }
}