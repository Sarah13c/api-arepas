using Arepas.Domain.Exceptions;
using Arepas.Domain.Interfaces.Repositories;
using Arepas.Domain.Models;
using Arepas.Infrastructure.Common;
using Arepas.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Arepas.Infrastructure.Repositories;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(AppDbContext appDbContext) : base(appDbContext)
    {

    }

    public async Task<IEnumerable<OrderDetailProduct>> GetOrderDetailProductsByOrderIdAsync(int id)
    {
        var orderDetails = await _appDbContext.OrderDetails.Where(x => x.OrderId == id).ToListAsync();
        var orderDetailProducts = new List<OrderDetailProduct>();

        foreach (var orderDetail in orderDetails)
        {
            var product = await _appDbContext.Products.FindAsync(orderDetail.ProductId);

            if (product is null)
            {
                throw new NotFoundException($"No hay Product con ProductId={orderDetail.ProductId}");
            }

            orderDetailProducts.Add(
                new OrderDetailProduct()
                {
                    ProductId = orderDetail.ProductId,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = orderDetail.Quantity,
                    PriceOrd = orderDetail.PriceOrd,
                    Image = product.Image
                });
        }

        return orderDetailProducts;
    }

    public async Task<IEnumerable<OrderDetail>> GetOrdersDetailByOrderIdAsync(int id)
    {
        var result = await _appDbContext.Set<OrderDetail>().ToListAsync<OrderDetail>();
        var ordersDetails = result.Where(x => x.OrderId == id);

        if (!ordersDetails.Any())
        {
            throw new NotFoundException($"No hay Orders details con OrderId={id}");
        }

        return ordersDetails;
    }
}