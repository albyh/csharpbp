using Microsoft.VisualStudio.TestTools.UnitTesting;
using Acme.Biz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Biz.Tests
{
    [TestClass()]
    public class ProductTests
    {
        [TestMethod()]
        public void SayHelloTest()
        {
            //Arrange
            var currentProduct = new Product();
            currentProduct.ProductName = "Saw";
            currentProduct.ProductId = 1;
            currentProduct.Description = "Steel Saw Blade";
            currentProduct.ProductVendor.CompanyName = "ABC Corp";
            var expected = "Hello Saw (1): Steel Saw Blade. Available on: ";

            //Act
            var actual = currentProduct.SayHello();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SayHello_ParameterizedConstructor()
        {
            //Arrange
            var currentProduct = new Product(1, "Saw", "Steel Saw Blade");
            var expected = "Hello Saw (1): Steel Saw Blade. Available on: ";

            //Act
            var actual = currentProduct.SayHello();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SayHello_ObjectInitializer()
        {
            //Arrange
            var currentProduct = new Product
            {
                ProductId = 1,
                ProductName = "Saw",
                Description = "Steel Saw Blade",
            };

            var expected = "Hello Saw (1): Steel Saw Blade. Available on: ";

            //Act
            var actual = currentProduct.SayHello();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SayHello_Product_Null()
        {
            //Arrange
            Product currentProduct = null;
            var companyName = currentProduct?.ProductVendor?.CompanyName;

            string expected = null;

            //Act
            var actual = companyName;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ConvertMetersToInchesTest()
        {
            // Arrange
            var expected = 78.74;

            // Act
            var actual = Product.InchesPerMeter * 2;

            // Assert
            Assert.AreEqual(expected, actual);

        }

        [TestMethod()]
        public void MinimumPriceTest_Default()
        {
            // Arrange
            var currentProduct = new Product();
            var expected = .96m;

            // Act
            var actual = currentProduct.MinimumPrice;

            // Assert
            Assert.AreEqual(expected, actual);

        }

        [TestMethod()]
        public void MinimumPriceTest_Bulk()
        {
            // Arrange
            var currentProduct = new Product(1, "Bulk Tools", "");
            var expected = 9.99m;

            // Act
            var actual = currentProduct.MinimumPrice;

            // Assert
            Assert.AreEqual(expected, actual);

        }

        [TestMethod()]
        public void ProductName_Format()
        {
            // Arrange
            //var currentProduct = new Product(1, "  Steel Hammer ", "");
            var currentProduct = new Product();
            currentProduct.ProductName = " Steel Hammer   ";

            var expected = "Steel Hammer";

            // Act
            var actual = currentProduct.ProductName;

            // Assert
            Assert.AreEqual(expected, actual);

        }

        [TestMethod()]
        public void ProductName_TooShort()
        {
            // Arrange
            var currentProduct = new Product();
            currentProduct.ProductName = " aw                        ";

            string expected = null;
            string expectedMsg = "Product Name invalid length.";

            // Act
            var actual = currentProduct.ProductName;
            var actualMsg = currentProduct.ValidationMessage;

            // Assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expectedMsg, actualMsg);

        }

        [TestMethod()]
        public void ProductName_TooLong()
        {
            // Arrange
            var currentProduct = new Product();
            currentProduct.ProductName = "asdsdfsdffasdfasdfasdfasdf";

            string expected = null;
            string expectedMsg = "Product Name invalid length.";

            // Act
            var actual = currentProduct.ProductName;
            var actualMsg = currentProduct.ValidationMessage;

            // Assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expectedMsg, actualMsg);
        }

        [TestMethod()]
        public void ProductName_ValidLength()
        {
            // Arrange
            var currentProduct = new Product();
            currentProduct.ProductName = " Valid Tool Name";

            string expected = "Valid Tool Name";
            string expectedMsg = null;

            // Act
            var actual = currentProduct.ProductName;
            var actualMsg = currentProduct.ValidationMessage;

            // Assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expectedMsg, actualMsg);
        }

        [TestMethod()]
        public void ProductCategory_Default()
        {
            // Arrange
            var currentProduct = new Product();

            string expectedCategory = "Tools";
            int expectedSeqNo = 1;

            // Act
            var actualCategory = currentProduct.Category;
            var actualSeqNo = currentProduct.SequenceNumber;

            // Assert
            Assert.AreEqual(expectedCategory, actualCategory);
            Assert.AreEqual(expectedSeqNo, actualSeqNo);
        }

        [TestMethod()]
        public void ProductCategory_New()
        {
            // Arrange
            var currentProduct = new Product();
            currentProduct.Category = "Guns";
            string expected = "Guns";

            // Act
            var actual = currentProduct.Category;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SequenceNumber_Default()
        {
            // Arrange
            var currentProduct = new Product();
            int expected = 1;

            // Act
            var actual = currentProduct.SequenceNumber;

            // Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void SequenceNumber_New()
        {
            // Arrange
            var currentProduct = new Product();

            currentProduct.SequenceNumber = 3;
            int expected = 3;

            // Act
            var actual = currentProduct.SequenceNumber;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ProductCode_DefaultValue()
        {
            // Arrange
            var currentProduct = new Product();
            //currentProduct.Category = "Tools";
            //currentProduct.SequenceNumber = 3;
            string expected = "Tools-1";

            // Act
            var actual = currentProduct.ProductCode;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CalculateSuggestedPriceTest()
        {
            // Arrange
            var currentProduct = new Product(1, "Saw", "");
            currentProduct.Cost = 50m;
            var expected = 55m;

            // Act
            var actual = currentProduct.CalculateSuggestedPrice(10m);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}