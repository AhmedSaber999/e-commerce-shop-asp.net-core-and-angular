using Core.Entities.OrderAggregate;

namespace Core.Specification
{
    public class OrderWithItemsAndOrderingSpecification : BaseSpecification<Order>
    {
        
        public OrderWithItemsAndOrderingSpecification(string email)
        : base(o => o.BuyerEmail == email)
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.Orderdate);
        }

        public OrderWithItemsAndOrderingSpecification(int id, string email)
        : base(o => o.BuyerEmail == email && o.Id == id)
        {
            
        }

    }
}