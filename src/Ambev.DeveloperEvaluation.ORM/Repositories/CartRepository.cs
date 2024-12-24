using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly DefaultContext _context;

        public CartRepository(DefaultContext context)
        {
            _context = context;
        }

        public async Task<Cart> CreateAsync(Cart cart, CancellationToken cancellationToken = default)
        {
            await _context.Carts.AddAsync(cart, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return cart;
        }

        public async Task<Cart> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context
                         .Carts
                         .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Cart>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var carts = await _context
                             .Carts
                             .AsNoTracking()
                             .ToListAsync();

            return carts;
        }

        public async Task<bool> UpdateAsync(Cart sale, CancellationToken cancellationToken = default)
        {
            var cart = await GetByIdAsync(sale.Id, cancellationToken);
            if (cart == null)
                return false;

            _context.Carts.Update(cart);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var cart = await GetByIdAsync(id, cancellationToken);
            if (cart == null)
                return false;

            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
