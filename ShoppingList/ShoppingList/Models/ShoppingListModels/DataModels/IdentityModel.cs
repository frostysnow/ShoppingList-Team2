using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace ShoppingList.Models.ShoppingListModels
{
    public class IdentityModel
    {
        public class ShoppingListUser : IdentityUser
        {
            public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ShoppingListUser> manager)
            {
                // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
                var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
                // Add custom user claims here
                return userIdentity;
            }
        }

        public class ShoppingListDbContext : IdentityDbContext<ShoppingListUser>
        {
            public ShoppingListDbContext()
                : base("DefaultConnection", throwIfV1Schema: false)
            {
            }

            public static ShoppingListDbContext Create()
            {
                return new ShoppingListDbContext();
            }

            public DbSet<ShoppingListEntity> Lists { get; set; }
            public DbSet<ShoppingListItemEntity> Items { get; set; }
            //public DbSet<ShoppingListNote> ItemNotes { get; set; }

            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
                modelBuilder
                    .Configurations
                        .Add(new IdentityUserLoginConfiguration())
                        .Add(new IdentityUserRoleConfiguration());
            }
            
        }

        public class IdentityUserLoginConfiguration : EntityTypeConfiguration<IdentityUserLogin>
        {
            public IdentityUserLoginConfiguration()
            {
                HasKey(iul => iul.UserId);
            }
        }

        public class IdentityUserRoleConfiguration : EntityTypeConfiguration<IdentityUserRole>
        {
            public IdentityUserRoleConfiguration()
            {
                HasKey(iur => iur.RoleId);
            }
        }
    }
}