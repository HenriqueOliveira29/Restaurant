namespace Restaurante.Models
{
    public class ItemRequest
    {
        public int Id { get; private set; }

        public Item Item { get; private set; }

        public Request Pedido { get; private set; }

        public int Quantity { get; private set; }

        public ItemRequest()
        {

        }
        public ItemRequest(int id, Item item, Request pedido, int quantity)
        {
            Id = id;
            Item = item;
            Pedido = pedido;
            Quantity = quantity;
        }
    }
}
