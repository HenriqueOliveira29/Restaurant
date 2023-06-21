using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurante.Models
{
    public class ItemIngredientes
    {
        public int Id { get; private set; }
        public Ingrediente Ingredientes { get; private set; }

        public Item Item { get; private set; }

        public decimal Quantity { get; private set; }

        public ItemIngredientes(int id, Ingrediente ingredientes, Item item, decimal quantity)
        {
            Id = id;
            Ingredientes = ingredientes;
            Item = item;
            Quantity = quantity;
        }
    }
}
