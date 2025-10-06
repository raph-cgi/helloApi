using HelloApi.Entities;
using HelloApi.Contexts;
using Microsoft.EntityFrameworkCore;

namespace HelloApi.Repositories
{
    public interface ITStockPartRepository
    {
        Task<IEnumerable<TStockPartEntity>> GetAllAsync();
        Task<TStockPartEntity?> GetByIdAsync(int id);
        Task<TStockPartEntity?> GetByPartCodeAsync(string partCode);
        Task<TStockPartEntity> AddAsync(TStockPartEntity part);
        Task<TStockPartEntity?> UpdateAsync(TStockPartEntity part);
        Task<bool> DeleteAsync(int id);
    }


    public class TStockPartRepository : ITStockPartRepository
    {
        private readonly HelloApiContext _context;


        public TStockPartRepository(HelloApiContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<TStockPartEntity>> GetAllAsync()
        {
            return await _context.TStockParts.AsNoTracking().ToListAsync();
        }


        public async Task<TStockPartEntity?> GetByIdAsync(int id)
        {
            return await _context.TStockParts.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }


        public async Task<TStockPartEntity?> GetByPartCodeAsync(string partCode)
        {
            return await _context.TStockParts.AsNoTracking().FirstOrDefaultAsync(p => p.PartCode == partCode);
        }


        public async Task<TStockPartEntity> AddAsync(TStockPartEntity part)
        {
            _context.TStockParts.Add(part);
            await _context.SaveChangesAsync();
            return part;
        }


        public async Task<TStockPartEntity?> UpdateAsync(TStockPartEntity part)
        {
            var existing = await _context.TStockParts.FirstOrDefaultAsync(p => p.Id == part.Id);
            if (existing == null)
                return null;


            // Copy incoming values onto the tracked entity (covers all scalar properties safely)
            _context.Entry(existing).CurrentValues.SetValues(part);


            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var stockpart = await _context.TStockParts.FindAsync(id);
            if (stockpart == null)
                return false;

            _context.TStockParts.Remove(stockpart);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
