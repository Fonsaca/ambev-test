using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    public class SaleValidator : AbstractValidator<Sale>
    {


        public SaleValidator() { 
        
            RuleFor(sale => sale.Branch)
                .NotEmpty()
                .WithMessage("Branch must not be empty");

            RuleFor(sale => sale.CustomerId)
                .NotEmpty()
                .WithMessage("Customer id must not be empty");

            RuleFor(sale => sale.CustomerName)
                .NotEmpty()
                .WithMessage("Customer name must not be empty");

            RuleFor(sale => sale.Items)
                .NotEmpty()
                .WithMessage("The sale must have at least one item");

            RuleForEach(sale => sale.Items)
                .SetValidator(new SaleItemValidator());

            RuleFor(sale => sale)
                .Must(sale => !sale.IsCanceled || sale.TotalAmount > 0.0m)
                .WithMessage("Sale total amount was not calculated correctly");

        }
    }
}
