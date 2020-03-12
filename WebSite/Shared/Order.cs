using System.Collections.Generic;

namespace Contoso.Online.Orders.Shared
{
    public class Order
    {
        public List<CartItem> Cart { get; set; } = new List<CartItem>();
        public int Id { get; set; }
    }
}