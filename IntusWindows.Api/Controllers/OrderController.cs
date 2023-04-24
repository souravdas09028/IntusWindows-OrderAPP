using AutoMapper;
using IntusWindows.Common;
using IntusWindows.Common.Models;
using IntusWindows.Core.Entities;
using IntusWindows.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Serilog;

namespace IntusWindows.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public OrderController(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrders()
        {
            try
            {
                var orders = await this.uow.ordrRepository.GetAll();

                if (orders == null)
                {
                    return NotFound();
                }
                else
                {
                    var orderDTOs = mapper.Map<IEnumerable<OrderDTO>>(orders);

                    return Ok(orderDTOs);
                }
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ErrorResponseDto("Sorry, we can't retrive orders at the moment."));
            }
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> AddOrder([FromBody] OrderDTO orderDTO)
        {
            try
            {
                var order = mapper.Map<Order>(orderDTO);
                this.uow.ordrRepository.Add(order);
                await this.uow.SaveAsync();
                return Ok(orderDTO);
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ErrorResponseDto("Sorry, we can't create order at the moment."));
            }
        }

        [HttpPut]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> EditOrder([FromBody] OrderDTO orderDTO)
        {
            try
            {
                var order = mapper.Map<Order>(orderDTO);
                await this.uow.ordrRepository.Update(order);
                await this.uow.SaveAsync();
                return Ok(orderDTO);
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ErrorResponseDto("Sorry, we can't update order at the moment."));
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteOrder([FromRoute] int id)
        {
            try
            {
                await this.uow.ordrRepository.Delete(id);
                await this.uow.SaveAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ErrorResponseDto("Sorry, we can't delete order at the moment."));
            }
        }
    }
}
