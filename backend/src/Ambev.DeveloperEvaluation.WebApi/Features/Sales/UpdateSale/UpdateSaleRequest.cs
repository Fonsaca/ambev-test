namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    public class UpdateSaleRequest
    {

        public Guid Id { get; set; }

        public string Branch { get; set; }

        public List<UpdateSaleItemRequest> Items { get; set; }

    }

    public class UpdateSaleItemRequest
    {
        public Guid Id { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
