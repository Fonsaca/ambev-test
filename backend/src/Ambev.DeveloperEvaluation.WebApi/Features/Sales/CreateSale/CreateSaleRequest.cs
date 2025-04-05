
namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleRequest
{
    public string Branch { get; set; }
    public string CustomerId { get; set; }
    public string CustomerName { get; set; }
    public List<CreateSaleItemRequest> Items { get; set; }
}

public class CreateSaleItemRequest
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}