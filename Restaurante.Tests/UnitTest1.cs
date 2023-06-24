using Restaurante.Models;

namespace Restaurante.Tests
{
    public class Tests
    {

        private Restaurante.Models.Restaurante _restautante;
        [SetUp]
        public void Setup()
        {
            _restautante = new Restaurante.Models.Restaurante(1, "Conga", "Av sei lá");
        }

        [Test]
        public void Adicionar_funcionario()
        {
            //prepare
            Funcionario funcionario = new Funcionario(1, "Albino", "Av não sei", "912345678");
            //act
            _restautante.AddFuncionario(funcionario);

            //assert
            Assert.AreEqual(_restautante.Funcionarios[0], funcionario);
        }

        [Test]
        public void Adicionar_Cliente()
        {
            //prepare
            Cliente cliente = new Cliente(1, "Ruben", "918245678");
            //act
            _restautante.AddCliente(cliente);

            //assert
            Assert.AreEqual(_restautante.Clientes[0], cliente);
        }

        [Test]
        public void Adicionar_ItemPedido_Se_Tiver_Stock_Ingredientes()
        {
            //prepare
            var funcionario = new Funcionario(1, "Martim", "Debaixo da ponte", "91111111");
            var cliente = new Cliente(1, "Vitor", "92222222");

            _restautante.AddFuncionario(funcionario);
            _restautante.AddCliente(cliente);

            var pedido = new Pedido(1, cliente, funcionario, 0, DateTime.Now);
            funcionario.AddPedido(pedido);

            var ingredientes = new List<Ingrediente>();
            ingredientes.Add(new Ingrediente(1, "Queijo", new Stock(1, 20, UnitM.g)));
            ingredientes.Add(new Ingrediente(2, "Bife", new Stock(2, 40, UnitM.g)));

            var item = new Item(1, "Francesinha", 10.00);

            int j = 0;
            foreach (Ingrediente ingrediente in ingredientes)
            {
                item.AddIngrediente(new ItemIngredientes(j, ingrediente, item, 10));
                j++;
            }

            var itemPedido = new ItemPedido(1, item, pedido, 1);

            var testIngredientes = new List<Ingrediente>();
            testIngredientes.Add(new Ingrediente(1, "Queijo", new Stock(1, 20, UnitM.g)));
            testIngredientes.Add(new Ingrediente(2, "Bife", new Stock(2, 40, UnitM.g)));

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