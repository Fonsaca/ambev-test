using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    public class SaleItemValidator : AbstractValidator<SaleItem>
    {

        public const short MAX_IDENTICAL_ITEMS = 20;

        public SaleItemValidator()
        {

            RuleFor(item => item.UnitPrice)
                .Must(price => price > 0.0m)
                .WithMessage("The item price must be a positive number");

            RuleFor(item => item.ProductId)
                .Must(productId => productId > 0)
                .WithMessage("Product id is invalid");

            RuleFor(item => item.ProductName)
                .NotEmpty()
                .WithMessage("Product name must be informed");

            RuleFor(item => item.Quantity)
                .Must(qty => qty > 0)
                .WithMessage("Item quantity must be a positive number");

            RuleFor(item => item.Quantity)
                .Must(qty => qty < MAX_IDENTICAL_ITEMS)
                .WithMessage($"The sale must have at most {MAX_IDENTICAL_ITEMS} identical items");

        }
    }
}
