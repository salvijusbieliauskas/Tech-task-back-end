using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tech_task_back_end.Domain.Enums;

namespace Tech_task_back_end.Domain.Entities;

public record StatusUpdate
{
    public StatusUpdate(int Id,
        DateTime Date,
        Package Package,
        Status Status)
    {
        this.Id = Id;
        this.Date = Date;
        this.Package = Package;
        this.Status = Status;
    }

    public StatusUpdate()
    {
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; init; }

    public DateTime Date { get; init; }
    public Package Package { get; init; }
    public Status Status { get; init; }
}