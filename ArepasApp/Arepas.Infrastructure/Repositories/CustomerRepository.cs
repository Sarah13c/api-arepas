using Arepas.Domain.Exceptions;
using Arepas.Domain.Interfaces.Repositories;
using Arepas.Domain.Models;
using Arepas.Infrastructure.Common;
using Arepas.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Arepas.Infrastructure.Repositories;

public class CustomerRepository : Repository<Customer>, ICustomerRepository
{
    public CustomerRepository(AppDbContext appDbContext) : base(appDbContext)
    {
           
    }

    public async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int id)
    {
        var result = await _appDbContext.Set<Order>().ToListAsync<Order>();
        var orders = result.Where(x => x.CustomerId == id);


        return orders;
    }
}