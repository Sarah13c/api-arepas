using Arepas.Domain.Common;
using Arepas.Domain.Models;

namespace Arepas.Domain.Interfaces.Repositories;

public interface ICustomerRepository : IRepository<Customer>
{
    Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int id);
}