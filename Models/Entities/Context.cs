using System;
using Microsoft.EntityFrameworkCore;

namespace PersonellerUoW.Models.Entities
{
    public class Context:DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }
        public DbSet<Personel> Personels { get; set; }
        public DbSet<Cografya> Cografyas { get; set; }
        public DbSet<Medya> Medias { get; set; }
    }
}
