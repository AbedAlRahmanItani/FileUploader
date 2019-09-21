using System.Collections.Generic;

namespace FileUploader.Domain.Entities
{
    public class Section
    {
        public Section()
        {
            ArticlePrices = new HashSet<ArticlePrice>();
        }
        
        public string Code { get; set; }

        public ICollection<ArticlePrice> ArticlePrices { get; set; }
    }
}
