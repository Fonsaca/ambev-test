using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class SaleItem : BaseEntity
    {

        /// <summary>
        /// Product Id
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Product Name
        /// </summary>
        public string ProductName { get; set; } = string.Empty;

        /// <summary>
        /// Quantity of items of this product in the sale
        /// </summary>
        public short Quantity { get; set; }

        /// <summary>
        /// Unit price of the product
        /// </summary>
        public decimal UnitPrice { get; set; }


        /// <summary>
        /// Total amount of this product in the sale
        /// </summary>
        public decimal TotalAmount
        {
            get { return Quantity * UnitPrice; }
        }


        /// <summary>
        /// Canceled status of an sale's item
        /// </summary>
        public virtual bool IsCanceled { get; protected set; } = false;


        /// <summary>
        /// Gets the date and time of the last update to the sale's item.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }


        /// <summary>
        /// Sale of the Item
        /// </summary>
        public Sale Sale { get; set; }



        /// <summary>
        /// Mark the item as canceled
        /// </summary>
        public void SetCanceled()
        {
            IsCanceled = true;
            UpdatedAt = DateTime.UtcNow;
        }

    }
}
