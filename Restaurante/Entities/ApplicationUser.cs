using Microsoft.AspNetCore.Identity;

namespace Restaurant.Infraestructure.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public bool Active { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }

        //Lockout duration in minutes
        public int LastLockoutDuration { get; set; } = 0;

        public string PreferedView { get; set; } = "";

        public ICollection<ApplicationUserRole> UserRoles { get; set; }

        public ApplicationRole? GetCurrentRole()
        {
            if (UserRoles == null || UserRoles.Count <= 0) return null;

            return UserRoles.First().Role;
        }

        public bool IsAdmin()
        {
            if (UserRoles == null || UserRoles.Count <= 0) return false;
            if (UserRoles.Any(a => a.Role.Name == Restaurant.Infraestructure.Entities.Roles.Admin.Value))
            {
                return true;
            }
            return false;
        }
    }
}
