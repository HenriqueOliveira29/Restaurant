using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurante.Models
{
    public class ItemPedido
    {
        public int Id { get; private set; }

        public Item Item { get; private set; }

        public Pedido Pedido { get; private set; }

        public int Quantity { get; private set; }

        public ItemPedido(int id, Item item, Pedido pedido, int quantity)
        {
            Id = id;
            Item = item;
            Pedido = pedido;
            Quantity = quantity;
        }
    }
}
