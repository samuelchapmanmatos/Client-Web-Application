using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAppClient.Models
{
    public partial class Invoice
    {
        public Invoice()
        {
            InvoiceDetail = new HashSet<InvoiceDetail>();
        }
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Mandatory to select client.")]
        [Display(Name = "Customer number")]
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "The field TotalItbis is required.")]
        [Display(Name = "Itbis")]
        public decimal TotalItbis { get; set; }
        [Required(ErrorMessage = "The field SubTotal is required.")]
        [Display(Name = "SubTotal without itebis")]
        public decimal SubTotal { get; set; }
        [Required(ErrorMessage = "The field Total is required.")]
        public decimal Total { get; set; }

        public virtual Customers Customer { get; set; }
        public virtual ICollection<InvoiceDetail> InvoiceDetail { get; set; }
    }
}
