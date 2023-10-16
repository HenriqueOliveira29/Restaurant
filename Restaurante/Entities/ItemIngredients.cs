namespace Restaurante.Models
{
    public class ItemIngredients
    {
        public int Id { get; private set; }
        public Ingredients Ingredientes { get; private set; }

        public Item Item { get; private set; }

        public decimal Quantity { get; private set; }

        public ItemIngredients()
        {

        }
        public ItemIngredients(int id, Ingredients ingredientes, Item item, decimal quantity)
        {
            Id = id;
            Ingredientes = ingredientes;
            Item = item;
            Quantity = quantity;
        }
    }
}
