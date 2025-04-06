using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    public class UpdateSaleRequestValidator : AbstractValidator<UpdateSaleRequest>
    {

        public UpdateSaleRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Sale ID is required");
            RuleFor(x => x.Branch).NotEmpty().WithMessage("Sale branch is required");

            RuleForEach(x => x.Items).SetValidator(new UpdateSaleItemRequestValidator());
        }
    }

    public class UpdateSaleItemRequestValidator : AbstractValidator<UpdateSaleItemRequest>
    {

        public UpdateSaleItemRequestValidator()
        {
            RuleFor(x => x.ProductId).GreaterThan(0).WithMessage("Product ID is required");
            RuleFor(x => x.ProductName).NotEmpty().WithMessage("Product name is required");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than 0");
            RuleFor(x => x.UnitPrice).Must(unitPrice => unitPrice > 0)
                .WithMessage("Unit price must be greater than 0");


        }
    }
}
