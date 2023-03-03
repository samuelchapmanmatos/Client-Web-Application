using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAppClient.Models
{
    public partial class CustomerTypes
    {
        public CustomerTypes()
        {
            Customers = new HashSet<Customers>();
        }
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Description field is required.")]
        public string Description { get; set; }

        public virtual ICollection<Customers> Customers { get; set; }
    }
}
