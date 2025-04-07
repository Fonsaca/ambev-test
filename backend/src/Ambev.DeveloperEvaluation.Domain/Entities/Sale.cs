using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale : BaseEntity
    {

        /// <summary>
        /// Date when the sale was made
        /// </summary>
        public DateTime SoldOn { get; set; }

        /// <summary>
        /// The customer id that bought the products
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// The customer name that bought the products
        /// </summary>
        public string CustomerName { get; set; } = string.Empty;

        /// <summary>
        /// Company branch where the sale was made
        /// </summary>
        public string Branch { get; set; } = string.Empty;

        /// <summary>
        /// The items of the sale
        /// </summary>
        public List<SaleItem> Items { get; set; } = new List<SaleItem>();


        /// <summary>
        /// Discount percentage
        /// 10 or more items = 20% discount
        /// more than 4 items = 10% discount
        /// </summary>
        public decimal DiscountPercentage { get; set; }


        /// <summary>
        /// Total amount of the sale
        /// </summary>
        public decimal TotalAmount { get; set; }


        /// <summary>
        /// Canceled status of a sale
        /// </summary>
        public virtual bool IsCanceled { get; protected set; } = false;


        /// <summary>
        /// Gets the date and time of the last update to the sale's information.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }


        public Sale()
        {
            SoldOn = DateTime.UtcNow;
        }


        /// <summary>
        /// Mark the sale as canceled
        /// </summary>
        public void SetCanceled()
        {
            IsCanceled = true;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Set new entity properties values
        /// </summary>
        /// <param name="updateSale">Sale entity with new values</param>
        public void Update(Sale updateSale)
        {
            this.Branch = updateSale.Branch;
            this.UpdateItems(updateSale.Items);
            this.UpdatedAt = DateTime.UtcNow;

            if (Items.TrueForAll(i => i.IsCanceled))
            {
                this.SetCanceled();
            }

            this.SetTotalAmount();
        }

        /// <summary>
        /// Update sale items
        /// - When the item has an increase of quantity then the existing item quantity is also increased
        /// - When the item has a decrease of quantity then the difference quantity is set as canceled and a new entity is insert in the list
        /// - When it is a new item then the item is added to the list of items.
        /// - Updates the UpdateAt date
        /// </summary>
        /// <param name="items">New list of items</param>
        private void UpdateItems(List<SaleItem> items)
        {
            items.ForEach(UpdateItem);

            var itemsToCancel = Items
                .Where(i => !i.IsCanceled)
                .Where(i => !items.Exists(x => x.ProductId == i.ProductId));

            foreach (var itemToCancel in itemsToCancel)
            {
                itemToCancel.SetCanceled();
            }

        }

        /// <summary>
        /// Update a sale item
        /// - When the item has an increase of quantity then the existing item quantity is also increased
        /// - When the item has a decrease of quantity then the difference quantity is set as canceled and a new entity is insert in the list
        /// - When it is a new item then the item is added to the list of items.
        /// - Updates the UpdateAt date
        /// </summary>
        /// <param name="items">New item</param>
        private void UpdateItem(SaleItem item)
        {
            var existingItem = Items.FirstOrDefault(i => i.ProductId == item.ProductId && !i.IsCanceled);

            if (existingItem == null)
            {
                Items.Add(item);
                return;
            }

            existingItem.ProductName = item.ProductName;
            existingItem.UnitPrice = item.UnitPrice;
            existingItem.Quantity = item.Quantity;

            if (item.Quantity < existingItem.Quantity)
            {
                existingItem.Quantity -= item.Quantity;
                existingItem.SetCanceled();

                if (item.Quantity > 0)
                    Items.Add(item);
            }
            else if (item.Quantity > existingItem.Quantity)
            {
                existingItem.Quantity = item.Quantity;
                existingItem.UpdatedAt = DateTime.UtcNow;
            }

        }

        /// <summary>
        /// Set total amount
        /// Total amount of items not canceled minus the discount
        /// </summary>
        public void SetTotalAmount()
        {
            this.SetDiscountPercentage();

            var totalAmountBeforeDiscount = Items
                    .Where(item => !item.IsCanceled)
                    .Sum(o => o.TotalAmount);

            TotalAmount = totalAmountBeforeDiscount * (1.0m - DiscountPercentage);
        }

        /// <summary>
        /// Set Discount percentage
        /// 10 or more items = 20% discount
        /// more than 4 items = 10% discount
        /// </summary>
        public void SetDiscountPercentage()
        {
            var maxItemQty = Items
                    .Where(item => !item.IsCanceled)
                    .Max(item => item.Quantity);

            decimal discount = 0.0m;

            if (maxItemQty >= 10)
                discount = 0.2m;
            else if (maxItemQty > 4)
                discount = 0.1m;

            DiscountPercentage = discount;
        }

        

    }
}
