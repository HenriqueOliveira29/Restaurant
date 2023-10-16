namespace Restaurante.Models
{
    public class Waitress
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        public string Morada { get; private set; }

        public string Contacto { get; private set; }

        public Restaurant Restaurante { get; private set; }

        public List<Request> Pedidos { get; private set; }

        public Waitress(int id, string name, string morada, string contacto)
        {
            Id = id;
            Name = name;
            Morada = morada;
            Contacto = contacto;
        }

        public void AddPedido(Request pedido)
        {
            if (Pedidos == null)
            {
                Pedidos = new List<Request>();
            }
            Pedidos.Add(pedido);
        }

        public void setRestautant(Restaurant restaurant)
        {
            Restaurante = restaurant;
        }
    }
}
