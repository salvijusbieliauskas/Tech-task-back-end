using System.ComponentModel.DataAnnotations;

namespace Tech_task_back_end.Domain.Entities;

public record Client
{
    public Client(int Id,
        string Address,
        string Name,
        string Phone)
    {
        this.Id = Id;
        this.Address = Address;
        this.Name = Name;
        this.Phone = Phone;
    }

    public Client()
    {
    }

    [Key]
    public int Id { get; init; }
    public string Address { get; init; }
    public string Name { get; init; }
    public string Phone { get; init; }
}