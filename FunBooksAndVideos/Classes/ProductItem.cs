using FunBooksAndVideos.Enums;

namespace FunBooksAndVideos.Classes
{
    public class ProductItem : IPurchaseItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Product.Type ProductType { get; set; }
    }
}
