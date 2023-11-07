using Microsoft.EntityFrameworkCore;
using SocialNetwork.API.Models.Domain;
using System.Collections.Generic;

namespace SocialNetwork.API.Data
{
    public class SocialNetworkDbContext : DbContext 
    {
        public SocialNetworkDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<Article> Article { get; set; }
        public DbSet<Events> Events { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Registration> Registration { get; set; }
        public DbSet<Staff> Staff {  get; set; }    

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
