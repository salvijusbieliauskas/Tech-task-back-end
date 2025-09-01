using Tech_task_back_end.Domain.Entities;
using Tech_task_back_end.Domain.Enums;

namespace Tech_task_back_end.Application.DTOs;

public class PackageDetailsDto
{
    public PackageDetailsDto(string trackingNumber,
        Client sender,
        Client recipient,
        DateTime created,
        StatusUpdateDto latestUpdate,
        IEnumerable<StatusDto> allowedTransitions)
    {
        TrackingNumber = trackingNumber;
        Sender = sender;
        Recipient = recipient;
        Created = created;
        LatestUpdate = latestUpdate;
        AllowedTransitions = allowedTransitions;
    }

    public PackageDetailsDto()
    {
    }

    public string TrackingNumber { get; set; }
    public Client Sender { get; set; }
    public Client Recipient { get; set; }
    public DateTime Created { get; set; }
    public StatusUpdateDto LatestUpdate { get; set; }
    public IEnumerable<StatusDto> AllowedTransitions { get; set; }
}