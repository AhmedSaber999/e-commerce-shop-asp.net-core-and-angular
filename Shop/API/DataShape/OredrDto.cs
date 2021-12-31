namespace API.DataShape
{
    public class OrderDto
    {
        public string BasketId { get; set; }
        public int DelveryMethodId { get; set; }
        public OrderAddressDto ShipToAddress { get; set; }
    }
}