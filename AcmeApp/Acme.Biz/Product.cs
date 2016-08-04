using Acme.Common;
using static Acme.Common.LoggingService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Biz
{
    /// <summary>
    /// Manages products carried in inventory.
    /// </summary>
    public class Product
    {
        public const double InchesPerMeter = 39.37;
        public readonly decimal MinimumPrice;

        #region Constructors
        public Product()
        {
            //this.productVendor = new Vendor();
            //moved this to lazy load in the ProductVendor Property
            Console.WriteLine("Product instance created");
            //set default value for read-only field
            this.MinimumPrice = .96m;
            this.Category = "Tools";
        }

        public Product(int productId,
                        string productName,
                        string description) : this()
        {
            this.ProductId = productId;
            this.ProductName = productName;
            this.Description = description;
            if (this.ProductName.StartsWith("Bulk"))
            {
                this.MinimumPrice = 9.99m;
            }
            Console.WriteLine("Product instance has a name: {0}", ProductName );
        }
        #endregion

        #region Properties
        private DateTime? availabilityDate;

        public DateTime? AvailabilityDate
        {
            get { return availabilityDate; }
            set { availabilityDate = value; }
        }

        public decimal Cost { get; set; }

        private string productName;

        public string ProductName
        {
            get {
                var formattedValue = productName?.Trim();
                    return formattedValue; }
            set {
                    value = value?.Trim();
                    if (value.Length < 3 || value.Length > 20)
                    {
                        ValidationMessage = "Product Name invalid length.";
                    }
                    else
                    {
                        productName = value;
                    }
                }
        }
        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        private int productId;

        public int ProductId
        {
            get { return productId; }
            set { productId = value; }
        }

        private Vendor productVendor;

        public Vendor ProductVendor
        {
            get {
                if (productVendor == null)
                {
                    productVendor = new Vendor();
                }
                return productVendor;
                }
            set { productVendor = value; }
        }
        internal string Category { get; set; }
        public int SequenceNumber { get; set; } = 1;

        public string ProductCode => String.Format("{0}-{1}", this.Category, this.SequenceNumber);

        public string ValidationMessage { get; private set; }

        #endregion

        /// <summary>
        /// Calculates the suggested retail price
        /// </summary>
        /// <param name="markupPercent">Percent used to mark up the cost</param>
        /// <returns></returns>
        public OperationResultDecimal CalculateSuggestedPrice(decimal markupPercent)
        {
            var message = "";
            if (markupPercent <= 0m)
            {
                message = "Invalid markeup percentage";
            }
            else if (markupPercent < 10)
            {
                message = "Below recommended markup";
            }
            var value = this.Cost + (this.Cost * markupPercent / 100);
            var operationResult = new OperationResultDecimal(value, message);
            return operationResult;
        }

        public string SayHello()
        {
            //var vendor = new Vendor();
            //vendor.SendWelcomeEmail("Message from Product Class");
            var emailService = new EmailService();
            var confirmation = emailService.SendMessage("New Product",
                this.ProductName, "sales@acme.com");

            var result = LogAction("saying hello");

            return String.Format("Hello {0} ({1}): {2}. Available on: {3}",
                ProductName, ProductId, Description, AvailabilityDate?.ToShortDateString());

        }

        public override string ToString() =>
            string.Format("{0} ({1})", this.ProductName, this.productId);

    }
}
