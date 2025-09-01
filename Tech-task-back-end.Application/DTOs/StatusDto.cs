using Tech_task_back_end.Domain.Enums;

namespace Tech_task_back_end.Application.DTOs;

public record StatusDto(
    Status Status,
    string StatusString
    );