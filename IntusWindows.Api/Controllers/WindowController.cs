using AutoMapper;
using IntusWindows.Common.Models;
using IntusWindows.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace IntusWindows.Api.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class WindowController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public WindowController(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        [HttpGet("for/{orderId:int}")]
        public async Task<ActionResult<IEnumerable<WindowDTO>>> GetWindows([FromRoute] int orderId)
        {
            try
            {
                var windows = await this.uow.windowRepository.GetByOrderID(orderId);

                if (windows == null)
                {
                    return NotFound();
                }
                else
                {
                    var windowDTOs = mapper.Map<IEnumerable<WindowDTO>>(windows);

                    return Ok(windowDTOs);
                }
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Sorry, we can't retrive windows at the moment.");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteWindow([FromRoute] int id)
        {
            try
            {
                await this.uow.windowRepository.Delete(id);
                await this.uow.SaveAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ErrorResponseDto("Sorry, we can't delete window at the moment."));
            }
        }
    }
}
