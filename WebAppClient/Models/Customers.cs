using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAppClient.Models
{
    public partial class Customers
    {
        public Customers()
        {
            Invoice = new HashSet<Invoice>();
        }
        [Key]
        [Display(Name = "Customer number")]
        public int Id { get; set; }
        [Required(ErrorMessage = "The name field is required.")]
        [Display(Name = "Customer Name")]
        public string CustName { get; set; }
        [Required(ErrorMessage = "The address field is required.")]
        public string Adress { get; set; }
        [Required(ErrorMessage = "Mandatory select state.")]
        public bool? Status { get; set; }
        public int CustomerTypeId { get; set; }
        [Display(Name = "Customer Type")]
        public virtual CustomerTypes CustomerType { get; set; }
        public virtual ICollection<Invoice> Invoice { get; set; }
    }
}
