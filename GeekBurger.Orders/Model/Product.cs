using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GeekBurger.Orders.Model
{
    public class Product
    {
       
        public Guid OrderId { get; set; }
        [Key]
        public Guid ProductId { get; set; }
        public Decimal Price { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
    }
}
