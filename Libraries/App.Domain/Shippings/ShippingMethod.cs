using App.Core.Common;

namespace App.Domain.Shippings
{
    public class ShippingMethod: AuditableEntity<int>
    {
        public string Name
        {
            get;set;
        }
        public string Description
        {
            get; set;
        }
        public int OrderDisplay
        {
            get; set;
        }
        public bool IgnoreCharges
        {
            get; set;
        }

	    public int Status
	    {
		    get; set;
	    }


		public ShippingMethod()
        {
        }
    }
}
