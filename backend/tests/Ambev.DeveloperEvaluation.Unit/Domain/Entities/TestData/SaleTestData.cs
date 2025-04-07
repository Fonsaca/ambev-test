using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData
{
    internal static class SaleTestData
    {
        public static readonly Faker<Sale> SaleFaker = new Faker<Sale>()
              .RuleFor(u => u.Branch, f => f.Internet.Random.String(15))
              .RuleFor(u => u.CustomerId, f => f.Random.Guid())
              .RuleFor(u => u.CustomerName, f => f.Internet.UserName())
              .RuleFor(u => u.IsCanceled, f => false)
              .RuleFor(u => u.DiscountPercentage, f => 0.0m)
              .RuleFor(u => u.Id, f => f.Random.Guid())
              .RuleFor(u => u.SoldOn, f => DateTime.UtcNow)
              .RuleFor(u => u.TotalAmount, f => 0.0m)
              .RuleFor(u => u.Items, f => Enumerable.Range(0,2).Select(x => SaleItemFaker!.Generate()).ToList());


        public static readonly Faker<SaleItem> SaleItemFaker = new Faker<SaleItem>()
              .RuleFor(u => u.ProductId, f => f.Random.Number(1, 999))
              .RuleFor(u => u.ProductName, f => $"Product_{f.Random.Number(100, 999)}")
              .RuleFor(u => u.Id, f => f.Random.Guid())
              .RuleFor(u => u.IsCanceled, f => false)
              .RuleFor(u => u.Quantity, f => (short)f.Random.Number(1, 19))
              .RuleFor(u => u.UnitPrice, f => f.Random.Decimal(1, 15000));



        public static Sale GetSaleWithIdenticalItems(short quantity, decimal unitPrice)
        {
            var sale = SaleFaker
                .RuleFor(u => u.Items, f => Enumerable.Range(0, 1).Select(x => SaleItemFaker!.Generate()).ToList())
                .Generate();

            sale.Items[0].Quantity = quantity;
            sale.Items[0].UnitPrice = unitPrice;
            return sale;
        }
    }
}
