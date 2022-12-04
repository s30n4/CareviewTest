namespace CareviewTest.Models
{
    public class InvoiceLineItem
    {
        public int Id { get; set; }
        public decimal Quantity { get; set; }
        public Service Service { get; set; }
        public Invoice Invoice { get; set; }
        public int InvoiceId { get; set; }

    }
}
