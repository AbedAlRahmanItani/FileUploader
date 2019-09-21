using System.Collections.Generic;

namespace FileUploader.Domain.Entities
{
    public class Color
    {
        public Color()
        {
            ArticlePrices = new HashSet<ArticlePrice>();
        }
        
        public string Code { get; set; }

        public ICollection<ArticlePrice> ArticlePrices { get; private set; }
    }
}
