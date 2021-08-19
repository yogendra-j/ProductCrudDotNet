using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Data
{
    public class ProductSqlData
    {
        private readonly ProductDbContext db;

        public ProductSqlData(ProductDbContext db)
        {
            this.db = db;
        }

        public int Commit()
        {
            return db.SaveChanges();
        }
        public Product Add(Product newProduct)
        {
            db.Add(newProduct);
            return newProduct;
        }

        public Product Update(Product newProduct)
        {
            var entity = db.Products.Attach(newProduct);
            entity.State = EntityState.Modified;
            return newProduct;
        }
        public Product Delete(int id)
        {
            var product = GetById(id);
            if (product != null)
            {
                db.Products.Remove(product);
            }
            return product;
        }

        public Product GetById(int id)
        {
            return db.Products.Find(id);
        }

        public IEnumerable<Product> GetProducts(int batchSize, int batchNumber)
        {
            var query = from r in db.Products
                        orderby r.CreatedOn
                        select r;
            return query.Skip(batchSize * (batchNumber - 1)).Take(batchSize);
        }

        public int GetCount()
        {
            return db.Products.Count();
        }
    }
}
