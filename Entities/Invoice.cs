namespace AlgorithmsWebAPI.Entities
{
    public class Invoice
    {
        public int InvoiceNumber { get; set; }
        public int SenderVKN { get; set; }
        public int RecieverVKN { get; set; }
        public int Amount { get; set; }
        public DateTime InvoiceDate { get; set; }
    }
}
