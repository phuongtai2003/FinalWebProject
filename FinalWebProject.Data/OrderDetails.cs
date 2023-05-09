using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalWebProject.Data
{
    public class OrderDetails
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int PhoneId { get; set; }
        public Phone Phone { get; set; }
        [Required]
        public int Quantity;
        [Required]
        public int Price;
    }
}
