﻿using Emedicine.DAL.DataAccess.Interface;
using Emedicine.DAL.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emedicine.BAL.CartBased
{
    public class CartMain : ICartMain
    {
        private readonly IDataAccess _da;
        public CartMain(IDataAccess da) 
        {
            _da = da;

        }
        public async Task<bool> AddCart(Cart cart)
        {
            if (cart == null)
            {
                return await Task.FromResult(false);
            }
            else
            {
                /*IEnumerable<Cart> carts = await _da.cart.GetAllListAsync(c=>c.UserId==cart.UserId);
                if (carts.Any(c =>c.MedicineId == cart.MedicineId))
                {
                    return await Task.FromResult(false);
                }*/
                Cart newCart = new Cart
                {
                    Id = cart.Id,
                    UserId = cart.UserId,
                    //user=cart.user,
                    Price=cart.Price,
                    Discount=cart.Discount,
                    Quantity=cart.Quantity,
                    MedicineId=cart.MedicineId,
                    //medicine=cart.medicine,
                    MedicalShopId=cart.MedicalShopId,
                    //medicicalshop=cart.medicicalshop,
                };
                _da.cart.AddAsync(newCart);
                _da.save();
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
    }
}
