
namespace FileUploader.Domain.Entities
{
    public class ArticlePrice
    {
        public string Key { get; set; }
        
        public decimal Price { get; set; }
        
        public decimal DiscountPrice { get; set; }
        
        public string ArticleCode { get; set; }
        
        public string DeliveredInCode { get; set; }
        
        public string SectionCode { get; set; } // Q1 column in the file
        
        public string SizeCode { get; set; }
        
        public string ColorCode { get; set; }

        public Article Article { get; set; }
        public DeliveredIn DeliveredIn { get; set; }
        public Section Section { get; set; }
        public Size Size { get; set; }
        public Color Color { get; set; }
    }
}
