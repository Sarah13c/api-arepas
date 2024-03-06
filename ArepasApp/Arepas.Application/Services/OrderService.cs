using Arepas.Application.Interfaces;
using Arepas.Domain.Dtos;
using Arepas.Domain.Exceptions;
using Arepas.Domain.Interfaces.Repositories;
using Arepas.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Arepas.Application.Services;

public class OrderService : IOrderService
{

    private readonly IOrderRepository _orderRepository;

    public bool DbUpdateException { get; private set; }

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Order> AddAsync(Order entity)
    {
        return await _orderRepository.AddAsync(entity);
    }

    public async Task<IEnumerable<Order>> FindAsync(Expression<Func<Order, bool>> predicate)
    {
        return await _orderRepository.FindAsync(predicate);
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        return await _orderRepository.GetAllAsync();
    }

    public async Task<Order> GetByIdAsync(int id)
    {
        var entity = await _orderRepository.GetByIdAsync(id);

        if (entity is null)
        {
            throw new NotFoundException($"Registro con Id={id} No Encontrado");
        }
        if (DbUpdateException)
        {
            throw new BadRequestException($"Registro con Id={id} No se pudo eliminar");
        }

        return entity;
    }

    public async Task<ResponseData<Order>> GetByQueryParamsAsync(QueryParams queryParams)
    {
        return await _orderRepository.GetByQueryParamsAsync(queryParams);
    }


    public async Task RemoveAsync(int id)
    {
        try
        {
            var entity = await _orderRepository.GetByIdAsync(id);

            if (entity is null)
            {
                throw new NotFoundException($"Registro con Id={id} No Encontrado");
            }

            await _orderRepository.RemoveAsync(entity);
        }
        catch (DbUpdateException)
        {
            throw new BadRequestException($"Registro con Id={id} No se pudo eliminar");
        }
    }

    public async Task<Order> UpdateAsync(int id, Order entity)
    {
        if (id != entity.Id)
        {
            throw new BadRequestException($"El Id={id} No Corresponde con el Id={entity.Id} del Registro");
        }

        var order = await _orderRepository.GetByIdAsync(id);

        if (entity is null)
        {
            throw new NotFoundException($"Registro con Id={id} No Encontrado");
        }

        await _orderRepository.UpdateAsync(entity);

        return entity;
    }

    public async Task<IEnumerable<OrderDetail>> GetOrdersDetailByOrderIdAsync(int id)
    {
        return await _orderRepository.GetOrdersDetailByOrderIdAsync(id);
    }

    public async Task<IEnumerable<OrderDetailProduct>> GetOrderDetailProductsByOrderIdAsync(int id)
    {
        return await _orderRepository.GetOrderDetailProductsByOrderIdAsync(id);
    }
}