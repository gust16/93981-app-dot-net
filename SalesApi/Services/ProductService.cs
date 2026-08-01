using SalesApi.Models;
using SalesApi.Repositories;

namespace SalesApi.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Product>> GetAllAsync()
        => _repository.GetAllAsync();

    public Task<Product?> GetByIdAsync(int id)
        => _repository.GetByIdAsync(id);

    public Task<Product> CreateAsync(Product product)
        => _repository.CreateAsync(product);

    public Task<Product?> UpdateAsync(int id, Product product)
        => _repository.UpdateAsync(id, product);

    public Task<bool> DeleteAsync(int id)
        => _repository.DeleteAsync(id);
}
