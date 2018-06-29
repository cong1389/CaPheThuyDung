using System;
using System.Web.Mvc;
using App.Domain.Orders;
using App.FakeEntity.Orders;
using App.Front.Extensions;
using App.Service.Common;
using App.Service.Orders;

namespace App.Front.Controllers
{
    public class OrderController : FrontBaseController
    {
        private readonly IOrderService _orderService;
        private readonly ICommonServices _services;

        public OrderController(IOrderService orderService
            , ICommonServices services)
        {
            _orderService = orderService;
            _services = services;
        }

        public ActionResult Details(int id)
        {
            var order = _orderService.GetById(id);

            if (IsNonExistenOrder(order))
            {
                return HttpNotFound();
            }

            if (IsAuthorizedOrder(order))
            {
                return new HttpUnauthorizedResult();
            }

            var model = PrepareOrderDetailsModel(order);

            return View(model);
        }

        [NonAction]
        private OrderViewModel PrepareOrderDetailsModel(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException("order");
            }

            var model = new OrderViewModel();
            order.ToModel(model);

            foreach (var orderItem in order.OrderItems)
            {
                var orderItemModel = new OrderViewModel.OrderItemModel
                {
                    Id = orderItem.Id,
                    PostId = orderItem.PostId,
                    PostName = orderItem.Post.Title,
                    Quantity = orderItem.Quantity,
                    UnitPriceInclTax = orderItem.UnitPriceInclTax,
                    SubTotalInclTax = orderItem.PriceInclTax
                };

                model.Items.Add(orderItemModel);
            }



            return model;
        }

        private bool IsNonExistenOrder(Order order)
        {
            var flag = order == null || order.Deleted || !HttpContext.User.Identity.IsAuthenticated;

            return flag;
        }

        private bool IsAuthorizedOrder(Order order)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return order == null || order.CustomerId != _services.WorkContext.CurrentCustomer.Id;
            }

            return order == null;
        }

    }
}