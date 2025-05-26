using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_No_1.Models // CORRECT: Use your project's root namespace + .Models
{
    public class Customer
    {
        [Display(Name = "Commercial Invoice No.")]
        public string CommercialInvoiceNo { get; set; }

        [Display(Name = "Ship Date")]
        [DataType(DataType.Date)]
        public DateTime ShipDate { get; set; }

        [Display(Name = "Forwarder")]
        public string Forwarder { get; set; }

        [Display(Name = "B/L No.")]
        public string BLNo { get; set; }

        [Display(Name = "PO")]
        public string PO { get; set; }

        [Display(Name = "Line")]
        public string Line { get; set; }

        [Display(Name = "Part #")]
        public string PartNo { get; set; }

        [Display(Name = "Vendor Code")]
        public string VendorCode { get; set; }

        [Display(Name = "Qty")]
        public int Qty { get; set; }

        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }

        [Display(Name = "Amount")]
        public decimal Amount { get; set; }

        [Display(Name = "Currency")]
        public string Currency { get; set; }

        [Display(Name = "Finance Date")]
        [DataType(DataType.Date)]
        public DateTime FinanceDate { get; set; }

        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }
    }
}