using Tech_task_back_end.Application.DTOs;
using Tech_task_back_end.Domain.Enums;
using Tech_task_back_end.Domain.Helpers.Wrappers;

namespace Tech_task_back_end.Application.Functions.Packages;

public interface IPackageRequestHandler
{
    Task<string?> AddPackageAsync(PackageCreateDto package);
    Task<Result> UpdatePackageStatusAsync(string trackingNumber, Status newStatus);

    Task<PackageListDto> GetPackagesAsync(int startFrom, int count, string? trackingNumber,
        Status? status);

    Task<int> GetPackageCountAsync();
    Task<PackageDetailsDto?> GetPackageDetailsAsync(string trackingNumber);
    Task<IEnumerable<StatusUpdateDto>?> GetStatusHistoryAsync(string trackingNumber);
}