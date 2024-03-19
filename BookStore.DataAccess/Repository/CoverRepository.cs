using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;

namespace BookStore.DataAccess.Repository
{
    public class CoverRepository :Repository<Cover>, ICoverRepository
    {
        private readonly ApplicationDbContext _db;

        public CoverRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
        public void Update(Cover obj)
        {
            _db.Covers.Update(obj);
        }
    }
}