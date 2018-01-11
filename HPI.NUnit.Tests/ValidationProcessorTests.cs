using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPI.BusinessEntities;
using HPI.BusinessServices;
namespace HPI.NUnit.Tests
{
    [TestFixture]
    public class ValidationProcessorTests
    {
        private ValidationProcessor validationProcessor;
        [OneTimeSetUp]
        public void Setup()
        {
            validationProcessor = new ValidationProcessor();
        }
        [Test]
        public void Given_a_productcode_When_Starts_with_A_Then_price_should_be_in_range_of_zero_to_less_than_hundred()
        {
            ProductModel prodModel = new ProductModel { ProductCode = "A001", Price = 35 };
            ValidationModel validModel = validationProcessor.ValidateProduct(prodModel);
            Assert.AreEqual(ValidationStatus.OK, validModel.Status);
        }

        [Test]
        public void Given_a_productcode_When_Starts_with_B_Then_price_should_be_in_range_of_hundred_to_less_than_thousand()
        {
            ProductModel prodModel = new ProductModel { ProductCode = "B001", Price = 100 };
            ValidationModel validModel = validationProcessor.ValidateProduct(prodModel);
            Assert.AreEqual(ValidationStatus.OK, validModel.Status);
        }

        [Test]
        public void Given_a_productcode_When_Starts_with_C_Then_price_should_be_greater_than__or_equal_to_thousand()
        {
            ProductModel prodModel = new ProductModel { ProductCode = "C001", Price = 1000 };
            ValidationModel validModel = validationProcessor.ValidateProduct(prodModel);
            Assert.AreEqual(ValidationStatus.OK, validModel.Status);
        }

        [Test]
        public void Given_a_productcode_When_Starts_with_A_Then_fail_validation_if_price_not_in_range_of_zero_and_less_than_hundred()
        {
            ProductModel prodModel = new ProductModel { ProductCode = "A001", Price = 100 };
            ValidationModel validModel = validationProcessor.ValidateProduct(prodModel);
            Assert.AreEqual(ValidationStatus.ValidationError, validModel.Status);
        }

        [Test]
        public void Given_a_productcode_When_Starts_with_B_Then_fail_validation_if_price_not_in_range_of_hundred_and_less_than_thousand()
        {
            ProductModel prodModel = new ProductModel { ProductCode = "B001", Price = 1000 };
            ValidationModel validModel = validationProcessor.ValidateProduct(prodModel);
            Assert.AreEqual(ValidationStatus.ValidationError, validModel.Status);
        }

        [Test]
        public void Given_a_productcode_When_Starts_with_C_Then_fail_validation_if_price_is_less_than_thousand()
        {
            ProductModel prodModel = new ProductModel { ProductCode = "C001", Price = 999 };
            ValidationModel validModel = validationProcessor.ValidateProduct(prodModel);
            Assert.AreEqual(ValidationStatus.ValidationError, validModel.Status);
        }

        [Test]
        public void Given_a_productcode_When_Starts_with_other_than_A_B_C_Then_fail_validation()
        {
            ProductModel prodModel = new ProductModel { ProductCode = "P001", Price = 0 };
            ValidationModel validModel = validationProcessor.ValidateProduct(prodModel);
            Assert.AreEqual(ValidationStatus.ValidationError, validModel.Status);
        }

        [Test]
        public void Given_a_productcode_When_set_to_blank_Then_fail_validation()
        {
            ProductModel prodModel = new ProductModel { ProductCode = string.Empty, Price = 0 };
            ValidationModel validModel = validationProcessor.ValidateProduct(prodModel);
            Assert.AreEqual(ValidationStatus.ValidationError, validModel.Status);
        }

        [OneTimeTearDown]
        public void DisposeAllObjects()
        {
            validationProcessor = null;
        }
    }
}
