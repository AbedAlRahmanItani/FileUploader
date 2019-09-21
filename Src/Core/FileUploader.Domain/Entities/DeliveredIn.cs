using System.Collections.Generic;

namespace FileUploader.Domain.Entities
{
    public class DeliveredIn
    {
        public DeliveredIn()
        {
            ArticlePrices = new HashSet<ArticlePrice>();
        }
        
        public string Code { get; set; }

        public ICollection<ArticlePrice> ArticlePrices { get; private set; }
    }
}
