namespace Tech_task_back_end.Application.DTOs;

public record PackageListDto(IEnumerable<PackagePreviewDto> Packages, int Count);