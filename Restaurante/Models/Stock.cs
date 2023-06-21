using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Stock(int id, int quantity, UnitM unit)
        {
            Id = id;
            Quatity = quantity;
            Unit = unit;
        }

    }
}
