﻿using AutoMapper;
using MongoDB.Driver;
using Services.Catalog.Dtos;
using Services.Catalog.Models;
using Services.Catalog.Settings;
using Shared.Dtos;

namespace Services.Catalog.Services;

internal class CourseService : ICourseService
{
    private readonly IMongoCollection<Course> _courseCollection;
    private readonly IMongoCollection<Category> _categoryCollection;
    private readonly IMapper _mapper;

    public CourseService(IMapper mapper, IDatabaseSettings databaseSettings)
    {
        var client = new MongoClient(databaseSettings.ConnectionString);

        var database = client.GetDatabase(databaseSettings.DatabaseName);

        _courseCollection = database.GetCollection<Course>(databaseSettings.CourseCollectionName);
        _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);

        _mapper = mapper;
    }

    public async Task<Response<List<CourseDto>>> GetAllAsync()
    {
        var courses = await _courseCollection.Find(course => true).ToListAsync();

        if (courses.Any())
        {
            foreach (var course in courses)
            {
                course.Category = await _categoryCollection.Find(x => x.Id == course.CategoryId).FirstAsync();
            }
        }else
        {
            courses = new List<Course>();
        }

        return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), StatusCodes.Status200OK);
    }

    public async Task<Response<CourseDto>> GetByIdAsync(string id)
    {
        var course = await _courseCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        if (course is null)
            return Response<CourseDto>.Fail("Course not found",StatusCodes.Status404NotFound);

        course.Category = await _categoryCollection.Find(x => x.Id == course.CategoryId).FirstAsync();

        return Response<CourseDto>.Success(_mapper.Map<CourseDto>(course), StatusCodes.Status200OK);
    }

    public async Task<Response<List<CourseDto>>> GetAllByUserIdAsync(string userId)
    {
        var courses = await _courseCollection.Find(x => x.UserId == userId).ToListAsync();

        if (courses.Any())
        {
            foreach (var course in courses)
            {
                course.Category = await _categoryCollection.Find(x => x.Id == course.CategoryId).FirstAsync();
            }
        }
        else
        {
            courses = new List<Course>();
        }

        return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), StatusCodes.Status200OK);
    }

    public async Task<Response<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto)
    {
        var newCourse = _mapper.Map<Course>(courseCreateDto);

        newCourse.CreatedTime = DateTime.Now;

        await _courseCollection.InsertOneAsync(newCourse);

        return Response<CourseDto>.Success(_mapper.Map<CourseDto>(newCourse), StatusCodes.Status201Created);
    }

    public async Task<Response<NoContent>> CreateAsync(CourseUpdateDto courseUpdateDto)
    {
        var updateCourse = _mapper.Map<Course>(courseUpdateDto);

        var result = await _courseCollection.FindOneAndReplaceAsync(x => x.Id == courseUpdateDto.Id,updateCourse);

        if (result is null)
            return Response<NoContent>.Fail("Course not found",StatusCodes.Status404NotFound);

        return Response<NoContent>.Success(StatusCodes.Status204NoContent);
    }

    public async Task<Response<NoContent>> DeleteAsync(string id)
    {
        var result = await _courseCollection.DeleteOneAsync(x => x.Id == id);

        if(result.DeletedCount > 0)
        {
            return Response<NoContent>.Success(StatusCodes.Status204NoContent);
        }else
        {
            return Response<NoContent>.Fail("Course not found",StatusCodes.Status404NotFound);
        }
    }

}
