using System.Collections.Generic;

namespace FileUploader.Domain.Entities
{
    public class Article
    {
        public Article()
        {
            ArticlePrices = new HashSet<ArticlePrice>();
        }
        
        public string Code { get; set; }
        
        public string Label { get; set; }
        
        public string Description { get; set; }

        public ICollection<ArticlePrice> ArticlePrices { get; private set; }
    }
}
