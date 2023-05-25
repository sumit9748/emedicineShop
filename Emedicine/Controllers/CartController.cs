﻿

using Emedicine.BAL.CartBased;
using Emedicine.DAL.DataManupulation;
using Emedicine.DAL.model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Emedicine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartMain ic;
        public CartController(ICartMain _ic)
        {
            ic = _ic;
        }
        [HttpPost]
        public async Task<IActionResult> AddCart(CartVm cart)
        {
            try
            {
                if(cart == null)
                {
                    return BadRequest();
                }
                else
                {
                    if(await ic.AddCart(cart)){
                          return StatusCode(
                           StatusCodes.Status200OK,
                           "Cart added successfully");

                    }
                    else return StatusCode(
                       StatusCodes.Status406NotAcceptable,
                       "cart is already present");

                }
            }catch(Exception ex)
            {
                return StatusCode(
                       StatusCodes.Status502BadGateway,
                       "Something went seriously wrong");
            }
        }

        [HttpGet("{userId}")]
        public Task<IEnumerable<Cart>> GetCarts(int userId)
        {
            try
            {
                return ic.GGetCartByUserId(userId);

            }
            catch(Exception ex){
                IEnumerable<Cart> carts = new List<Cart>();
                return (Task<IEnumerable<Cart>>)carts;
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCart(int id, [FromBody] Cart cart)
        {
            try
            {
                Cart ct = await ic.GetCartById(id);
                if (ct == null) return NotFound();

                ct.Price = cart.Price;
                ct.Quantity = cart.Quantity;
                ct.Discount = cart.Discount;

                ic.UpdateCart(ct);
                return Ok("Cart updated successfully");
            }
            catch (Exception es)
            {
                return BadRequest("Cart cannot be added");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            try
            {
                Cart ct = await ic.GetCartById(id);
                if (ct == null) return NotFound();
                ic.DeleteCart(ct);
                return Ok("Cart removed successfully");
            }
            catch (Exception es)
            {
                return BadRequest("Error deleting cart item");
            }

        }
        [HttpGet("cartofUser/{userId}")]
        public async Task<IEnumerable<Medicine>> GetMedOfuserFromcart(int userId)
        {

              return (IEnumerable<Medicine>)await ic.getMedicinefromCart(userId);
            
        }
    }
}