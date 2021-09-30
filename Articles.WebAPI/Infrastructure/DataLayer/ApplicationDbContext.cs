using Articles.WebAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Articles.WebAPI.Infrastructure.DataLayer
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
          : base(options)
        {
            Database.EnsureCreated();

            SeedData();
        }

        public DbSet<ArticleEntity> Articles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        private void SeedData()
        {
            if (!Articles.Any())
            {
                Articles.AddRange(new List<ArticleEntity>
                {
                    new ArticleEntity
                    {
                        AddedDate = new DateTime(2021, 09, 09),
                        Title = "Elon Musk says Tesla owners 'don't seem to listen to me' because they ignore an NDA and share lots of videos of the company's 'full self-driving' tech",
                        Description = "\"I don't know why there's an NDA\" for Tesla's Full Self-Driving beta,\" Elon Musk said. \"We probably don't need it.\" VICE first reported on the NDA."
                    },
                    new ArticleEntity
                    {
                        AddedDate = new DateTime(2021, 09, 10),
                        Title = "Deals on Machines, Beans, and More for National Coffee Day",
                        Description = "This gear will upgrade your at-home café. Plus, the scoop on where to get a free cup of joe today."
                    },
                    new ArticleEntity
                    {
                        AddedDate = new DateTime(2021, 09, 11),
                        Title = "Links 9/29/2021",
                        Description = "Our popular links: bird migration, desalination, food scarcity, fading AZ, kid vaccines, Zoom forever? UK petrol, moonsoon shift, New Cold War, budget staredown, judiciary in crisis? Tesla hits cops, crypto, yacht macho"
                    }
                });

                SaveChanges();
            }
        }
    }
}
