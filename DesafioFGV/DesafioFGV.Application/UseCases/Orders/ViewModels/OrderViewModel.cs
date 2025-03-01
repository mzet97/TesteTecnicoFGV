using DesafioFGV.Application.UseCases.Users.ViewModels;
using DesafioFGV.Application.ViewModels;
using DesafioFGV.Domain.Entities;

namespace DesafioFGV.Application.UseCases.Orders.ViewModels;

public class OrderViewModel : BaseViewModel
{
    public string Description { get; set; }

    public decimal Value { get; set; }

    public DateTime DateOrder { get; set; }

    public Guid IdUser { get; set; }
    public UserViewModel User { get; set; }

    public OrderViewModel()
    {
        
    }


    public OrderViewModel(Order order)
    {
        Id = order.Id;
        Description = order.Description;
        Value = order.Value;
        DateOrder = order.DateOrder;
        IdUser = order.IdUser;
        CreatedAt = order.CreatedAt;
        UpdatedAt = order.UpdatedAt;
        DeletedAt = order.DeletedAt;
        IsDeleted = order.IsDeleted;

        if(order.User != null)
            User = new UserViewModel(order.User);
    }

    public OrderViewModel(string description, decimal value, DateTime dateOrder, Guid idUser)
    {
        Description = description;
        Value = value;
        DateOrder = dateOrder;
        IdUser = idUser;
    }

    public Order ToDomain()
    {
        return new Order(
            Guid.NewGuid(),
            Description,
            Value,
            DateOrder,
            IdUser,
            null,
            CreatedAt,
            UpdatedAt,
            DeletedAt,
            IsDeleted
        );
    }
}