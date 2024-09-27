using DataBase.Context;
using DataBase.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TechnicalTest_API.DTO_s;
using TechnicalTest_API.Services;

namespace TechnicalTest_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellsController : ControllerBase
    {
        private readonly ISellService _sellService;

        public SellsController(ISellService sellService)
        {
            _sellService = sellService;
        }

        [HttpGet, Authorize(Roles = "Noob, Admin")]
        public async Task<ActionResult<IEnumerable<Sells>>> GetAllSells()
        {
            var sells = await _sellService.GetAllSellsAsync();
            return Ok(sells); 
        }

        [HttpPost, Authorize(Roles = "Noob, Admin")]
        public async Task<ActionResult<Sells>> CreateSell(SellDto sell)
        {
            var newSell = await _sellService.CreateSellAsync(sell);
            if (newSell == null)
            {
                return BadRequest("Invalid sell request.");
            }

            return CreatedAtAction(nameof(GetSellById), new { id = newSell.SellId }, newSell);
        }

        [HttpGet("{id}"), Authorize(Roles = "Noob, Admin")]
        public async Task<ActionResult<Sells>> GetSellById(int id)
        {
            var sell = await _sellService.GetSellByIdAsync(id);

            if (sell == null)
            {
                return NotFound($"Sell with ID {id} not found.");
            }

            return Ok(sell);
        }

        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteSell(int id)
        {
            var result = await _sellService.DeleteSellAsync(id);
            if (!result)
            {
                return NotFound($"Sell with ID {id} not found.");
            }

            return NoContent();
        }

        [HttpPut("{id}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateSell(int id, SellDto updatedSell)
        {
            var result = await _sellService.UpdateSellAsync(id, updatedSell);
            if (!result)
            {
                return BadRequest("Invalid sell update request.");
            }

            return NoContent();
        }
    }


}

