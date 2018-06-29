using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Aplication;
using App.Aplication.Extensions;
using App.Aplication.Filters;
using App.Domain.Entities.Data;
using App.Domain.Entities.Orders;
using App.FakeEntity.Common;
using App.Front.Models.ShoppingCart;
using App.Service.Common;
using App.Service.Customers;
using App.Service.GenericAttribute;
using App.Service.Orders;
using App.Service.PaymentMethodes;
using App.Service.Post;
using AutoMapper;

namespace App.Front.Controllers
{
    public class ShoppingCartController : FrontBaseController
    {
        private readonly IShoppingCartItemService _shoppingCartItemService;
        private readonly IPostService _postService;
        private readonly IWorkContext _workContext;
        private readonly IGenericAttributeService _genericAttributeService;

        public ShoppingCartController(IShoppingCartItemService shoppingCartItemService
            , IPostService postService, IWorkContext workContext, IGenericAttributeService genericAttributeService,
            IPaymentMethodService paymentMethodService)
        {
            _shoppingCartItemService = shoppingCartItemService;
            _postService = postService;
            _workContext = workContext;
            _genericAttributeService = genericAttributeService;
        }

        [HttpPost]
        public JsonResult AddProduct(int postId, int quantity, decimal price, FormCollection form)
        {
            var post = _postService.GetById(postId);

            var ctx = new AddToCartContext
            {
                Post = post,
                Quantity = quantity,
                Price = price
            };

            //Create cart
            _shoppingCartItemService.AddToCart(ctx);

            var model = PrepareMiniShoppingCartModel();

            var jsonResult =
                Json(new { success = true, list = this.RenderRazorViewToString("_Order.TopCart", model) },
                    JsonRequestBehavior.AllowGet);

            return jsonResult;
        }

        private MiniShoppingCartModel PrepareMiniShoppingCartModel()
        {
            var cart = _workContext.CurrentCustomer.GetCartItems();

            var lstPost = new List<Post>();

            if (cart.IsAny())
            {
                ViewBag.CurrentOrderItem = cart.FirstOrDefault(x => x.CreatedDate >= DateTime.UtcNow.AddSeconds(-2));

                foreach (var item in cart)
                {
                    var objPost = _postService.GetById(item.PostId, false);
                    lstPost.Add(objPost);
                }
            }

            var model = new MiniShoppingCartModel
            {
                Items = lstPost,
                ShoppingCarts = cart,
                SubTotal = _shoppingCartItemService.GetCurrentCartSubTotal(cart)
            };

            return model;
        }

        [HttpPost]
        public JsonResult GetPopupCart()
        {
            var model = PrepareMiniShoppingCartModel();

            var jsonResult =
                Json(new { success = true, list = this.RenderRazorViewToString("_Order.TopCart", model) },
                    JsonRequestBehavior.AllowGet);

            return jsonResult;
        }

        public JsonResult DeleteProduct(int id)
        {
            var shppingCart = _shoppingCartItemService.GetById(id);

            _shoppingCartItemService.Delete(shppingCart);

            var jsonResult = Json(new { success = true }, JsonRequestBehavior.AllowGet);

            return jsonResult;
        }

        public JsonResult OrderNotification()
        {
            var model = new MiniShoppingCartModel();

            IEnumerable<ShoppingCartItem> lstShoppingCart = _workContext.CurrentCustomer.ShoppingCartItems;

            if (lstShoppingCart.Any())
            {
                var obj = lstShoppingCart.OrderByDescending(x => x.CreatedDate).First();

                var lstPost = new List<Post>();
                var objPost = _postService.GetById(obj.PostId);
                lstPost.Add(objPost);

                model = new MiniShoppingCartModel
                {
                    Items = lstPost,
                    ShoppingCarts = lstShoppingCart
                };
            }

            var jsonResult =
                Json(
                    new
                    {
                        success = true,
                        list = this.RenderRazorViewToString("_Order.Notification", model)
                    }, JsonRequestBehavior.AllowGet
                    );

            return jsonResult;
        }

        [NonAction]
        public void PrepareShoppingCartModel(IOrderedEnumerable<ShoppingCartItem> cart)
        {
            if (cart == null)
            {
                throw new ArgumentNullException("cart");
            }

            if (!cart.Any())
            {
            }
        }

        public ActionResult Cart()
        {
            var cart = _workContext.CurrentCustomer.GetCartItems();

            var lstPost = new List<Post>();

            if (cart.Any())
            {
                ViewBag.CurrentOrderItem = cart.FirstOrDefault(x => x.CreatedDate >= DateTime.UtcNow.AddSeconds(-2));

                foreach (var item in cart)
                {
                    var objPost = _postService.GetById(item.PostId);
                    lstPost.Add(objPost);
                }
            }

            var model = new MiniShoppingCartModel
            {
                Items = lstPost,
                ShoppingCarts = cart,
                SubTotal = _shoppingCartItemService.GetCurrentCartSubTotal(cart)
            };


            PrepareShoppingCartModel(cart);

            return View(model);
        }

        public ActionResult CartItem(MiniShoppingCartModel model)
        {
            return PartialView("_Cart.CartItem", model);
        }

        /// <summary>
        /// Update cart khi thay đổi số lượng
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="quantity"></param>
        /// <param name="price"></param>
        /// <returns>IEnumber tất cả sản phẩm của khách hàng</returns>
        public JsonResult UpdateCartItem(int postId, int quantity, decimal price)
        {
            //Update cart theo số lượng mới
            var post = _postService.GetById(postId);

            var ctx = new AddToCartContext
            {
                Post = post,
                Quantity = quantity,
                Price = price
            };

            //Update cart
            _shoppingCartItemService.AddToCart(ctx);

            var model = PrepareMiniShoppingCartModel();

            var jsonResult =
                Json(new { success = true, list = this.RenderRazorViewToString("_Cart.CartItem", model) },
                    JsonRequestBehavior.AllowGet);

            return jsonResult;
        }

        [ValidateInput(false)]
        [HttpPost, ActionName("Cart")]
        [FormValueRequired("startcheckout")]
        public ActionResult StartCheckout()
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "User");
            }

            var cart = _workContext.CurrentCustomer.GetCartItems();

            if (cart.IsAny())
            {
                return RedirectToAction("BillingAddress", "Checkout");
            }

            return RedirectToAction("Index", "Home");
        }

        [ChildActionOnly]
        public ActionResult OrderSummary()
        {
            var cart = _workContext.CurrentCustomer.GetCartItems();
            var model = new ShoppingCartModel();

            PrepareShoppingCartModel(model, cart);

            return PartialView(model);
        }

        private void PrepareShoppingCartModel(ShoppingCartModel model, IOrderedEnumerable<ShoppingCartItem> cart)
        {
            //billing info
            var billAddress = _workContext.CurrentCustomer.Addresses;
            if (billAddress.IsAny())
            {
                foreach (var address in billAddress)
                {
                    var map = Mapper.Map<AddressViewModel>(address);
                    model.OrderReviewData.BillingAddress = map;
                }
            }

            //Shipping info
            var shippingAddress = _workContext.CurrentCustomer.ShippingAddress;
            if (shippingAddress != null)
            {
                var mapShipping = Mapper.Map<AddressViewModel>(shippingAddress);
                model.OrderReviewData.ShippingAddress = mapShipping;
            }

            //Shipping method
            var selectedShippingMethodSystemName = _workContext.CurrentCustomer.GetAttribute("Customer", Contains.SelectedShippingOption
                , _genericAttributeService);
            model.OrderReviewData.ShippingMethod = selectedShippingMethodSystemName;

            //Payment method
            var selectedPaymentMethodSystemName = _workContext.CurrentCustomer.GetAttribute("Customer", Contains.SelectedPaymentMethod
                , _genericAttributeService);
            model.OrderReviewData.PaymentMethod = selectedPaymentMethodSystemName;
        }
    }
}