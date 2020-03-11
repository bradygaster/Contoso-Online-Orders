using System;
using System.Collections.Generic;

namespace Server.Data
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderTimeStamp { get; set; }
        public List<CartItem> Cart { get; set; } = new List<CartItem>();
    }
}