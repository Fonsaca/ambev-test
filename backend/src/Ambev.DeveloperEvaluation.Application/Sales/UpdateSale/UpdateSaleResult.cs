namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleResult
    {
        /// <summary>
        /// Sale Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Branch where the sale was made
        /// </summary>
        public string Branch { get; set; }

        /// <summary>
        /// Customer ID
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// Customer Name
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// Date the sale was made
        /// </summary>
        public DateTime SoldOn { get; set; }

        /// <summary>
        /// Discount percentage of the sale
        /// </summary>
        public decimal DiscountPercentage { get; set; }

        /// <summary>
        /// Total amount after discount
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Sale items
        /// </summary>
        public List<UpdateSaleItemResult> Items { get; set; }

    }

    public class UpdateSaleItemResult
    {

        public Guid Id { get; set; }

        /// <summary>
        /// Item product ID
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Item product name
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Item quantity
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Item unit price
        /// </summary>
        public decimal UnitPrice { get; set; }

    }
}
