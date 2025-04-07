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
    public class SaleItemTests
    {
        private readonly SaleItemValidator _validator;

        public SaleItemTests()
        {
            _validator = new SaleItemValidator();
        }

        [Fact(DisplayName = "Validation should fail if sale item product id is not a positive number")]
        public void Given_InvalidProductId_When_Validated_Then_ShouldHaveError()
        {
            // Arrange
            var item = SaleTestData.SaleItemFaker.Generate();
            item.ProductId = 0;

            // Act
            var result = _validator.TestValidate(item);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.ProductId);
        }

        [Fact(DisplayName = "Validation should fail if sale item unit price is zero")]
        public void Given_InvalidUnitPrice_When_Validated_Then_ShouldHaveError()
        {
            // Arrange
            var item = SaleTestData.SaleItemFaker.Generate();
            item.UnitPrice = 0.0m;

            // Act
            var result = _validator.TestValidate(item);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.UnitPrice);
        }

        [Fact(DisplayName = "Validation should fail if sale item quantity is zero")]
        public void Given_InvalidQuantity_When_Validated_Then_ShouldHaveError()
        {
            // Arrange
            var item = SaleTestData.SaleItemFaker.Generate();
            item.Quantity = 0;

            // Act
            var result = _validator.TestValidate(item);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Quantity);
        }

        [Fact(DisplayName = "Validation should fail if sale item quantity is greater the the max quantity allowed")]
        public void Given_InvalidMaxQuantity_When_Validated_Then_ShouldHaveError()
        {
            // Arrange
            var item = SaleTestData.SaleItemFaker.Generate();
            item.Quantity = SaleItemValidator.MAX_IDENTICAL_ITEMS+1;

            // Act
            var result = _validator.TestValidate(item);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Quantity);
        }


        [Fact(DisplayName = "The item must have IsCanceled set to true when SetCanceled is called")]
        public void Given_IsCanceled_When_SetCanceled_Then_ShouldBeTrue()
        {
            // Arrange
            var item = SaleTestData.SaleItemFaker.Generate();

            // Act
            item.SetCanceled();

            // Assert
            Assert.True(item.IsCanceled);
            Assert.NotNull(item.UpdatedAt);
        }

        [Fact(DisplayName = "Must calculate the Total amount correctly.")]
        public void Given_ValidItem_When_Created_Then_ShouldHaveCorrectTotalAmount()
        {
            // Arrange
            var item = SaleTestData.SaleItemFaker.Generate();
            item.Quantity = 4;
            item.UnitPrice = 5;

            // Act
            decimal totalAmount = 20;

            // Assert
            Assert.Equal(item.TotalAmount, totalAmount);
        }
    }
}
