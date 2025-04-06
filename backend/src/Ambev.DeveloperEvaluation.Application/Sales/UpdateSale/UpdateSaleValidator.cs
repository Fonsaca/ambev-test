
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleValidator : AbstractValidator<UpdateSaleCommand>
    {
        /// <summary>
        /// Initializes validation rules for DeleteUserCommand
        /// </summary>
        public UpdateSaleValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Sale ID is required");
            RuleFor(x => x.Branch).NotEmpty().WithMessage("Sale branch is required");

            RuleForEach(x => x.Items).SetValidator(new UpdateSaleItemValidator());
        }
    }

    public class UpdateSaleItemValidator : AbstractValidator<UpdateSaleItemCommand>
    {

        public UpdateSaleItemValidator()
        {
            RuleFor(x => x.ProductId).GreaterThan(0).WithMessage("Product ID is required");
            RuleFor(x => x.ProductName).NotEmpty().WithMessage("Product name is required");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than 0");
            RuleFor(x => x.UnitPrice).Must(unitPrice => unitPrice > 0)
                .WithMessage("Unit price must be greater than 0");


        }
    }
}
