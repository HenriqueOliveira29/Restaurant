using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Restaurante.Models
{
    public class Ingrediente
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        public Stock Stock { get; private set; }

        public List<ItemIngredientes> ItemIngredientes { get; private set; }

        public Ingrediente(int id, string name, Stock stock)
        {
            Id = id;
            Name = name;
            Stock = stock;
        }

        public void AddItemIngrediente(ItemIngredientes itemIngredientes)
        {
            if (ItemIngredientes == null)
            {
                ItemIngredientes = new List<ItemIngredientes>();
            }
            ItemIngredientes.Add(itemIngredientes);
        }

    }
}
