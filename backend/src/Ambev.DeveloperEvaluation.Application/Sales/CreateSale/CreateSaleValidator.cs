using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleValidator : AbstractValidator<CreateSaleCommand>
{
    public CreateSaleValidator()
    {
        RuleFor(x => x.Branch).NotEmpty().WithMessage("Branch is required");
        RuleFor(x => x.CustomerId).NotEmpty().WithMessage("Customer ID is required");
        RuleFor(x => x.CustomerName).NotEmpty().WithMessage("Customer name is required");
        RuleFor(x => x.Items).NotEmpty().WithMessage("At least one sale item is required");

        RuleForEach(x => x.Items).SetValidator(new CreateSaleItemValidator());
    }
}

public class CreateSaleItemValidator : AbstractValidator<CreateSaleItem>
{
    public CreateSaleItemValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty().WithMessage("Product ID is required");
        RuleFor(x => x.ProductName).NotEmpty().WithMessage("Product name is required");
        RuleFor(x => x.Quantity).Must(qty => qty > 0).WithMessage("Quantity must be greater than 0");
        RuleFor(x => x.UnitPrice).GreaterThan(0).WithMessage("Unit price must be greater than 0");
    }
}