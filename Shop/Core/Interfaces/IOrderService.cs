using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities.OrderAggregate;

namespace Core.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string buyerEmail, 
        int deliveryMethod, string basketId, Address address);

        IReadOnlyList<Order> GetOrdersForUser(string buyerEmail);

        Order GetOrderById(int id, string buyerEmail);
        IReadOnlyList<DeliveryMethod> GetDeliveryMethods();

        
    }
}