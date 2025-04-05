using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale
{

    public class DeleteSaleValidator : AbstractValidator<DeleteSaleCommand>
    {
        /// <summary>
        /// Initializes validation rules for DeleteUserCommand
        /// </summary>
        public DeleteSaleValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Sale ID is required");
        }
    }
}
