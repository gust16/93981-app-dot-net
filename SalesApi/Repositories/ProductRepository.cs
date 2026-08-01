using SalesApi.Models;

namespace SalesApi.Repositories;

public class ProductRepository : IProductRepository
{
    // In-memory store — replace with DbContext (EF Core) when DB is wired up.
    private readonly List<Product> _store = [];
    private int _nextId = 1;

    public Task<IEnumerable<Product>> GetAllAsync()
        => Task.FromResult<IEnumerable<Product>>(_store);

    public Task<Product?> GetByIdAsync(int id)
        => Task.FromResult(_store.FirstOrDefault(p => p.Id == id));

    public Task<Product> CreateAsync(Product product)
    {
        product.Id = _nextId++;
        product.CreatedAt = DateTime.UtcNow;
        _store.Add(product);
        return Task.FromResult(product);
    }

    public Task<Product?> UpdateAsync(int id, Product product)
    {
        var existing = _store.FirstOrDefault(p => p.Id == id);
        if (existing is null) return Task.FromResult<Product?>(null);

        existing.Name        = product.Name;
        existing.Description = product.Description;
        existing.Price       = product.Price;
        existing.Stock       = product.Stock;
        existing.UpdatedAt   = DateTime.UtcNow;

        return Task.FromResult<Product?>(existing);
    }

    public Task<bool> DeleteAsync(int id)
    {
        var existing = _store.FirstOrDefault(p => p.Id == id);
        if (existing is null) return Task.FromResult(false);

        _store.Remove(existing);
        return Task.FromResult(true);
    }
}
