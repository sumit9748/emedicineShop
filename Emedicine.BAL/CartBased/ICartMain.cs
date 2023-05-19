using Emedicine.DAL.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emedicine.BAL.CartBased
{
    public interface ICartMain
    {
        public Task<IEnumerable<Cart>> GGetCartByUserId(int userId);
        public Task<bool> AddCart(Cart cart);
        public void UpdateCart(Cart cart);
        public void DeleteCart(Cart cart);
        public Task<Cart> GetCartById(int id);
    }
}
