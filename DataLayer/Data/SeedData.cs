using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DataLayer.Data;
using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using DataLayer.Models.Auth;

namespace DataLayer.Models{
    public static class SeedData {
        public static void Initialize(IServiceProvider serviceProvider) {
            using (var context = new MuseumManagementContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MuseumManagementContext>>())) {
                
                
                // Look for any artworks.
                if (context.Artworks.Any()) {
                    context.RemoveRange(context.Artworks);
                    context.RemoveRange(context.Museums);
                    context.RemoveRange(context.LendingToMuseums);
                    context.RemoveRange(context.Restaurations);
                    context.SaveChanges();
                       // DB has been seeded
                }
                var artworks = new Artwork[] {
                    new Artwork
                    {
                        Title = "The Last Supper",
                        Period = "1592",
                        Author = "Leonardo da Vinci",
                        EntryDate= DateTime.Parse("2019-1-11"),
                        EconomicValue = 100,
                        MuseumRoom = "West",
                        Id = 1,
                        LendingToMuseums = new List<LendingToMuseum>()
                    },
                    new Artwork
                    {
                        Title = "The Birth of Venus",
                        Period = "1596",
                        Author = "Leonardo da Vinci",
                        EntryDate= DateTime.Parse("2019-2-11"),
                        EconomicValue = 120,
                        MuseumRoom = "South",
                        Id = 2,
                        LendingToMuseums = new List<LendingToMuseum>()
                    },
                    new Artwork
                    {
                        Title = "Mona Lisa",
                        Period = "1503",
                        Author = "Leonardo da Vinci",
                        EntryDate= DateTime.Parse("2019-3-11"),
                        EconomicValue = 130,
                        MuseumRoom = "East",
                        Id = 3,
                        LendingToMuseums = new List<LendingToMuseum>()
                    }
                };
                context.Artworks.AddRange(
                    artworks[0],
                    artworks[1],
                    artworks[2]
                    );
                
                context.Restaurations.AddRange(
                    new Restauration
                    {
                        Id = 1,
                        ArtworkId = 1,
                        Artwork = artworks[0],
                        RestaurationType = RestaurationType.Complete,
                        StartDate = DateTime.Parse("2019-1-11"),
                        EndDate = DateTime.Parse("2019-2-11")

                    },
                    new Restauration
                    {
                        Id = 2,
                        ArtworkId = 2,
                        Artwork = artworks[1],
                        RestaurationType = RestaurationType.Partial,
                        StartDate = DateTime.Parse("2019-2-11"),
                        EndDate = DateTime.Parse("2019-3-11")
                    },
                    new Restauration
                    {
                        Id = 3,
                        ArtworkId = 3,
                        Artwork = artworks[2],
                        RestaurationType = RestaurationType.Minimal,
                        StartDate = DateTime.Parse("2019-3-11"),
                        EndDate = DateTime.Parse("2019-4-11")
                    }
                );
                var museums = new Museum[] {
                    new Museum{
                        Id = 1,
                        Name = "Museum of Modern Art"
                    },
                    new Museum{
                        Id = 2,
                        Name = "Museum of Fine Arts"

                    },
                    new Museum{
                        Id = 3,
                        Name = "My Museum"
                    }
                };
                context.Museums.AddRange(
                    museums[0],
                    museums[1],
                    museums[2]
                );
                context.SaveChanges();
                var lendingToMuseums = new LendingToMuseum[] {
                    new LendingToMuseum
                    {
                        ArtworkId = 1,
                        PeriodInDays = 40,
                        LendingState = LendingState.Requested,
                        Artwork = artworks[0],
                        Museum = museums[0],
                        MuseumId = 1
                    },
                    new LendingToMuseum
                    {

                        ArtworkId = 2,
                        PeriodInDays = 30,
                        LendingState = LendingState.Requested,
                        Artwork = artworks[1],
                        Museum = museums[1],
                        MuseumId = 2
                    },
                    new LendingToMuseum
                    {
                        ArtworkId = 3,
                        PeriodInDays = 20,
                        LendingState = LendingState.Requested,
                        Artwork = artworks[2],
                        Museum = museums[1],
                        MuseumId = 3
                    }
                };
                context.LendingToMuseums.AddRange(
                    lendingToMuseums[0],
                    lendingToMuseums[1],
                    lendingToMuseums[2]
                );
                var count = 0;
                foreach (var art in context.Artworks){
                    art.LendingToMuseums.Add(lendingToMuseums[count]);
                    count++;
                }
                
                context.SaveChanges();
                var x = context.LendingToMuseums.ToList();
                System.Console.WriteLine( x[0].Artwork.Title);
            }

        }

        public static async void CreateRolesAsync (IServiceProvider serviceProvider) {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            
            string[] roleNames = { UserRoles.Admin, UserRoles.CatalogManager, UserRoles.Director, UserRoles.Restaurator };
            IdentityResult roleResult;
            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }

        public static async void CreateAdminAsync (IServiceProvider serviceProvider) {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            if(userManager.Users.Any()) {
                return;
            }
            var user = CreateUser();
            IUserStore<IdentityUser> userStore = serviceProvider.GetRequiredService<IUserStore<IdentityUser>>();
            var emailStore = (IUserEmailStore<IdentityUser>)userStore;
            await userStore.SetUserNameAsync(user, "admin@gmail.com", CancellationToken.None);
            await emailStore.SetEmailAsync(user, "admin@gmail.com", CancellationToken.None);
            var result = await userManager.CreateAsync(user, "Leinad*2222");
            user.EmailConfirmed=true;
            if(!result.Succeeded) {
                throw new Exception("problem creating admin user");

            }
            await userManager.AddToRoleAsync(user, UserRoles.Admin);
        }

        private static IdentityUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<IdentityUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                    $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }
    }
}