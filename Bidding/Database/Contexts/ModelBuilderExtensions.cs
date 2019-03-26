using BiddingAPI.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Type = BiddingAPI.Models.DatabaseModels.Type;

namespace Bidding.Database.Contexts
{
    public static class ModelBuilderExtensions
    {
        // https://www.learnentityframeworkcore.com/migrations/seeding
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // todo: kke: has data for role table!

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    UserFirstName = "Bidding",
                    UserLastName = "Admin",
                    UserEmail = "info@bidding.lv",
                    UserRoleId = 1,
                    UserUniqueIdentifier = "",
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = 1 // todo: kke: this will probably fail, because of FKey!
                }
            );

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Transports", CreatedAt = DateTime.UtcNow, CreatedBy = 1 },
                new Category { CategoryId = 2, CategoryName = "Manta", CreatedAt = DateTime.UtcNow, CreatedBy = 1 },
                new Category { CategoryId = 3, CategoryName = "Nekustamais īpašums", CreatedAt = DateTime.UtcNow, CreatedBy = 1 }
            );

            modelBuilder.Entity<Type>().HasData(
                new Type { TypeId = 1, TypeName = "Vieglais transports līdz 3,5t", CreatedAt = DateTime.UtcNow, CreatedBy = 1 },
                new Type { TypeId = 2, TypeName = "Traktortehnika", CreatedAt = DateTime.UtcNow, CreatedBy = 1 },
                new Type { TypeId = 3, TypeName = "Kravas auto", CreatedAt = DateTime.UtcNow, CreatedBy = 1 },
                new Type { TypeId = 4, TypeName = "Mototehnika", CreatedAt = DateTime.UtcNow, CreatedBy = 1 },
                new Type { TypeId = 5, TypeName = "Ūdens transports", CreatedAt = DateTime.UtcNow, CreatedBy = 1 },
                new Type { TypeId = 6, TypeName = "Cits transports", CreatedAt = DateTime.UtcNow, CreatedBy = 1 },
                new Type { TypeId = 7, TypeName = "Biroja tehnika", CreatedAt = DateTime.UtcNow, CreatedBy = 1 },
                new Type { TypeId = 8, TypeName = "Elektrotehnika", CreatedAt = DateTime.UtcNow, CreatedBy = 1 },
                new Type { TypeId = 9, TypeName = "Rūpniecības tehnika", CreatedAt = DateTime.UtcNow, CreatedBy = 1 },
                new Type { TypeId = 10, TypeName = "Instrumenti", CreatedAt = DateTime.UtcNow, CreatedBy = 1 },
                new Type { TypeId = 11, TypeName = "Cita manta", CreatedAt = DateTime.UtcNow, CreatedBy = 1 },
                new Type { TypeId = 12, TypeName = "Dzīvoklis", CreatedAt = DateTime.UtcNow, CreatedBy = 1 },
                new Type { TypeId = 13, TypeName = "Māja", CreatedAt = DateTime.UtcNow, CreatedBy = 1 },
                new Type { TypeId = 14, TypeName = "Zeme", CreatedAt = DateTime.UtcNow, CreatedBy = 1 }
            );
        }
    }
}
