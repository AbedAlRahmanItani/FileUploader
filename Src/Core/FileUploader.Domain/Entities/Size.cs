using System.Collections.Generic;

namespace FileUploader.Domain.Entities
{
    public class Size
    {
        public Size()
        {
            ArticlePrices = new HashSet<ArticlePrice>();
        }
        
        public string Code { get; set; }

        public ICollection<ArticlePrice> ArticlePrices { get; private set; }
    }
}