namespace FunBooksAndVideos.OrderItems
{
    public interface IPurchaseItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
