using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.Specification;

namespace Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository basketRepository;
        private readonly IUnitOfWork unitOfWork;

        public OrderService(IUnitOfWork unitOfWork,
         IBasketRepository basketRepository)
        {
            this.unitOfWork = unitOfWork;
            this.basketRepository = basketRepository;
        }
        public async Task<Order> CreateOrderAsync(string buyerEmail,
         int deliveryMethodId, string basketId, Address address)
        {
            // get basket from the basket repo
            var basket = await basketRepository.GetBasketAsync(basketId);
            // get items from the product repo
            var items = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var productItem = unitOfWork.Repository<Product>().GetById(item.Id);
                var itemOrdered = new ProductItemOrdered(productItem.Id,
                productItem.Name, productItem.PictureUrl);

                var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);

                items.Add(orderItem);
            }
            // get delivery method from repo
            var deliveryMethod =  unitOfWork.Repository<DeliveryMethod>().GetById(deliveryMethodId);
            // calc subtotal
            var subtotal = items.Sum(item => item.Price * item.Quantity);
            // save to db
            var order = new Order(buyerEmail, address, deliveryMethod, items, subtotal);
            unitOfWork.Repository<Order>().Insert(order);
            
            var result = await unitOfWork.Complete();

            if(result <= 0) return null;

            await basketRepository.DeleteBasketAsync(basketId);

            return order;
        }

        public IReadOnlyList<DeliveryMethod> GetDeliveryMethods()
        {
            return unitOfWork.Repository<DeliveryMethod>().GetAll();
        }

        public Order GetOrderById(int id, string buyerEmail)
        {
            var specification = new OrderWithItemsAndOrderingSpecification(id, buyerEmail);
            
            return unitOfWork.Repository<Order>().GetEntityWithSpecification(specification);
        }

        public IReadOnlyList<Order> GetOrdersForUser(string buyerEmail)
        {
            var specification = new OrderWithItemsAndOrderingSpecification(buyerEmail);
            
            return unitOfWork.Repository<Order>().GetListWithSpecification(specification);
        }
    }
}