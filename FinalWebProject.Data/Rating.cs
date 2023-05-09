using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalWebProject.Data
{
    public class Rating
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int PhoneId { get; set; }
        public Phone Phone { get; set; }
        [Required]
        public double ReviewRating { get; set; }
    }
}
