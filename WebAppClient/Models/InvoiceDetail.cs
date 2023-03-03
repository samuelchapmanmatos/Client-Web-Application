using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAppClient.Models
{
    public partial class InvoiceDetail
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Mandatory to select client.")]
        [Display(Name = "Customer number")]
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "The Qty field is required.")]
        [Display(Name = "Quantity")]
        public int Qty { get; set; }
        [Required(ErrorMessage = "The Price field is required.")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "The TotalItbis field is required.")]
        [Display(Name = "Itbis")]
        public decimal TotalItbis { get; set; }
        [Required(ErrorMessage = "The SubTotal field is required.")]
        [Display(Name = "SubTotal without itebis")]
        public decimal SubTotal { get; set; }
        [Required(ErrorMessage = "The Total field is required.")]
        public decimal Total { get; set; }

        public virtual Invoice Customer { get; set; }
    }
}
