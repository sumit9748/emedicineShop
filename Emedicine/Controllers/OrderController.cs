﻿using Emedicine.BAL.CartBased;
using Emedicine.BAL.OrderBased;
using Emedicine.DAL.model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Web.Mvc;
using ControllerBase = Microsoft.AspNetCore.Mvc.ControllerBase;
using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Emedicine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderMain ic;
        public OrderController(IOrderMain _ic)
        {
            ic = _ic;
        }
        [HttpPost("AddOrder")]
        public async Task<IActionResult> AddOrder([FromBody] IList<Cart> carts)
        {
            try
            {
               /* if(await ic.AddOrderItem(order,carts))
                {
                    if(await ic.AddOrder(order))
                    {
                        return StatusCode(
                       StatusCodes.Status200OK,
                       "order added successfully");
                    }
                    else
                    {
                        return StatusCode(
                       StatusCodes.Status403Forbidden,
                       "Order items are not found");
                    }
                    
                }
                else
               */return  StatusCode(
                       StatusCodes.Status403Forbidden,
                       " Format is wrong");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteOrder")]
        public async Task<IActionResult> deleteItems(int orderId)
        {
            try
            {

                 if(await ic.RemoveOrderItem(orderId))
                {
                    Order order=await ic.GetOrderById(orderId);
                    if(await ic.RemoveOrder(order))
                    {
                        return StatusCode(
                       StatusCodes.Status200OK,
                       "order deleted successfully");
                    }
                    else
                    {
                        return StatusCode(
                       StatusCodes.Status403Forbidden,
                       " Format is wrong");
                    }
                }
                else
                {
                    return StatusCode(
                       StatusCodes.Status403Forbidden,
                       " Format is wrong");
                }

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
