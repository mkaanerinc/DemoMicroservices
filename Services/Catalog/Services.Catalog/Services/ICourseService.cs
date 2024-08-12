using Services.Catalog.Dtos;
using Shared.Dtos;

namespace Services.Catalog.Services;

internal interface ICourseService
{
    Task<Response<List<CourseDto>>> GetAllAsync();
    Task<Response<CourseDto>> GetByIdAsync(string id);
    Task<Response<List<CourseDto>>> GetAllByUserIdAsync(string userId);
    Task<Response<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto);
    Task<Response<NoContent>> CreateAsync(CourseUpdateDto courseUpdateDto);
    Task<Response<NoContent>> DeleteAsync(string id);
}
