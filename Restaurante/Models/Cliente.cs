using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurante.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Contacto { get; set; }

        public Restaurante Restaurante { get; set; }
        public List<Pedido> Pedidos { get; set; }

        public Cliente(int id, string nome, string contacto, Restaurante restaurante)
        {
            Id = id;
            Nome = nome;
            Contacto = contacto;
            Restaurante = restaurante;
        }

        public void AddPedido(Pedido pedido)
        {
            Pedidos.Add(pedido);
        }
    }
}
