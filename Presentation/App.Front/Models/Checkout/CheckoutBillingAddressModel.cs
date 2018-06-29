using System.Collections.Generic;
using App.FakeEntity.Common;
using App.Front.Models.Customer;
using App.Front.Models.ShoppingCart;

namespace App.Front.Models.Checkout
{
    public class CheckoutBillingAddressModel
    {
        public CheckoutBillingAddressModel()
        {
            ExistingAddresses = new List<AddressViewModel>();
            NewAddress = new AddressViewModel();
            CustomerInfoModel = new CustomerInfoModel();
        }

        public IList<AddressViewModel> ExistingAddresses { get; set; }

        public AddressViewModel NewAddress { get; set; }

        public CustomerInfoModel CustomerInfoModel { get; set; }

        public MiniShoppingCartModel MiniShoppingCart { get; set; }

        /// <summary>
        /// Used on one-page checkout page
        /// </summary>
        public bool NewAddressPreselected { get; set; }
    }
}