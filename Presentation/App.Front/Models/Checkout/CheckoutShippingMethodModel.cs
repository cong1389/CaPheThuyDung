using System.Collections.Generic;

namespace App.Front.Models.Checkout
{
    public class CheckoutShippingMethodModel 
    {
        public CheckoutShippingMethodModel()
        {
            ShippingMethods = new List<ShippingMethodModel>();
            Warnings = new List<string>();
        }

        public IList<ShippingMethodModel> ShippingMethods { get; set; }

        public IList<string> Warnings { get; set; }

        #region Nested classes

        public class ShippingMethodModel
        {
            public int Id { get; set; }
            public int ShippingMethodId { get; set; }
            public string ShippingRateComputationMethodSystemName { get; set; }
            public string Name { get; set; }
            public string BrandUrl { get; set; }
            public string Description { get; set; }
            public string Fee { get; set; }
            public decimal FeeRaw { get; set; }
            public bool Selected { get; set; }
        }
        #endregion
    }
}