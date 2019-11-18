using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models
{
    public class Hotel
    {
        public int ID { get; set; }
        [Required]
        [DisplayName("Hotel Name")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Street Address")]
        public string StreetAddress { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        [RegularExpression(@"\d{3}-\d{3}-\d{4}")]
        public string Phone { get; set; }

        public ICollection<HotelRoom> HotelRoom { get; set; }
    }
}
