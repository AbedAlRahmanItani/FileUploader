namespace FileUploader.Application.ViewModels
{
    public class ArticlePriceViewModel
    {
        public string Key { get; set; }
        
        public decimal Price { get; set; }
        
        public decimal DiscountPrice { get; set; }
        
        public string ArticleCode { get; set; }
        
        public string DeliveredInCode { get; set; }
        
        public string SectionCode { get; set; } // Q1 column in the file
        
        public string SizeCode { get; set; }
        
        public string ColorCode { get; set; }
    }
}