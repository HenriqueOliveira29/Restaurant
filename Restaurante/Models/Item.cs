using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurante.Models
{
    public class Item
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public double Cost { get; private set; }
        public List<ItemIngredientes> ItemIngredientes { get; private set; }

        public Item(int id, string name, double cost)
        {
            Id = id;
            Name = name;
            Cost = cost;
        }

        public List<Ingrediente> GetIngredientes()
        {
            return ItemIngredientes.Where(t => t.Item.Id == this.Id).Select(t => t.Ingredientes).ToList();
        }

        public bool HaveIngredients()
        {
            return ItemIngredientes.Any(t => t.Quantity > t.Ingredientes.Stock.Quatity);
        }

        public void AddIngrediente(ItemIngredientes itemIngredientes)
        {
            if (ItemIngredientes == null)
            {
                ItemIngredientes = new List<ItemIngredientes>();
            }
            ItemIngredientes.Add(itemIngredientes);
        }
    }
}
