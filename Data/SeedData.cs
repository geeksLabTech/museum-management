using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using museum_management.Data;
using System;
using System.Linq;

namespace museum_management.Models{
    public static class SeedData {
        public static void Initialize(IServiceProvider serviceProvider) {
            using (var context = new MuseumManagementContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MuseumManagementContext>>())) {
                // Look for any artworks.
                if (context.Artworks.Any()) {
                    return;   // DB has been seeded
                }
                var artworks = new Artwork[] {
                    new Artwork
                    {
                        Title = "The Last Supper",
                        Period = "1592",
                        Author = "Leonardo da Vinci",
                        EntryDate= DateTime.Parse("2019-1-11"),
                        EconomicValue = 100,
                        Id = 1
                    },
                    new Artwork
                    {
                        Title = "The Birth of Venus",
                        Period = "1596",
                        Author = "Leonardo da Vinci",
                        EntryDate= DateTime.Parse("2019-2-11"),
                        EconomicValue = 120,
                        Id = 2
                    },
                    new Artwork
                    {
                        Title = "Mona Lisa",
                        Period = "1503",
                        Author = "Leonardo da Vinci",
                        EntryDate= DateTime.Parse("2019-3-11"),
                        EconomicValue = 130,
                        Id = 3
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
                context.LendingToMuseums.AddRange(
                    new LendingToMuseum
                    {
                        ArtworkId = 1,
                        PeriodInDays = 40,
                        IsFinished = false,
                        Artwork = artworks[0],
                        Museum = museums[0],
                        MuseumId = 1
                    },
                    new LendingToMuseum
                    {
                        ArtworkId = 2,
                        PeriodInDays = 30,
                        IsFinished = true,
                        Artwork = artworks[1],
                        Museum = museums[1],
                        MuseumId = 2
                    },
                    new LendingToMuseum
                    {
                        ArtworkId = 3,
                        PeriodInDays = 20,
                        IsFinished = false,
                        Artwork = artworks[2],
                        Museum = museums[1],
                        MuseumId = 3
                    }
                );
                context.SaveChanges();
            }
        }
    }
}