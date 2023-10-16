namespace Restaurante.Models
{
    public enum UnitM
    {
        unit, g, kg, lt
    }

    public class Stock
    {
        public int Id { get; set; }
        public decimal Quatity { get; set; }
        public UnitM Unit { get; set; }

        public int IngredientId { get; set; }
        public Ingredients Ingredient { get; set; }

        public Stock(int id, int quantity, UnitM unit, Ingredients ingredient)
        {
            Id = id;
            Quatity = quantity;
            Unit = unit;
            Ingredient = ingredient;
        }

        public Stock()
        {

        }

    }
}
