using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly ApplicationDbContext _db;

        public OrderHeaderRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
        public void Update(OrderHeader obj)
        {
            _db.OrderHeader.Update(obj);
        }

        public void UpdateStatus(int id, string orderStatus, string? paymentIntentId=null, string? paymentStatus = null)
        {
          var orderFromDb=_db.OrderHeader.SingleOrDefault(x => x.Id == id);
            if(orderFromDb != null)
            {
                orderFromDb.OrderStatus = orderStatus;
                if (paymentStatus != null)
                {
                    orderFromDb.PaymentStatus = paymentStatus;
                }
                if(paymentIntentId != null)
                {
                    orderFromDb.PaymentIntentId = paymentIntentId;
                }
            }
          
        }
        public void UpdateStripePaymentId(int id, string sessionId)
        {
            var orderFromDb = _db.OrderHeader.SingleOrDefault(x => x.Id == id);

            orderFromDb.PaymentDate= DateTime.Now;
            orderFromDb.SessionId = sessionId;
        }
    }
}
