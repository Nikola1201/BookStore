using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Repository
{
    public class ProductRepository: Repository<Product>, IProductRepository
    {
     
            private readonly ApplicationDbContext _db;
            public ProductRepository(ApplicationDbContext db) : base(db)
            {
                _db = db;
            }
            public void Update(Product obj)
            {
            var objFromDb = _db.Products.SingleOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Title = obj.Title;
                objFromDb.Description = obj.Description;
                objFromDb.ISBN = obj.ISBN;
                objFromDb.Author = obj.Author;
                objFromDb.Price= obj.Price;
                objFromDb.Price50= obj.Price50;
                objFromDb.ListPrice = obj.ListPrice;
                objFromDb.Price100=obj.Price100;
                obj.CategoryId= obj.CategoryId;
                obj.CoverId=obj.CoverId;
                if (obj.ImageURL != null)
                {
                    objFromDb.ImageURL = obj.ImageURL;
                }
            }
                
            }

       
    }
}
