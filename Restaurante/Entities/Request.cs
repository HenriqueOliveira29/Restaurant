namespace Restaurante.Models
{
    public class Request
    {
        public int Id { get; private set; }

        public Client Cliente { get; private set; }

        public Waitress Funcionario { get; private set; }

        public double Preco { get; private set; }

        public List<ItemRequest> ItemPedidos { get; private set; }

        public DateTime Date { get; private set; }

        public Request()
        {

        }

        public Request(int id, Client cliente, Waitress funcionario, double preco, DateTime date)
        {
            Id = id;
            Cliente = cliente;
            Funcionario = funcionario;
            Preco = preco;
            Date = date;
        }

        public bool AddItemPedido(ItemRequest itemPedido)
        {
            var ingredientes = itemPedido.Item.ItemIngredientes;
            foreach (var ingrediente in ingredientes)
            {

                if (ingrediente.Ingredientes.Stock.Quatity - (ingrediente.Quantity * itemPedido.Quantity) <= 0)
                {
                    return false;
                }
                else
                {
                    ingrediente.Ingredientes.Stock.Quatity -= (ingrediente.Quantity * itemPedido.Quantity);
                }
            }

            if (ItemPedidos == null)
            {
                ItemPedidos = new List<ItemRequest>();
            }
            ItemPedidos.Add(itemPedido);
            return true;
        }

        public void CalculateTotalCost()
        {
            var itemPedidos = this.ItemPedidos.Where(t => t.Pedido.Id == Id).ToList();

            double cost = 0;
            foreach (var itemPedido in itemPedidos)
            {
                cost += itemPedido.Item.Cost * itemPedido.Quantity;
            }

            Preco = cost;
        }
    }
}
