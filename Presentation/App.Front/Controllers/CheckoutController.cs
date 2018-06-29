using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using App.Aplication;
using App.Aplication.Extensions;
using App.Aplication.Filters;
using App.Core.Extensions;
using App.Domain.Entities.Data;
using App.Domain.Entities.Orders;
using App.FakeEntity.Common;
using App.Front.Extensions;
using App.Front.Models.Checkout;
using App.Front.Models.ShoppingCart;
using App.Service.Addresses;
using App.Service.Common;
using App.Service.Customers;
using App.Service.GenericAttribute;
using App.Service.Orders;
using App.Service.PaymentMethodes;
using App.Service.Post;
using App.Service.ShippingMethodes;
using AutoMapper;

namespace App.Front.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly IWorkContext _workContext;

        private readonly ICustomerService _customerService;

        private readonly IAddressService _addressService;

        private readonly IPostService _postService;

        private readonly IShoppingCartItemService _shoppingCartItemService;

        private readonly IPaymentMethodService _paymentMethodService;

        private readonly IShippingMethodService _shippingMethodService;

        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IOrderService _orderService;
        private readonly IOrderProcessingService _orderProcessingService;

        public CheckoutController(IWorkContext workContext, ICustomerService customerService,
            IAddressService addressService, IPostService postService
            , IShoppingCartItemService shoppingCartItemService
            , IPaymentMethodService paymentMethodService
            , IGenericAttributeService genericAttributeService
            , IShippingMethodService shippingMethodService
            , IOrderService orderService
            , IOrderProcessingService orderProcessingService)
        {
            _workContext = workContext;
            _customerService = customerService;
            _addressService = addressService;
            _postService = postService;
            _shoppingCartItemService = shoppingCartItemService;
            _paymentMethodService = paymentMethodService;
            _genericAttributeService = genericAttributeService;
            _shippingMethodService = shippingMethodService;
            _orderService = orderService;
            _orderProcessingService = orderProcessingService;
        }

        #region Billing Address

        public ActionResult BillingAddress()
        {
            var cart = _workContext.CurrentCustomer.GetCartItems();

            if (cart == null || !cart.Any())
            {
                return RedirectToAction("Index", "Home");
            }

            var model = PrepareBillingAddressModel();

            return View(model);
        }

        [NonAction]
        protected CheckoutBillingAddressModel PrepareBillingAddressModel()
        {
            var model = new CheckoutBillingAddressModel();
            try
            {
                var billAddress = _workContext.CurrentCustomer.Addresses;
                if (billAddress.Count != 0 && billAddress.Any())
                {
                    model.NewAddress = new AddressViewModel();
                    foreach (var address in billAddress)
                    {
                        model.ExistingAddresses.Add(Mapper.Map<AddressViewModel>(address));
                    }
                }

                var customer = _workContext.CurrentCustomer;
                if (customer != null)
                {
                    model.CustomerInfoModel.ShippingAddressId = customer.ShippingAddress.Id;
                }
            }
            catch
            {
                // ignored
            }

            return model;
        }

        [HttpPost, ActionName("BillingAddress")]
        [FormValueRequired("saveAddress")]
        public ActionResult NewBillingAddress(CheckoutBillingAddressModel model)
        {
            //validation
            var cart = _workContext.CurrentCustomer.GetCartItems();

            if (!cart.Any())
            {
                return RedirectToRoute("ShoppingCart");
            }

            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return new HttpUnauthorizedResult();
            }

            if (ModelState.IsValid)
            {
                var address = model.NewAddress.ToEntity();

                _workContext.CurrentCustomer.Addresses.Add(address);
                _workContext.CurrentCustomer.BillingAddress = address;

                _customerService.Update(_workContext.CurrentCustomer);
            }

            //var addresses = _workContext.CurrentCustomer.Addresses;

            //if (ModelState.IsValid)
            //{
            //    if (addresses.Count == 0 || !addresses.Any())
            //    {
            //        var address = model.NewAddress.ToEntity();

            //        //some validation
            //        if (address.CountryId == 0)
            //            address.CountryId = null;

            //        if (address.StateProvinceId == 0)
            //            address.StateProvinceId = null;

            //        _workContext.CurrentCustomer.Addresses.Add(address);
            //        _workContext.CurrentCustomer.BillingAddress = address;

            //        _customerService.Update(_workContext.CurrentCustomer);

            //        //return RedirectToAction("PaymentMethod");
            //    }
            //    else
            //    {
            //        //var objAddress = addresses.FirstOrDefault();

            //        var objAddress = model.NewAddress.ToEntity();
            //        objAddress.FirstName = model.NewAddress.FirstName;
            //        objAddress.Email = model.NewAddress.Email;
            //        objAddress.PhoneNumber = model.NewAddress.PhoneNumber;
            //        objAddress.Address1 = model.NewAddress.Address1;
            //        _customerService.Update(_workContext.CurrentCustomer);
            //        _addressService.Create(objAddress);

            //        //return RedirectToAction("PaymentMethod");
            //    }
            //}

            //If we got this far, something failed, redisplay form
            model = PrepareBillingAddressModel();

            return View(model);
        }

        public ActionResult SelectShippingAddress(int addressId)
        {
            var address = _workContext.CurrentCustomer.Addresses.FirstOrDefault(a => a.Id == addressId);
            if (address == null)
            {
                return RedirectToAction("PaymentMethod");
            }

            _workContext.CurrentCustomer.ShippingAddress = address;
            _customerService.Update(_workContext.CurrentCustomer);

            return RedirectToAction("PaymentMethod");
        }

        #endregion

        #region Payment method

        public ActionResult PaymentMethod()
        {
            var cart = _workContext.CurrentCustomer.GetCartItems();

            if (!cart.IsAny())
            {
                return RedirectToAction("Index", "Home");
            }

            var model = PreparePaymentMethodModel(cart);

            return View(model);
        }

        [NonAction]
        protected CheckoutPaymentMethodModel PreparePaymentMethodModel(IOrderedEnumerable<ShoppingCartItem> cart)
        {
            var model = new CheckoutPaymentMethodModel();

            var payment = _paymentMethodService.GetAll();

            if (payment.IsAny())
            {
                model.PaymentMethods = (from x in payment
                                        select new CheckoutPaymentMethodModel.PaymentMethodModel
                                        {
                                            Id = x.Id,
                                            PaymentMethodSystemName = x.PaymentMethodSystemName,
                                            FullDescription = x.Description
                                        }).ToList();

            }

            var selectedPaymentMethodSystemName = _workContext.CurrentCustomer.GetAttribute("Customer", Contains.SelectedPaymentMethod, _genericAttributeService);

            var selected = false;
            if (selectedPaymentMethodSystemName.HasValue())
            {
                var paymentMethodToSelect = model.PaymentMethods.Find(pm => pm.PaymentMethodSystemName.IsCaseInsensitiveEqual(selectedPaymentMethodSystemName));
                if (paymentMethodToSelect != null)
                {
                    paymentMethodToSelect.Selected = true;
                    selected = true;
                }
            }

            // if no option has been selected, let's do it for the first one
            if (!selected)
            {
                var paymentMethodToSelect = model.PaymentMethods.FirstOrDefault();
                if (paymentMethodToSelect != null)
                {
                    paymentMethodToSelect.Selected = true;
                }
            }

            return model;
        }

        [HttpPost, ActionName("PaymentMethod")]
        [FormValueRequired("nextstep")]
        public ActionResult SelectPaymentMethod(string paymentmethod, CheckoutPaymentMethodModel model, FormCollection form)
        {
            var customer = _workContext.CurrentCustomer;
            var cart = customer.GetCartItems();

            if (cart == null || !cart.Any())
            {
                return RedirectToAction("Index", "Home");
            }

            //Save payment method
            var paymentMethodKey = form.AllKeys.FirstOrDefault(m => m.StartsWith("payment_method_id", StringComparison.InvariantCultureIgnoreCase));
            if (paymentMethodKey.HasValue())
            {
                var paymentMethodName = form[paymentMethodKey];
                _genericAttributeService.SaveGenericAttribute(customer.Id, "Customer", Contains.SelectedPaymentMethod, paymentMethodName);
            }

            //Save shipping method
            var shippingMethodKey = form.AllKeys.FirstOrDefault(m => m.StartsWith("shipping_rate_id", StringComparison.InvariantCultureIgnoreCase));
            if (shippingMethodKey.HasValue())
            {
                var shippingMethodName = form[shippingMethodKey];
                _genericAttributeService.SaveGenericAttribute(customer.Id, "Customer", Contains.SelectedShippingOption, shippingMethodName);
            }

            return RedirectToAction("Confirm");
        }

        #endregion

        #region Shipping method

        [ChildActionOnly]
        public ActionResult ShippingMethod()
        {
            var cart = _workContext.CurrentCustomer.GetCartItems();

            if (!cart.IsAny())
            {
                return RedirectToAction("Index", "Home");
            }

            var model = PrepareShippingMethodModel(cart);

            return PartialView(model);
        }

        [NonAction]
        protected CheckoutShippingMethodModel PrepareShippingMethodModel(IOrderedEnumerable<ShoppingCartItem> cart)
        {
            var model = new CheckoutShippingMethodModel();
            //var customer = _workContext.CurrentCustomer;
            var shipping = _shippingMethodService.GetAll();

            if (shipping.IsAny())
            {
                model.ShippingMethods = (from x in shipping
                                         select new CheckoutShippingMethodModel.ShippingMethodModel
                                         {
                                             Id = x.Id,
                                             Name = x.Name,
                                             Description = x.Description
                                         }).ToList();

            }

            var selectedShippingOption = _workContext.CurrentCustomer.GetAttribute("Customer", Contains.SelectedShippingOption, _genericAttributeService);

            var selected = false;
            if (selectedShippingOption.HasValue())
            {
                var shippingOptionToSelect = model.ShippingMethods.ToList()
                         .Find(so => !string.IsNullOrEmpty(so.Name) && so.Name.Equals(selectedShippingOption, StringComparison.InvariantCultureIgnoreCase));

                if (shippingOptionToSelect != null)
                {
                    shippingOptionToSelect.Selected = true;
                    selected = true;
                }
            }

            // if no option has been selected, let's do it for the first one
            if (!selected)
            {
                var shippingOptionToSelect = model.ShippingMethods.FirstOrDefault();
                if (shippingOptionToSelect != null)
                {
                    shippingOptionToSelect.Selected = true;
                }
            }

            return model;
        }

        #endregion

        #region Cart checkout

        /// <summary>
        /// Get Cart theo khách hàng
        /// </summary>
        /// <returns></returns>
        private MiniShoppingCartModel PrepareMiniShoppingCartModel()
        {
            //Get Cart by customer
            var cart = _workContext.CurrentCustomer.GetCartItems();
            var lstPost = new List<Post>();

            if (cart.IsAny())
            {
                foreach (var item in cart)
                {
                    var objPost = _postService.GetById(item.PostId);
                    lstPost.Add(objPost);
                }
            }
            var miniShopping = new MiniShoppingCartModel
            {
                Items = lstPost,
                ShoppingCarts = cart,
                SubTotal = _shoppingCartItemService.GetCurrentCartSubTotal(cart)
            };

            HttpContext.Session["OrderPaymentInfo"] = miniShopping;

            return miniShopping;
        }

        /// <summary>
        /// Ajax show sản phẩm bên trái khi checkout
        /// </summary>
        /// <returns></returns>
        public JsonResult CartByCustomer()
        {
            var miniCart = PrepareMiniShoppingCartModel();

            var jsonResult =
                Json(new {success = true, list = this.RenderRazorViewToString("_Checkout.Cart", miniCart)},
                    JsonRequestBehavior.AllowGet);

            return jsonResult;

        }

        #endregion

        #region Confirm

        public ActionResult Confirm()
        {
            var cart = _workContext.CurrentCustomer.GetCartItems();

            if (!cart.IsAny())
            {
                return RedirectToAction("Index", "Home");
            }

            var model = PrepareConfirmOrderModel(cart);

            return View(model);
        }

        private CheckoutConfirmModel PrepareConfirmOrderModel(IOrderedEnumerable<ShoppingCartItem> cart)
        {
            var model = new CheckoutConfirmModel();

            return model;
        }

        [HttpPost, ActionName("Confirm")]
        [ValidateInput(false)]
        public ActionResult ConfirmOrder(FormCollection form)
        {
            var storeId = 0;
            var customer = _workContext.CurrentCustomer;
            var cart = _workContext.CurrentCustomer.GetCartItems();

            if (!cart.IsAny())
            {
                return RedirectToAction("Index", "Home");
            }

            var processPaymentRequest = new ProcessPaymentRequest
            {
                StoreId = storeId,
                CustomerId = customer.Id,
                PaymentMethodSystemName =
                    customer.GetAttribute("Customer", Contains.SelectedPaymentMethod, _genericAttributeService)
            };

            var placeOrderExtraData = new Dictionary<string, string>();

            _orderProcessingService.PlaceOrder(processPaymentRequest, placeOrderExtraData);

            return RedirectToAction("Complete");
        }

        #endregion

        #region Complete

        public ActionResult Complete()
        {
            var customer = _workContext.CurrentCustomer;

            var ieOrder = _orderService.GetByCustomerId(customer.Id, false);

            if (ieOrder == null)
            {
                return HttpNotFound();
            }

            var order = ieOrder.OrderByDescending(m => m.Id).FirstOrDefault();

            return View(order);
        }

        public ActionResult AddressDelete(int addressId)
        {
            if (addressId < 1)
            {
                return HttpNotFound();
            }

            var customer = _workContext.CurrentCustomer;

            var address = customer.Addresses.FirstOrDefault(a => a.Id == addressId);

            customer.RemoveAddress(address);
            _customerService.UpdateCustomer(customer);
            _addressService.Update(address);

            return RedirectToAction("BillingAddress");
        }

        #endregion


    }
}