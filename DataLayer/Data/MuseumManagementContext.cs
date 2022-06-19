using Microsoft.EntityFrameworkCore;
using DataLayer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace DataLayer.Data
{
    public class MuseumManagementContext: IdentityDbContext<IdentityUser> {

        public MuseumManagementContext(DbContextOptions<MuseumManagementContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }
        
        public DbSet<Artwork> Artworks {get; set;}
        public DbSet<Museum> Museums {get; set;}
        public DbSet<Picture> Pictures {get; set;}
        public DbSet<Sculpture> Sculptures {get; set;}
        public DbSet<LendingToMuseum> LendingToMuseums {get; set;}
        public DbSet<Restauration> Restaurations {get; set;}
        //public DbSet<User> Users {get; set;}


        protected override void OnModelCreating(ModelBuilder modelBuilder){
            
            modelBuilder.Entity<Artwork>().ToTable("Artwork");
            modelBuilder.Entity<Museum>().ToTable("Museum");
            modelBuilder.Entity<Picture>().ToTable("Picture");
            modelBuilder.Entity<Sculpture>().ToTable("Sculpture");
            modelBuilder.Entity<LendingToMuseum>().ToTable("LendingToMuseum");
            modelBuilder.Entity<Restauration>().ToTable("Restauration");
            //modelBuilder.Entity<User>().ToTable("User");

            modelBuilder.Entity<Restauration>()
                .HasKey(r => new {r.Id, r.ArtworkId});
            
            modelBuilder.Entity<LendingToMuseum>()
                .HasKey(l => new {l.ArtworkId, l.MuseumId});
            base.OnModelCreating(modelBuilder);
        }
    }
}