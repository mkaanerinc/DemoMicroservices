﻿using Services.Basket.Dtos;
using Shared.Dtos;
using System.Text.Json;

namespace Services.Basket.Services;

public class BasketService : IBasketService
{
    private readonly RedisService _redisService;

    public BasketService(RedisService redisService)
    {
        _redisService = redisService;
    }

    public async Task<Response<bool>> Delete(string userId)
    {
        var status = await _redisService.GetDb().KeyDeleteAsync(userId);

        return status ? Response<bool>.Success(StatusCodes.Status204NoContent) :
            Response<bool>.Fail("Basket not found", StatusCodes.Status404NotFound);
    }

    public async Task<Response<BasketDto>> GetBasket(string userId)
    {
        var existBasket = await _redisService.GetDb().StringGetAsync(userId);

        if (String.IsNullOrEmpty(existBasket))
        {
            return Response<BasketDto>.Fail("Basket not found",StatusCodes.Status404NotFound);
        }

        return Response<BasketDto>.Success(JsonSerializer.Deserialize<BasketDto>(existBasket), StatusCodes.Status200OK);
    }

    public async Task<Response<bool>> SaveOrUpdate(BasketDto basketDto)
    {
        var status = await _redisService.GetDb().StringSetAsync(basketDto.UserId,JsonSerializer.Serialize(basketDto));

        return status ? Response<bool>.Success(StatusCodes.Status204NoContent) : 
            Response<bool>.Fail("Basket could not update or save", StatusCodes.Status500InternalServerError);
    }
}
