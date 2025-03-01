using DesafioFGV.Domain.Identities;
using DesafioFGV.Domain.Shared;

namespace DesafioFGV.Domain.Entities;

public class Order : Entity
{
    public string Description { get; set; }
    public decimal Value { get; set; }
    public DateTime DateOrder { get; set; }

    public Guid IdUser { get; set; }
    public ApplicationUser User { get; set; }

    public Order() : base()
    {

    }

    public Order(
        Guid Id,
        string description,
        decimal value,
        DateTime dateOrder,
        Guid idUser,
        ApplicationUser user,
        DateTime createdAt,
        DateTime? updatedAt,
        DateTime? deletedAt,
        bool isDeleted
        ) : base(Id, createdAt, updatedAt, deletedAt, isDeleted)
    {
        Description = description;
        Value = value;
        DateOrder = dateOrder;
        IdUser = idUser;
        User = user;
    }
}
