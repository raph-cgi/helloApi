using HelloApi.Entities;
using HelloApi.Contexts;
using Microsoft.EntityFrameworkCore;

namespace HelloApi.Repositories
{
   public interface ITPersonRepository
    {
        Task<IEnumerable<TPersonEntity>> GetAllAsync();
        Task<TPersonEntity?> GetByIdAsync(int id);
        Task<TPersonEntity> AddAsync(TPersonEntity person);
        Task<TPersonEntity?> UpdateAsync(TPersonEntity person);
        Task<bool> DeleteAsync(int id);
    }

    public class TPersonRepository : ITPersonRepository
    {
        private readonly HelloApiContext _context;

        public TPersonRepository(HelloApiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TPersonEntity>> GetAllAsync()
        {
            return await _context.TPersons.ToListAsync();
        }

        public async Task<TPersonEntity?> GetByIdAsync(int id)
        {
            return await _context.TPersons.FindAsync(id);
        }

        public async Task<TPersonEntity> AddAsync(TPersonEntity person)
        {
            _context.TPersons.Add(person);
            await _context.SaveChangesAsync();
            return person;
        }

        public async Task<TPersonEntity?> UpdateAsync(TPersonEntity person)
        {
            var existing = await _context.TPersons.FindAsync(person.Id);
            if (existing == null)
                return null;

            existing.Nom = person.Nom;
            existing.Prenom = person.Prenom;
            existing.DateBorn = person.DateBorn;
            existing.DateDead = person.DateDead;
            existing.Nationalite = person.Nationalite;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var person = await _context.TPersons.FindAsync(id);
            if (person == null)
                return false;

            _context.TPersons.Remove(person);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
