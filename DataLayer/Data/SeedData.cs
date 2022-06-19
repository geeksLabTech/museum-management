using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DataLayer.Data;
using System;
using System.Linq;

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
                        StartDate = DateTime.Parse("2019-1-11"),
                        EndDate = DateTime.Parse("2019-2-11")

                    },
                    new Restauration
                    {
                        Id = 2,
                        ArtworkId = 2,
                        StartDate = DateTime.Parse("2019-2-11"),
                        EndDate = DateTime.Parse("2019-3-11")
                    },
                    new Restauration
                    {
                        Id = 3,
                        ArtworkId = 3,
                        StartDate = DateTime.Parse("2019-3-11"),
                        EndDate = DateTime.Parse("2019-4-11")
                    }
                );
                var museums = new Museum[] {
                    new Museum{
                        Name = "Museum of Modern Art"
                    },
                    new Museum{
                        Name = "Museum of Fine Arts"
                    },
                    new Museum{
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
                        LendingState = LendingState.Lended,
                        Artwork = artworks[0],
                        Museum = museums[0],
                        MuseumId = 1
                    },
                    new LendingToMuseum
                    {
                        ArtworkId = 2,
                        PeriodInDays = 30,
                        LendingState = LendingState.Returned,
                        Artwork = artworks[1],
                        Museum = museums[1],
                        MuseumId = 2
                    },
                    new LendingToMuseum
                    {
                        ArtworkId = 3,
                        PeriodInDays = 20,
                        LendingState = LendingState.Denied,
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
    }
}