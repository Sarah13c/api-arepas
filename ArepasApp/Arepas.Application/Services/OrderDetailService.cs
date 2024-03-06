using Arepas.Application.Interfaces;
using Arepas.Domain.Dtos;
using Arepas.Domain.Exceptions;
using Arepas.Domain.Interfaces.Repositories;
using Arepas.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace Arepas.Application.Services
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;

        public OrderDetailService(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }

        public bool DbUpdateException { get; private set; }

        public async Task<OrderDetail> AddAsync(OrderDetail entity)
        {
            if (entity.Quantity < 0)
            {
                throw new BadRequestException($"La Cantidad debe ser Mayor a Cero");
            }
            return await _orderDetailRepository.AddAsync(entity);
        }

        public async Task<IEnumerable<OrderDetail>> FindAsync(Expression<Func<OrderDetail, bool>> predicate)
        {
            return await _orderDetailRepository.FindAsync(predicate);
        }

        public async Task<IEnumerable<OrderDetail>> GetAllAsync()
        {
            return await _orderDetailRepository.GetAllAsync();
        }

        public async Task<OrderDetail> GetByIdAsync(int id)
        {
            var entity = await _orderDetailRepository.GetByIdAsync(id);

            if (entity is null)
            {
                throw new NotFoundException($"Registro con Id={id} No Encontrado");
            }

            return entity;
        }

        public async Task<ResponseData<OrderDetail>> GetByQueryParamsAsync(QueryParams queryParams)
        {
            return await _orderDetailRepository.GetByQueryParamsAsync(queryParams);
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var entity = await _orderDetailRepository.GetByIdAsync(id);

                if (entity is null)
                {
                    throw new NotFoundException($"Registro con Id={id} No Encontrado");
                }

                await _orderDetailRepository.RemoveAsync(entity);
            }
            catch (DbUpdateException)
            {
                throw new BadRequestException($"Registro con Id={id} No se pudo eliminar");
            }
        }

        public async Task<OrderDetail> UpdateAsync(int id, OrderDetail entity)
        {
            if (id != entity.Id)
            {
                throw new BadRequestException($"El Id={id} No Corresponde con el Id={entity.Id} del Registro");
            }

            if (entity.Quantity < 0)
            {
                throw new BadRequestException($"La Cantidad debe ser Mayor a Cero");
            }

            var student = await _orderDetailRepository.GetByIdAsync(id);

            if (entity is null)
            {
                throw new NotFoundException($"Registro con Id={id} No Encontrado");
            }

            await _orderDetailRepository.UpdateAsync(entity);

            return entity;
        }
    }
}