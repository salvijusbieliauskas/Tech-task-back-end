using System.ComponentModel.DataAnnotations;
using Tech_task_back_end.Domain.Enums;

namespace Tech_task_back_end.Domain.Entities;

public record Package
{
    public Package(int Id,
        string TrackingNumber,
        Status Status,
        Client Recipient,
        Client Sender,
        DateTime Created)
    {
        this.Id = Id;
        this.TrackingNumber = TrackingNumber;
        this.Status = Status;
        this.Recipient = Recipient;
        this.Sender = Sender;
        this.Created = Created;
    }

    public Package()
    {
    }

    [Key] public int Id { get; init; }

    public string TrackingNumber { get; init; }
    public Status Status { get; set; }
    public Client Recipient { get; init; }
    public Client Sender { get; init; }
    public DateTime Created { get; init; }
}