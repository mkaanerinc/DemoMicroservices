﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Catalog.Dtos;
using Services.Catalog.Models;
using Services.Catalog.Services;
using Shared.ControllerBases;

namespace Services.Catalog.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : CustomBaseController
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _categoryService.GetAllAsync();

        return CreateActionResultInstance(response);

    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var response = await _categoryService.GetByIdAsync(id);

        return CreateActionResultInstance(response);

    }

    [HttpPost]
    public async Task<IActionResult> Create(CategoryDto categoryDto)
    {
        var response = await _categoryService.CreateAsync(categoryDto);

        return CreateActionResultInstance(response);

    }
}
