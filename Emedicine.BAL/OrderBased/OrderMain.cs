using Emedicine.BAL.CartBased;
using Emedicine.DAL.DataAccess.Interface;
using Emedicine.DAL.DataManupulation;
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
        public async Task<bool> AddOrder(OrderVm order)
        {
            if (order == null)
            {
                return await Task.FromResult(false);
            }
            else
            {
                double orderTotal = 0, discount = 0;
                foreach(var item in order.carts)
                {
                    orderTotal += item.Price;
                    discount += item.Discount;
                }
                orderTotal = (orderTotal * (100 - discount)) / 100;

                Order newOrder = new Order
                {
                    UserId = order.UserId,
                    OrderTotal = (decimal)orderTotal,
                    OrderStatus = "pending",
                };
                _da.order.AddAsync(newOrder);
                _da.save();
                foreach (var item in order.carts)
                {
                    var orderItem = new OrderItem()
                    {

                        MedicineId = item.MedicineId,
                        OrderId = newOrder.Id,
                    };
                    _da.orderItem.AddAsync(orderItem);
                    _da.save();
                };
                
                
                
                return await Task.FromResult(true);
            }
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
        public async Task<Order> kireOrderDibi(int id)
        {
            return await _da.order.GetorderById(id);
        }
        
    }
}
