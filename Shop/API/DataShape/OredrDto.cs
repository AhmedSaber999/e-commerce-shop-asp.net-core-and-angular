namespace API.DataShape
{
    public class OrderDto
    {
        public string BasketId { get; set; }
        public int DeliveryMethodId { get; set; }
        public OrderAddressDto ShipToAddress { get; set; }
    }
}