namespace FunBooksAndVideos.OrderItems
{
    public class PurchaseOrder
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public decimal TotalPrice { get; set; }
        public List<IPurchaseItem> PurchaseItems { get; set; } = new List<IPurchaseItem>();
    }
}
