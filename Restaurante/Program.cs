// See https://aka.ms/new-console-template for more information

using Restaurante.Models;

namespace Restaurante;

internal class Program
{
    static void Main(string[] args)
    {
        var restaurante = new Restaurante.Models.Restaurante(1, "Conga", "Av. Sei lá");

        var funcionario = new Funcionario(1, "Martim", "Debaixo da ponte", "91111111", restaurante);
        var cliente = new Cliente(1, "Vitor", "92222222", restaurante);

        restaurante.AddFuncionario(funcionario);
        restaurante.AddCliente(cliente);

        var pedido = new Pedido(1, cliente, funcionario, 0, DateTime.Now);
        funcionario.AddPedido(pedido);

        var ingrediente = new Ingrediente(1, "Queijo", new Stock(1, 10, UnitM.g));

        var item = new Item(1, "Francesinha", 10.00);

        item.AddIngrediente(new ItemIngredientes(1, ingrediente, item, 10));

        var itemPedido = new ItemPedido(1, item, pedido, 1);
        pedido.AddItemPedido(itemPedido);

        Console.WriteLine(pedido.TotalCost());
    }
}
