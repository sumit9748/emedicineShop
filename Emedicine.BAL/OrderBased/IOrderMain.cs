using Emedicine.DAL.model;

namespace Emedicine.BAL.OrderBased
{
    public interface IOrderMain
    {
        public Task<bool> AddOrder(Order order);
        public Task<bool> RemoveOrder(Order order);
        public Task<bool> RemoveOrderItem(int orderId);
        public Task<bool> AddOrderItem(Order order,IList<Cart> cartItem);
        public Task<IEnumerable<OrderItem>> DeleteOrderItems(int orderId);
        public Task<Order> GetOrderById(int orderId);
    }
}
