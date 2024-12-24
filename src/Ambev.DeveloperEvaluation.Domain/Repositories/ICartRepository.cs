using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface ICartRepository
    {
        Task<Cart> CreateAsync(Cart sale, CancellationToken cancellationToken = default);
        Task<Cart> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Cart>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(Cart sale, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
