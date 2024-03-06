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
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public bool DbUpdateException { get; private set; }

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Customer> AddAsync(Customer entity)
        {
            // Verificar si el userMail ya existe
            var emailExists = await _customerRepository.FindAsync(c => c.UserEmail == entity.UserEmail);
            if (emailExists is null)
            {
                
                throw new InternalServerErrorException($"El UserEmail {entity.UserEmail} Ya Existe");
            }
            return await _customerRepository.AddAsync(entity);

        }


        public async Task<IEnumerable<Customer>> FindAsync(Expression<Func<Customer, bool>> predicate)
        {
            return await _customerRepository.FindAsync(predicate);
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _customerRepository.GetAllAsync();
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            var entity = await _customerRepository.GetByIdAsync(id);

            if (entity is null)
            {
                throw new NotFoundException($"Registro con Id={id} No Encontrado");
            }

            return entity;
        }

        public async Task<ResponseData<Customer>> GetByQueryParamsAsync(QueryParams queryParams)
        {
            return await _customerRepository.GetByQueryParamsAsync(queryParams);
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var entity = await _customerRepository.GetByIdAsync(id);

                if (entity is null)
                {
                    throw new NotFoundException($"Registro con Id={id} No Encontrado");
                }

                await _customerRepository.RemoveAsync(entity);
            }
            catch (DbUpdateException)
            {
                throw new InternalServerErrorException($"No es Posible Eliminar el Registro {id} por Valores Dependientes Asociados");
            }
        }

        public async Task<Customer> UpdateAsync(int id, Customer entity)
        {
            if (id != entity.Id)
            {
                throw new BadRequestException($"El Id={id} No Corresponde con el Id={entity.Id} del Registro");
            }

            var currentEntity = await _customerRepository.GetByIdAsync(id);

            if (currentEntity is null)
            {
                throw new NotFoundException($"Registro con Id={id} No Encontrado");
            }

            await _customerRepository.UpdateAsync(entity);

            return entity;
        }

        public async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int id)
        {
            var entity = await this.GetByIdAsync(id);

            if (entity is null)
            {
                throw new NotFoundException($"Registro con Id={id} No Encontrado");
            }

            return await _customerRepository.GetOrdersByCustomerIdAsync(id);
        }

    }
}