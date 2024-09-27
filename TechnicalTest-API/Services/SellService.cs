using DataBase.Context;
using DataBase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechnicalTest_API.DTO_s;

namespace TechnicalTest_API.Services
{
    public class SellService : ISellService
    {
        private readonly TechnicalTestContext _context;

        public SellService(TechnicalTestContext context)
        {
            _context = context;
        }

        public async Task<Sells> CreateSellAsync(SellDto sell)
        {
            var item = await _context.Items.FirstOrDefaultAsync(e => e.ItemId == sell.ItemId);
            if (item == null || !IsValidSell(sell.Quantity, item.Stock))
            {
                return null;
            }

            var newSell = new Sells
            {
                ItemId = sell.ItemId,
                Quantity = sell.Quantity,
                Total = item.Price * sell.Quantity
            };

            item.Stock -= sell.Quantity;
            _context.Sells.Add(newSell);
            await _context.SaveChangesAsync();

            return newSell;
        }

        public async Task<Sells> GetSellByIdAsync(int id)
        {
            return await _context.Sells.AsNoTracking().FirstOrDefaultAsync(e => e.SellId == id);
        }

        public async Task<bool> DeleteSellAsync(int id)
        {
            var sell = await _context.Sells.FindAsync(id);
            if (sell == null) return false;

            var item = await _context.Items.FirstOrDefaultAsync(e => e.ItemId == sell.ItemId);
            if (item == null) return false;

            item.Stock += sell.Quantity;
            _context.Sells.Remove(sell);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateSellAsync(int id, SellDto updatedSell)
        {
            var existingSell = await _context.Sells.FirstOrDefaultAsync(e => e.SellId == id);
            if (existingSell == null) return false;

            var item = await _context.Items.FirstOrDefaultAsync(e => e.ItemId == updatedSell.ItemId);
            if (item == null) return false;

            item.Stock += existingSell.Quantity;
            if (!IsValidSell(updatedSell.Quantity, item.Stock))
            {
                return false;
            }

            item.Stock -= updatedSell.Quantity;

            existingSell.Quantity = updatedSell.Quantity;
            existingSell.Total = updatedSell.Quantity * item.Price;

            await _context.SaveChangesAsync();
            return true;
        }

        private bool IsValidSell(int quantity, int stock)
        {
            return quantity <= stock;
        }

        public async Task<ActionResult<IEnumerable<Sells>>> GetAllSellsAsync()
        {
            return await _context.Sells.AsNoTracking().ToListAsync();
        }
    }

}
