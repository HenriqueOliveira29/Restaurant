namespace Restaurante.Models
{
    public class Ingredients
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        public Stock Stock { get; private set; }

        public List<ItemIngredients> ItemIngredientes { get; private set; }

        public Ingredients()
        {

        }

        public Ingredients(int id, string name, int stock)
        {
            Id = id;
            Name = name;
            Stock = new Stock(1, stock, UnitM.g, this);
        }

        public void AddItemIngrediente(ItemIngredients itemIngredientes)
        {
            if (ItemIngredientes == null)
            {
                ItemIngredientes = new List<ItemIngredients>();
            }
            ItemIngredientes.Add(itemIngredientes);
        }

    }
}
