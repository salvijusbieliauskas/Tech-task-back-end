using Tech_task_back_end.Domain.Enums;

namespace Tech_task_back_end.Application.DTOs;

public record PackagePreviewDto(
    string TrackingNumber,
    StatusDto Status,
    string SenderName,
    string RecipientName,
    DateTime Created,
    IEnumerable<StatusDto> AllowedTransitions
);