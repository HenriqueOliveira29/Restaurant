using Ardalis.SmartEnum;
using Microsoft.AspNetCore.Identity;

namespace Restaurant.Infraestructure.Entities
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole()
        {
        }
        public ApplicationRole(string roleName) : base(roleName)
        {
        }

        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }

    public sealed class Roles : SmartEnum<Roles, string>
    {
        public static readonly Roles Admin = new Roles("Admin", "Admin");
        public static readonly Roles Chef = new Roles("Chef", "Chef");
        public static readonly Roles Attendant = new Roles("Attendant", "Attendant");
        public static readonly Roles Client = new Roles("Client", "Client");
        public static readonly Roles Manager = new Roles("Manager", "Manager");

        protected Roles(string name, string value) : base(name, value)
        {
        }
    }
}
