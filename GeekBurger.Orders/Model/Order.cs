using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GeekBurger.Orders.Model
{
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; }
        public Guid StoreId { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<Production> Productions { get; set; }
    }
}
