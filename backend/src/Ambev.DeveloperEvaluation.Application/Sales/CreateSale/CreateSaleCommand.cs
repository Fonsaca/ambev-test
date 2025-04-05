using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Command for creating a new sale.
/// </summary>
/// <remarks>
/// This command is used to capture the required data for creating a sale, 
/// including branch, customer ID, customer name, and sale items. 
/// It implements <see cref="IRequest{CreateSaleResult}"/> to initiate the request 
/// that returns a <see cref="CreateSaleResult"/>.
/// 
/// The data provided in this command is validated using the 
/// <see cref="CreateSaleCommandValidator"/> which extends 
/// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly 
/// populated and follow the required rules.
/// </remarks>
public class CreateSaleCommand : IRequest<CreateSaleResult>
{

    /// <summary>
    /// Gets or sets the branch of the sale to be created.
    /// </summary>
    public string Branch { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the customer ID for the sale.
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the customer name for the sale.
    /// </summary>
    public string CustomerName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the sale items for the sale.
    /// </summary>
    public List<CreateSaleItem> Items { get; set; } = new List<CreateSaleItem>();
}



/// <summary>
/// Sale Item data to create a sale 
/// </summary>
public class CreateSaleItem
{
    /// <summary>
    /// Gets or sets the product ID for the sale item.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// The name of the product
    /// </summary>
    public string ProductName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the quantity of the sale item.
    /// </summary>
    public short Quantity { get; set; }

    /// <summary>
    /// Gets or sets the unit price of the sale item.
    /// </summary>
    public decimal UnitPrice { get; set; }
}