using DesafioFGV.Domain.Entities.Validations;
using DesafioFGV.Domain.Test.Fakes;

namespace DesafioFGV.Domain.Test.Validations;

public class OrderValidationTest
{
    [Fact(DisplayName = "Test order description validation")]
    [Trait("OrderValidation", "order description")]
    public void OrderValidation_WhenDescriptionIsEmpty_ShouldReturnError()
    {
        // Arrange
        var order = new OrderFaker().CreateValid(1).First();
        order.Description = "";
        var validator = new OrderValidation();
        // Act
        var result = validator.Validate(order);
        // Assert
        Assert.False(result.IsValid);
        Assert.Equal("Description is required.", result.Errors[0].ErrorMessage);
    }

    [Fact(DisplayName = "Test order description validation success")]
    [Trait("OrderValidation", "order description")]
    public void OrderValidation_WhenDescriptionIsValid_ShouldNotReturnError()
    {
        // Arrange
        var order = new OrderFaker().CreateValid(1).First();
        order.Description = "Valid description";
        var validator = new OrderValidation();
        // Act
        var result = validator.Validate(order);
        // Assert
        Assert.True(result.IsValid);
    }

    [Fact(DisplayName = "Test order user validation")]
    [Trait("OrderValidation", "order user")]
    public void OrderValidation_WhenUserIsEmpty_ShouldReturnError()
    {
        // Arrange
        var order = new OrderFaker().CreateValid(1).First();
        order.IdUser = Guid.Empty;
        var validator = new OrderValidation();
        // Act
        var result = validator.Validate(order);
        // Assert
        Assert.False(result.IsValid);
        Assert.Equal("User is required.", result.Errors[0].ErrorMessage);
    }

    [Fact(DisplayName = "Test order user validation success")]
    [Trait("OrderValidation", "order user")]
    public void OrderValidation_WhenUserIsValid_ShouldNotReturnError()
    {
        // Arrange
        var order = new OrderFaker().CreateValid(1).First();
        order.IdUser = Guid.NewGuid();
        var validator = new OrderValidation();
        // Act
        var result = validator.Validate(order);
        // Assert
        Assert.True(result.IsValid);
    }

    [Fact(DisplayName = "Test order value validation")]
    [Trait("OrderValidation", "order value")]
    public void OrderValidation_WhenValueIsZeroOrLess_ShouldReturnError()
    {
        // Arrange
        var order = new OrderFaker().CreateValid(1).First();
        order.Value = 0;
        var validator = new OrderValidation();
        // Act
        var result = validator.Validate(order);
        // Assert
        Assert.False(result.IsValid);
        Assert.Equal("Value must be greater than zero.", result.Errors[0].ErrorMessage);
    }

    [Fact(DisplayName = "Test order value validation success")]
    [Trait("OrderValidation", "order value")]
    public void OrderValidation_WhenValueIsValid_ShouldNotReturnError()
    {
        // Arrange
        var order = new OrderFaker().CreateValid(1).First();
        order.Value = 100;
        var validator = new OrderValidation();
        // Act
        var result = validator.Validate(order);
        // Assert
        Assert.True(result.IsValid);
    }

    [Fact(DisplayName = "Test order date validation")]
    [Trait("OrderValidation", "order date")]
    public void OrderValidation_WhenDateOrderIsEmpty_ShouldReturnError()
    {
        // Arrange
        var order = new OrderFaker().CreateValid(1).First();
        order.DateOrder = default;
        var validator = new OrderValidation();
        // Act
        var result = validator.Validate(order);
        // Assert
        Assert.False(result.IsValid);
        Assert.Equal("Order date is required.", result.Errors[0].ErrorMessage);
    }

    [Fact(DisplayName = "Test order date validation success")]
    [Trait("OrderValidation", "order date")]
    public void OrderValidation_WhenDateOrderIsValid_ShouldNotReturnError()
    {
        // Arrange
        var order = new OrderFaker().CreateValid(1).First();
        order.DateOrder = DateTime.Now;
        var validator = new OrderValidation();
        // Act
        var result = validator.Validate(order);
        // Assert
        Assert.True(result.IsValid);
    }

    [Fact(DisplayName = "Test order date validation out of range")]
    [Trait("OrderValidation", "order date")]
    public void OrderValidation_WhenDateOrderIsOutOfRange_ShouldReturnError()
    {
        // Arrange
        var order = new OrderFaker().CreateValid(1).First();
        order.DateOrder = new DateTime(1800, 1, 1);
        var validator = new OrderValidation();
        // Act
        var result = validator.Validate(order);
        // Assert
        Assert.False(result.IsValid);
        Assert.Equal("Order date must be between 01/01/1900 and 31/12/3000.", result.Errors[0].ErrorMessage);

        // Arrange
        order.DateOrder = new DateTime(3100, 1, 1);
        // Act
        result = validator.Validate(order);
        // Assert
        Assert.False(result.IsValid);
        Assert.Equal("Order date must be between 01/01/1900 and 31/12/3000.", result.Errors[0].ErrorMessage);
    }

    [Fact(DisplayName = "Test order date validation within range")]
    [Trait("OrderValidation", "order date")]
    public void OrderValidation_WhenDateOrderIsWithinRange_ShouldNotReturnError()
    {
        // Arrange
        var order = new OrderFaker().CreateValid(1).First();
        order.DateOrder = new DateTime(2000, 1, 1);
        var validator = new OrderValidation();
        // Act
        var result = validator.Validate(order);
        // Assert
        Assert.True(result.IsValid);
    }
}
