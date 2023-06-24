using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurante.Models
{
    public class Restaurante
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Morada { get; private set; }
        public List<Funcionario> Funcionarios { get; private set; }
        public List<Cliente> Clientes { get; private set; }

        public Restaurante(int id, string nome, string morada)
        {
            Id = id;
            Nome = nome;
            Morada = morada;
        }

        public void AddFuncionario(Funcionario funcionario)
        {
            funcionario.setRestautant(this);
            if (Funcionarios == null)
            {
                Funcionarios = new List<Funcionario>();
            }
            Funcionarios.Add(funcionario);
        }

        public void AddCliente(Cliente cliente)
        {
            cliente.SetRestaurant(this);
            if (Clientes == null)
            {
                Clientes = new List<Cliente>();
            }
            Clientes.Add(cliente);
        }
    }
}
