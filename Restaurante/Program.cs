// See https://aka.ms/new-console-template for more information

using Restaurante.Models;

namespace Restaurante;

internal class Program
{
    static void Main(string[] args)
    {
        var restaurante = new Restaurante.Models.Restaurant(1, "Conga", "Av. Sei lá");

        var funcionario = new Waitress(1, "Martim", "Debaixo da ponte", "91111111");
        var cliente = new Client(1, "Vitor", "92222222");

        restaurante.AddFuncionario(funcionario);
        restaurante.AddCliente(cliente);

        var pedido = new Request(1, cliente, funcionario, 0, DateTime.Now);
        funcionario.AddPedido(pedido);

        Ingredients ingrediente = new Ingredients(1, "Queijo", 20);

        var item = new Item(1, "Francesinha", 10.00);

        item.AddIngrediente(new ItemIngredients(1, ingrediente, item, 10));

        var itemPedido = new ItemRequest(1, item, pedido, 1);
        pedido.AddItemPedido(itemPedido);

        pedido.CalculateTotalCost();

        Console.WriteLine(pedido.Preco);
    }
}
