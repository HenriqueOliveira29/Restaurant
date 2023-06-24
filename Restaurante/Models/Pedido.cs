using Restaurante.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurante.Models
{
    public class Pedido
    {
        public int Id { get; private set; }

        public Cliente Cliente { get; private set; }

        public Funcionario Funcionario { get; private set; }

        public double Preco { get; private set; }

        public List<ItemPedido> ItemPedidos { get; private set; }

        public DateTime Date { get; private set; }


        public Pedido(int id, Cliente cliente, Funcionario funcionario, double preco, DateTime date)
        {
            Id = id;
            Cliente = cliente;
            Funcionario = funcionario;
            Preco = preco;
            Date = date;
        }

        public bool AddItemPedido(ItemPedido itemPedido)
        {
            var ingredientes = itemPedido.Item.ItemIngredientes;
            foreach (var ingrediente in ingredientes)
            {

                if (ingrediente.Ingredientes.Stock.Quatity - (ingrediente.Quantity * itemPedido.Quantity) <= 0)
                {
                    return false;
                }
                else
                {
                    ingrediente.Ingredientes.Stock.Quatity -= (ingrediente.Quantity * itemPedido.Quantity);
                }
            }

            if (ItemPedidos == null)
            {
                ItemPedidos = new List<ItemPedido>();
            }
            ItemPedidos.Add(itemPedido);
            return true;
        }

        public void CalculateTotalCost()
        {
            var itemPedidos = this.ItemPedidos.Where(t => t.Pedido.Id == Id).ToList();

            double cost = 0;
            foreach (var itemPedido in itemPedidos)
            {
                cost += itemPedido.Item.Cost * itemPedido.Quantity;
            }

            Preco = cost;
        }
    }
}
