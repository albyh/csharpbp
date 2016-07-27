using Acme.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Biz
{
    /// <summary>
    /// Manages the vendors from whom we purchase our inventory.
    /// </summary>
    public class Vendor 
    {
        public enum IncludeAddress { Yes, No };
        public enum SendCopy { Yes, No };
            
        public int VendorId { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }

        /// <summary>
        /// Sends a product order to the vendor
        /// </summary>
        /// <param name="product">Product to order.</param>
        /// <param name="quantity">Quantity of the Product to order.</param>
        /// <returns></returns>
        public OperationResult PlaceOrder(Product product, int quantity)
        {
            return PlaceOrder(product, quantity, null, null); 
        }

        /// <summary>
        /// Sends a product order to the vendor
        /// </summary>
        /// <param name="product">Product to order.</param>
        /// <param name="quantity">Quantity of the Product to order.</param>
        /// <param name="deliverBy">Requested delivery date.</param>
        /// <returns></returns>
        public OperationResult PlaceOrder(Product product, int quantity, DateTimeOffset? deliverBy)
        {
            return PlaceOrder(product, quantity, deliverBy, null);
        }

        /// <summary>
        /// Sends a product order to the vendor
        /// </summary>
        /// <param name="product">Product to order.</param>
        /// <param name="quantity">Quantity of the Product to order.</param>
        /// <param name="deliverBy">Requested delivery date.</param>
        /// <param name="instructions">Delivery instructions</param>
        /// <returns></returns>
        public OperationResult PlaceOrder(Product product, int quantity, 
                                            DateTimeOffset? deliverBy, 
                                            string instructions)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(Product));
            if (quantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(quantity));
            if (deliverBy <= DateTimeOffset.Now)
                throw new ArgumentOutOfRangeException(nameof(deliverBy));

            var success = false;

            var orderText = String.Format("Order from Acme, Inc{0}Product: {1}{0}Quantity: {2}", System.Environment.NewLine, product.ProductCode, quantity);

            if (deliverBy.HasValue)
            {
                orderText += String.Format("{0}Deliver By: {1}",
                                            Environment.NewLine, deliverBy.Value.ToString("d"));
            }

            var emailService = new EmailService();
            var confirmation = emailService.SendMessage("New Order", orderText, this.Email);

            if (confirmation.StartsWith("Message sent:"))
            {
                success = true;
            }
            var operationResult = new OperationResult(success, orderText);
            return operationResult;
        }

        /// <summary>
        /// Sends a product order to the vendor.
        /// </summary>
        /// <param name="product">Product to order.</param>
        /// <param name="quantity">Quantity of the product to order.</param>
        /// <param name="includeAddress">True to include the shipping address.</param>
        /// <param name="sendCOpy">True to send a copy of the email to the current</param>
        /// <returns>Success flag and order text</returns>
        public OperationResult PlaceOrder(Product product, int quantity,
                                            IncludeAddress includeAddress, SendCopy sendCopy) 
        {
            var orderText = "Test";
            if (includeAddress == IncludeAddress.Yes) orderText += " With Address";
            if (sendCopy == SendCopy.Yes) orderText += " With Copy";

            var operationResult = new OperationResult(true, orderText);
            return operationResult;

        }

        /// <summary>
        /// Sends an email to welcome a new vendor.
        /// </summary>
        /// <returns></returns>
        public string SendWelcomeEmail(string message)
        {
            var emailService = new EmailService();
            var subject = ("Hello " + this.CompanyName).Trim();
            var confirmation = emailService.SendMessage(subject,
                                                        message, 
                                                        this.Email);
            return confirmation;
        }
    }
}
