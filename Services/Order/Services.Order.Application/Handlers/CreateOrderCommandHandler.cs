using MediatR;
using Microsoft.AspNetCore.Http;
using Services.Order.Application.Commands;
using Services.Order.Application.Dtos;
using Services.Order.Domain.OrderAggregate;
using Services.Order.Infrastructure;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Order.Application.Handlers;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Response<CreatedOrderDto>>
{
    private readonly OrderDbContext _context;

    public CreateOrderCommandHandler(OrderDbContext context)
    {
        _context = context;
    }

    public async Task<Response<CreatedOrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var newAddress = new Address(request.AddressDto.Province,request.AddressDto.District,
            request.AddressDto.Street,request.AddressDto.ZipCode,request.AddressDto.Line);

        Domain.OrderAggregate.Order newOrder = new(request.BuyerId, newAddress);

        request.OrderItems.ForEach(x =>
        {
            newOrder.AddOrderItem(x.ProductId, x.ProductName, x.Price, x.PictureUrl);
        });

        await _context.Orders.AddAsync(newOrder,cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return Response<CreatedOrderDto>.Success(new CreatedOrderDto { OrderId = newOrder.Id}, StatusCodes.Status201Created);
    }
}
