using System;
using System.Collections.Generic;

namespace Contoso.Data
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderTimeStamp { get; set; }
        public DateTime? ReadyForShippingTimeStamp { get; set; }
        public DateTime? ShippedTimeStamp { get; set; }
        public List<CartItem> Items { get; set; }
    }
}