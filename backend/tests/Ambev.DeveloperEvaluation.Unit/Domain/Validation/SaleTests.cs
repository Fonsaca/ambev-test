using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation
{
    public class SaleTests
    {
        private readonly SaleValidator _validator;

        public SaleTests()
        {
            _validator = new SaleValidator();
        }

        [Fact(DisplayName = "Validation should fail if sale customer id is empty")]
        public void Given_InvalidCustomerId_When_Validated_Then_ShouldHaveError()
        {
            // Arrange
            var sale = SaleTestData.SaleFaker.Generate();
            sale.CustomerId = Guid.Empty;

            // Act
            var result = _validator.TestValidate(sale);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.CustomerId);
        }

        [Fact(DisplayName = "Validation should fail if sale doesn't have any item")]
        public void Given_EmptyListOfItems_When_Validated_Then_ShouldHaveError()
        {
            // Arrange
            var sale = SaleTestData.SaleFaker.Generate();
            sale.Items.Clear();

            // Act
            var result = _validator.TestValidate(sale);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Items);
        }


        [Theory(DisplayName = "Total amount must be correct for identical items")]
        [InlineData(2,10.0,20)]
        [InlineData(5,10.0,45)]
        [InlineData(10,10.0,80)]
        public void Given_IdenticalItems_When_SetAmount_Then_ShouldHaveCorrectCalculation(short quantity, decimal unitPrice, decimal totalAmount)
        {
            // Arrange
            var sale = SaleTestData.GetSaleWithIdenticalItems(quantity,unitPrice);

            // Act
            sale.SetTotalAmount();

            // Assert
            Assert.Equal(sale.TotalAmount, totalAmount, 0);
        }
    }
}
