using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    public class SaleValidator : AbstractValidator<Sale>
    {

        private const short MAX_IDENTICAL_ITEMS = 20;

        public SaleValidator() { 
        
            RuleFor(sale => sale.Branch).NotEmpty();

            RuleFor(sale => sale.CustomerId)
                .NotEmpty()
                .WithMessage("Customer id must not be empty");

            RuleFor(sale => sale.CustomerName)
                .NotEmpty()
                .WithMessage("Customer name must not be empty");

            RuleFor(sale => sale.Items)
                .NotEmpty()
                .WithMessage("The sale must have at least one item");

            RuleFor(sale => sale.Items.Where(i => !i.IsCanceled))
                .Must(items => items.All(items => items.Quantity <= MAX_IDENTICAL_ITEMS))
                .WithMessage($"The sale must have at most {MAX_IDENTICAL_ITEMS} identical items item");

            RuleForEach(sale => sale.Items)
                .SetValidator(new SaleItemValidator());

        }
    }
}
