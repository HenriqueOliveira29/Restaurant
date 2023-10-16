namespace Restaurante.Models
{
    public class Restaurant
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Morada { get; private set; }
        public List<Waitress> Funcionarios { get; private set; }
        public List<Client> Clientes { get; private set; }

        public Restaurant(int id, string nome, string morada)
        {
            Id = id;
            Nome = nome;
            Morada = morada;
        }

        public void AddFuncionario(Waitress funcionario)
        {
            funcionario.setRestautant(this);
            if (Funcionarios == null)
            {
                Funcionarios = new List<Waitress>();
            }
            Funcionarios.Add(funcionario);
        }

        public void AddCliente(Client cliente)
        {
            cliente.SetRestaurant(this);
            if (Clientes == null)
            {
                Clientes = new List<Client>();
            }
            Clientes.Add(cliente);
        }
    }
}
