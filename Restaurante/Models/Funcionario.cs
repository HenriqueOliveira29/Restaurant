using Restaurante.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurante.Models
{
    public class Funcionario
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        public string Morada { get; private set; }

        public string Contacto { get; private set; }

        public Restaurante Restaurante { get; private set; }

        public List<Pedido> Pedidos { get; private set; }

        public Funcionario(int id, string name, string morada, string contacto, Restaurante restaurante)
        {
            Id = id;
            Name = name;
            Morada = morada;
            Contacto = contacto;
            Restaurante = restaurante;
        }

        public void AddPedido(Pedido pedido)
        {
            if (Pedidos == null)
            {
                Pedidos = new List<Pedido>();
            }
            Pedidos.Add(pedido);
        }
    }
}
