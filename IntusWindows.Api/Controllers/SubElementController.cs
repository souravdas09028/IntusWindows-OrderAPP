using AutoMapper;
using IntusWindows.Common.Models;
using IntusWindows.Core.Entities;
using IntusWindows.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace IntusWindows.Api.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class SubElementController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public SubElementController(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        [HttpGet("for/{windowId:int}")]
        public async Task<ActionResult<IEnumerable<SubElementDTO>>> GetByWindowID([FromRoute] int windowId)
        {
            try
            {
                var subElements = await this.uow.subElementRepository.GetByWindowID(windowId);

                if (subElements == null)
                {
                    return NotFound();
                }
                else
                {
                    var windowDTOs = mapper.Map<IEnumerable<SubElementDTO>>(subElements);

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

        [HttpPut]
        public async Task<ActionResult<IEnumerable<SubElementDTO>>> EditSubElement([FromBody] SubElementDTO subElementDTO)
        {
            try
            {
                SubElement subElement = mapper.Map<SubElement>(subElementDTO);
                subElement = await this.uow.subElementRepository.Update(subElement);
                subElementDTO = mapper.Map<SubElementDTO>(subElement);
                return Ok(subElementDTO);
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ErrorResponseDto("Sorry, we can't update sub element at the moment."));
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteSubElement([FromRoute] int id)
        {
            try
            {
                await this.uow.subElementRepository.Delete(id);
                await this.uow.SaveAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ErrorResponseDto("Sorry, we can't delete sub element at the moment."));
            }
        }
    }
}
