using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalWebProject.Data
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string CustomerEmail { get; set; }
        [Required]
        public string CustomerPassword { get; set; }
        [Required]
        public string CustomerAddress { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
