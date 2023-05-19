using Emedicine.BAL.CartBased;
using Emedicine.DAL.DataAccess.Interface;
using Emedicine.DAL.model;

namespace Emedicine.BAL.OrderBased
{
    public class OrderMain : IOrderMain
    {
        private readonly IDataAccess _da;
        public OrderMain(IDataAccess da) 
        {
            _da = da;
        }
        public async Task<bool> AddOrder(Order order)
        {
            if (order == null)
            {
                return await Task.FromResult(false);
            }
            else
            {
                
                _da.order.AddAsync(order);
                
                _da.save();
                return await Task.FromResult(true);
            }
        }

        public async Task<bool> AddOrderItem(Order order,IList<Cart> cartItem)
        {
            if(order==null || cartItem.Count==0)
            {
                return  await Task.FromResult(false);
            }
            foreach(var item in cartItem)
            {
                OrderItem ori = new OrderItem();
                ori.OrderId = order.Id;
                ori.order=order;
                ori.MedicineId = item.MedicineId;
                ori.medicine = item.medicine;
                _da.orderItem.AddAsync(ori);
            }
            return await Task.FromResult(true);

        }

        public async Task<Order> GetOrderById(int orderId)
        {
            return await _da.order.GetFirstOrDefaultAsync(c=>c.Id==orderId);
        }

        public async Task<bool> RemoveOrder(Order order)
        {
            if(order==null)
            {
                return await Task.FromResult(false);
            }
           _da.order.Remove(order);
           _da.save();
            return await Task.FromResult(true);

        }

        public async Task<bool> RemoveOrderItem(int orderId)
        {
            IEnumerable<OrderItem> orderItems= await _da.orderItem.GetAllListAsync(o=>o.OrderId == orderId);
            if(orderItems.Count()==0) return await Task.FromResult(false);
            foreach(var item in orderItems)
            {
                _da.orderItem.Remove(item);
            }
            return await Task.FromResult(true);
        }
        public async Task<IEnumerable<OrderItem>> DeleteOrderItems(int orderId)
        {
            return await _da.orderItem.GetAllListAsync(c=>c.OrderId==orderId);
        }
        
    }
}
