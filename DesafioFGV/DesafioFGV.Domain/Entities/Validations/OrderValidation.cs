using FluentValidation;

namespace DesafioFGV.Domain.Entities.Validations;

public class OrderValidation : AbstractValidator<Order>
{
    public OrderValidation()
    {
        RuleFor(order => order.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

        RuleFor(order => order.IdUser)
            .NotEmpty().WithMessage("User is required.");

        RuleFor(order => order.Value)
            .GreaterThan(0).WithMessage("Value must be greater than zero.")
            .NotEmpty().WithMessage("Value is required.");

        RuleFor(order => order.DateOrder)
            .NotEmpty().WithMessage("Order date is required.")
            .Must(date => date >= new DateTime(1900, 1, 1) && date <= new DateTime(3000, 12, 31))
            .WithMessage("Order date must be between 01/01/1900 and 31/12/3000.");
    }
}
