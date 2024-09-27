using DataBase.Models;
using Microsoft.AspNetCore.Mvc;
using TechnicalTest_API.DTO_s;

namespace TechnicalTest_API.Services
{
    public interface ISellService
    {
        Task<Sells> CreateSellAsync(SellDto sell);
        Task<Sells> GetSellByIdAsync(int id);
        Task<ActionResult<IEnumerable<Sells>>> GetAllSellsAsync();
        Task<bool> DeleteSellAsync(int id);
        Task<bool> UpdateSellAsync(int id, SellDto updatedSell);
    }
}
