using Emedicine.DAL.DataAccess.Interface;
using Emedicine.DAL.DataManupulation;
using Emedicine.DAL.model;


namespace Emedicine.BAL.CartBased
{
    public class CartMain : ICartMain
    {
        private readonly IDataAccess _da;
        public CartMain(IDataAccess da) 
        {
            _da = da;

        }
        public async Task<bool> AddCart(CartVm cart)
        {
            if (cart == null)
            {
                return await Task.FromResult(false);
            }
            else
            {
                IEnumerable<Cart> carts = await _da.cart.GetAllListAsync(c=>c.UserId==cart.UserId);
                
                foreach (var item in cart.Medicines)
                {
                    var medicine = await _da.medicine.GetFirstOrDefaultAsync(c => c.Id == item);
                    if (carts.Any(c => c.MedicineId == item)) continue;
                    
                    Cart newCart = new Cart
                    {

                        UserId = cart.UserId,
                        Price=medicine.UnitPrice* cart.Quantity,
                        Discount=medicine.Discount,
                        Quantity=cart.Quantity,
                        MedicineId=item,
                        MedicalShopId=cart.MedicalShopId,

                    };
                    _da.cart.AddAsync(newCart);
                    _da.save();
                }
                return await Task.FromResult(true);
            }
        }

        public void DeleteCart(Cart cart)
        {
            _da.cart.Remove(cart);
            _da.save();
        }


        public async Task<IEnumerable<Cart>> GGetCartByUserId(int userId)
        {
            return await _da.cart.GetAllListAsync(c=>c.UserId==userId);
        }

        public void UpdateCart(Cart cart)
        {
            _da.cart.UpdateExisting(cart);
            _da.save();
        }
        public async Task<Cart> GetCartById(int id)
        {
            return await _da.cart.GetFirstOrDefaultAsync(c=>c.Id==id);
        }
        public async Task<IEnumerable<Medicine>> getMedicinefromCart(int userId)
        {
            return await _da.cart.GetMedicinesByUserfromcart(userId);
        }

        
    }
}
