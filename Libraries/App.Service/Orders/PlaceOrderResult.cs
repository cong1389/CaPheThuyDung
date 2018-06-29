using System.Collections.Generic;
using App.Domain.Orders;

namespace App.Service.Orders
{
    /// <summary>
    /// Represents a PlaceOrderResult
    /// </summary>
    public class PlaceOrderResult
    {
        public IList<string> Errors { get; set; }

        public PlaceOrderResult()
        {
            Errors = new List<string>();
        }

        public bool Success => (Errors.Count == 0);

        public void AddError(string error)
        {
            Errors.Add(error);
        }


        /// <summary>
        /// Gets or sets the placed order
        /// </summary>
        public Order PlacedOrder { get; set; }
    }
}
