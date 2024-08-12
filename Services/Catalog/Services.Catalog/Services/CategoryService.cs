using AutoMapper;
using MongoDB.Driver;
using Services.Catalog.Dtos;
using Services.Catalog.Models;
using Services.Catalog.Settings;
using Shared.Dtos;

namespace Services.Catalog.Services;

public class CategoryService : ICategoryService
{
    private readonly IMongoCollection<Category> _categoryCollection;
    private readonly IMapper _mapper;

    public CategoryService(IMapper mapper, IDatabaseSettings databaseSettings)
    {
        var client = new MongoClient(databaseSettings.ConnectionString);
        var database = client.GetDatabase(databaseSettings.DatabaseName);
        _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);

        _mapper = mapper;

        //_categoryCollection = InitializeDatabase(databaseSettings);

    }

    public async Task<Response<List<CategoryDto>>> GetAllAsync()
    {
        var categories = await _categoryCollection.Find(category => true).ToListAsync();

        return Response<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(categories), StatusCodes.Status200OK);
    }

    public async Task<Response<CategoryDto>> CreateAsync(CategoryDto categoryDto)
    {
        var category = _mapper.Map<Category>(categoryDto);

       await _categoryCollection.InsertOneAsync(category);

        return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), StatusCodes.Status201Created);
    }

    public async Task<Response<CategoryDto>> GetByIdAsync(string id)
    {
        var category = await _categoryCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        if(category is null)
            return Response<CategoryDto>.Fail("Category not found", StatusCodes.Status404NotFound);

        return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), StatusCodes.Status200OK);
    }


    //private IMongoCollection<Category> InitializeDatabase(IDatabaseSettings databaseSettings)
    //{
    //    var client = new MongoClient(databaseSettings.ConnectionString);

    //    var database = client.GetDatabase(databaseSettings.DatabaseName);

    //    if (!database.ListCollectionNames().ToList().Contains(databaseSettings.CategoryCollectionName))
    //    {
    //        database.CreateCollection(databaseSettings.CategoryCollectionName);
    //    }

    //    return database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
    //}
}
