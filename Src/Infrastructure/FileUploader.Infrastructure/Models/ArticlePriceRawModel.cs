using System;

namespace FileUploader.Infrastructure.Models
{
    internal class ArticlePriceRawModel
    {
        public string Key { get; set; }
        public string ArticleCode { get; set; }
        public string ArticleLabel { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; }
        public string DeliveredIn { get; set; }
        public string Q1 { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }

        public static ArticlePriceRawModel FromCsv(string csvLine)
        {
            var values = csvLine.Split(',');
            var rawModel = new ArticlePriceRawModel
            {
                Key = values[0],
                ArticleCode = values[1],
                ArticleLabel = values[2],
                Description = values[3],
                Price = Convert.ToDecimal(values[4]),
                DiscountPrice = Convert.ToDecimal(values[5]),
                DeliveredIn = values[6],
                Q1 = values[7],
                Size = values[8],
                Color = values[9]
            };
            return rawModel;
        }
    }
}
