﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Acme.Biz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acme.Common;

namespace Acme.Biz.Tests
{
    [TestClass()]
    public class VendorTests
    {
        [TestMethod()]
        public void SendWelcomeEmail_ValidCompany_Success()
        {
            // Arrange
            var vendor = new Vendor();
            vendor.CompanyName = "ABC Corp";
            var expected = "Message sent: Hello ABC Corp";

            // Act
            var actual = vendor.SendWelcomeEmail("Test Message");

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SendWelcomeEmail_EmptyCompany_Success()
        {
            // Arrange
            var vendor = new Vendor();
            vendor.CompanyName = "";
            var expected = "Message sent: Hello";

            // Act
            var actual = vendor.SendWelcomeEmail("Test Message");

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SendWelcomeEmail_NullCompany_Success()
        {
            // Arrange
            var vendor = new Vendor();
            vendor.CompanyName = null;
            var expected = "Message sent: Hello";

            // Act
            var actual = vendor.SendWelcomeEmail("Test Message");

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void PlaceOrderTest()
        {
            //Arrange
            var vendor = new Vendor();
            var product = new Product(1, "Saw", "");
            var expected = new OperationResult(true,
                "Order from Acme, Inc\r\nProduct: Tools-1\r\nQuantity: 12");

            //Act
            var actual = vendor.PlaceOrder(product, 12);

            //Assert 
            Assert.AreEqual(expected.Success, actual.Success);
            Assert.AreEqual(expected.Message, actual.Message);
        }

        [TestMethod()]
        public void PlaceOrder_WithAddress()
        {
            // Arrange
            var vendor = new Vendor();
            var product = new Product(1, "Saw", "");
            var expected = new OperationResult(true, "Test With Address");

            // Act
            var actual = vendor.PlaceOrder(product, 12,
                Vendor.IncludeAddress.Yes, Vendor.SendCopy.No );

            // Assert 
            Assert.AreEqual(expected.Success, actual.Success);
            Assert.AreEqual(expected.Message, actual.Message);
        }


        [TestMethod()]
        public void PlaceOrder_3Parameters()
        {
            //Arrange
            var vendor = new Vendor();
            var product = new Product(1, "Saw", "");
            var expected = new OperationResult(true,
                "Order from Acme, Inc\r\nProduct: Tools-1\r\nQuantity: 12" +
                "\r\nDeliver By: 7/26/2016");

            //Act
            var actual = vendor.PlaceOrder(product, 12,
                new DateTimeOffset(2016, 7, 26, 0, 0, 0, new TimeSpan(-7, 0, 0)));

            //Assert 
            Assert.AreEqual(expected.Success, actual.Success);
            Assert.AreEqual(expected.Message, actual.Message);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PlaceOrder_NullProduct_Exception()
        {
            // Arrange
            var vendor = new Vendor();

            // Act
            var actual = vendor.PlaceOrder(null, 12);

            // Assert 
            // Expected exception
        }


    }
}