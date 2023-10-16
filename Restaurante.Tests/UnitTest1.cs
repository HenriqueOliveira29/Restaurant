using Restaurante.Models;

namespace Restaurante.Tests
{
    public class Tests
    {

        private Restaurante.Models.Restaurant _restautante;
        [SetUp]
        public void Setup()
        {
            _restautante = new Restaurante.Models.Restaurant(1, "Conga", "Av sei lá");
        }

        [Test]
        public void Adicionar_funcionario()
        {
            //prepare
            Waitress funcionario = new Waitress(1, "Albino", "Av não sei", "912345678");
            //act
            _restautante.AddFuncionario(funcionario);

            //assert
            Assert.AreEqual(_restautante.Funcionarios[0], funcionario);
        }

        [Test]
        public void Adicionar_Cliente()
        {
            //prepare
            Client cliente = new Client(1, "Ruben", "918245678");
            //act
            _restautante.AddCliente(cliente);

            //assert
            Assert.AreEqual(_restautante.Clientes[0], cliente);
        }

        [Test]
        public void Adicionar_ItemPedido_Se_Tiver_Stock_Ingredientes()
        {
            //prepare
            var funcionario = new Waitress(1, "Martim", "Debaixo da ponte", "91111111");
            var cliente = new Client(1, "Vitor", "92222222");

            _restautante.AddFuncionario(funcionario);
            _restautante.AddCliente(cliente);

            var pedido = new Request(1, cliente, funcionario, 0, DateTime.Now);
            funcionario.AddPedido(pedido);

            var ingredientes = new List<Ingredients>();
            ingredientes.Add(new Ingredients(1, "Queijo", 20));
            ingredientes.Add(new Ingredients(2, "Bife", 20));

            var item = new Item(1, "Francesinha", 10.00);

            int j = 0;
            foreach (Ingredients ingrediente in ingredientes)
            {
                item.AddIngrediente(new ItemIngredients(j, ingrediente, item, 10));
                j++;
            }

            var itemPedido = new ItemRequest(1, item, pedido, 1);

            var testIngredientes = new List<Ingredients>();
            testIngredientes.Add(new Ingredients(1, "Queijo", 20));
            testIngredientes.Add(new Ingredients(2, "Bife", 40));

            //act
            pedido.AddItemPedido(itemPedido);

            //assert
            int i = 0;
            foreach (var ingredientes1 in item.ItemIngredientes)
            {
                var quantity = testIngredientes[i].Stock.Quatity;
                decimal value = quantity - (ingredientes1.Quantity * itemPedido.Quantity);
                Assert.AreEqual(value, ingredientes[i].Stock.Quatity);
                i++;
            }
        }
    }
}