using LearnAPI.Repos;
using SignalRDemo3ytEFC.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace SignalRDemo3ytEFC.Repositories
{
    public class ProductRepository
    {
     
        private readonly LearndataContext dbContext;

        public ProductRepository(LearndataContext _dbContext)
        {
            this.dbContext = _dbContext;
        }

        public List<Product> GetProducts()
        {
            var prodList = dbContext.Product.ToList();
            foreach (var prod in prodList)
            {
                dbContext.Entry(prod).Reload();
            }
            //var f = dbContext.Product.ToList();
            return prodList;
        }

        public List<ProductForGraph> GetProductsForGraph()
        {
            List<ProductForGraph> productsForGraph = new List<ProductForGraph>();

            productsForGraph = dbContext.Product.GroupBy(p => p.Category)
                .Select(g => new ProductForGraph
                {
                                Category = g.Key,
                                Products = g.Count()
                            }).ToList();
            return productsForGraph;
        }
    }
}
