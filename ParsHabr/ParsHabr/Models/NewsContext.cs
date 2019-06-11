using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ParsHabr.Models
{
    public class NewsContext:DbContext
    {
        public DbSet<Source> Sources { get; set; }
        public DbSet<News> News { get; set; }
        public NewsContext(DbContextOptions<NewsContext> options)
           : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //объявление составного ключа
            modelBuilder.Entity<News>().HasKey(u => new { u.Headline, u.PublicationDate });
            //инициализаця таблицы Source
            modelBuilder.Entity<Source>().HasData(
            new Source[]
            {
                new Source { Id=1, Link="http://www.interfax.by/news/feed/", Name="Interfax"},
                new Source { Id=2, Link="http://habrahabr.ru/rss/", Name="HabraHabr"}
            });
        }
    }
}
