using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalWebProject.Data
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [Required]
        public int TotalPrice { get; set; }
        [Required]
        public DateTime OrderedAt { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
