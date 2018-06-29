using App.Core.Common;

namespace App.Domain.Entities.Payments
{
	public class PaymentMethod : AuditableEntity<int>
	{
		public string PaymentMethodSystemName
        {
			get;
			set;
		}

		public string Description
        {
			get;
			set;
		}

	    public string ImageUrl
	    {
	        get;
	        set;
	    }

	    public int OrderDisplay
	    {
	        get;
	        set;
	    }

	    public int Status
	    {
	        get;
	        set;
	    }

        public PaymentMethod()
		{
		}
	}
}