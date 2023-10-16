namespace Restaurante.Models
{
    public class Client
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Contacto { get; set; }

        public Restaurant Restaurante { get; set; }
        public List<Request> Pedidos { get; set; }

        public Client()
        {

        }

        public Client(int id, string nome, string contacto)
        {
            Id = id;
            Nome = nome;
            Contacto = contacto;
        }

        public void AddPedido(Request pedido)
        {
            Pedidos.Add(pedido);
        }

        public void SetRestaurant(Restaurant restaurant)
        {
            Restaurante = restaurant;
        }
    }
}
