using Services.Catalog.Dtos;
using Services.Catalog.Models;
using Shared.Dtos;

namespace Services.Catalog.Services;

internal interface ICategoryService
{
    Task<Response<List<CategoryDto>>> GetAllAsync();
    Task<Response<CategoryDto>> CreateAsync(CategoryDto categoryDto);
    Task<Response<CategoryDto>> GetByIdAsync(string id);
}
