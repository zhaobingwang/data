using DataPump.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataPump.Data
{
    public class PumpDbContext : DbContext
    {
        public DbSet<ChineseName> ChineseNames { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // 数据库迁移时用
            //string resourcePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../../../Data/resource");
            //optionsBuilder.UseSqlite("Data Source=../../Data/SQLite/Trifles.Dict.db");

            // 数据库操CRUD等操作时用
            string resourcePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../../../Data/SQLite/Trifles.Dict.db");
            optionsBuilder.UseSqlite("Data Source=" + resourcePath);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChineseName>()
                .ToTable("ChineseName");
        }
    }
}
