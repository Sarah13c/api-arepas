using Arepas.Domain.Common;
using Arepas.Domain.Models;

namespace Arepas.Domain.Interfaces.Repositories;

public interface IOrderRepository : IRepository<Order>
{
    Task<IEnumerable<OrderDetailProduct>> GetOrderDetailProductsByOrderIdAsync(int id);
    Task<IEnumerable<OrderDetail>> GetOrdersDetailByOrderIdAsync(int id);
}