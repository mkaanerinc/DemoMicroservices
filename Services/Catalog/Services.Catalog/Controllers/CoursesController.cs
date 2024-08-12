﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Catalog.Dtos;
using Services.Catalog.Services;
using Shared.ControllerBases;

namespace Services.Catalog.Controllers;

[Route("api/[controller]")]
[ApiController]
internal class CoursesController : CustomBaseController
{
    private readonly ICourseService _courseService;

    public CoursesController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    public async Task<IActionResult> GetAll()
    {
        var response = await _courseService.GetAllAsync();

        return CreateActionResultInstance(response);

    }

    [HttpGet("{id}")]  // courses?4 => courses/4
    public async Task<IActionResult> GetById(string id)
    {
        var response = await _courseService.GetByIdAsync(id);

        return CreateActionResultInstance(response);

    }

    [Route("api/[controller]/GetAllByUserId/{userId}")]
    public async Task<IActionResult> GetAllByUserId(string userId)
    {
        var response = await _courseService.GetAllByUserIdAsync(userId);

        return CreateActionResultInstance(response);

    }

    [HttpPost]
    public async Task<IActionResult> Create(CourseCreateDto courseCreateDto)
    {
        var response = await _courseService.CreateAsync(courseCreateDto);

        return CreateActionResultInstance(response);

    }

    [HttpPut]
    public async Task<IActionResult> Update(CourseUpdateDto courseUpdateDto)
    {
        var response = await _courseService.UpdateAsync(courseUpdateDto);

        return CreateActionResultInstance(response);

    }

    [HttpDelete]
    public async Task<IActionResult> Delete(string id)
    {
        var response = await _courseService.DeleteAsync(id);

        return CreateActionResultInstance(response);

    }
}
