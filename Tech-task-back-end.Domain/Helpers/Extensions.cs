using Tech_task_back_end.Domain.Enums;

namespace Tech_task_back_end.Domain.Helpers;

public static class Extensions
{
    public static IEnumerable<Status> GetAllowedTransitions(this Status status)
    {
        return status switch
        {
            Status.Accepted => Array.Empty<Status>(),
            Status.Canceled => Array.Empty<Status>(),
            Status.Returned => new[] { Status.Sent, Status.Canceled },
            Status.Sent => new[] { Status.Accepted, Status.Returned, Status.Canceled },
            Status.Created => new[] { Status.Sent, Status.Canceled },
            _ => throw new NotImplementedException(),
        };
    }
}