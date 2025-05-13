using AutoMapper;
using ComputerStoreAPI.Data;
using ComputerStoreAPI.DTOs;
using ComputerStoreAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace ComputerStoreAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ProductService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        public async Task<List<ProductResponseDto>> GetAllProductsAsync()
        {
            var products = await _context.Products.ToListAsync();
            return _mapper.Map<List<ProductResponseDto>>(products);
        }

        public async Task<ProductResponseDto?> GetProductByIdAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            return product == null ? null : _mapper.Map<ProductResponseDto>(product);
        }

        public async Task<List<ProductResponseDto>> CreateProductAsync(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return await GetAllProductsAsync();
        }

        public async Task<List<ProductResponseDto>> UpdateProductAsync(int id, ProductDto productDto)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return null;

            _mapper.Map(productDto, product);
            await _context.SaveChangesAsync();

            return await GetAllProductsAsync();
        }

        public async Task<List<ProductResponseDto>> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return null;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return await GetAllProductsAsync();
        }
    }
}
