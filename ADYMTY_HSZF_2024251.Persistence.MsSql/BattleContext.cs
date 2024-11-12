using ADYMTY_HSZF_2024251.Model;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ADYMTY_HSZF_2024251.Persistence.MsSql
{
    public class BattleContext:DbContext
    {
        public DbSet<Heroes> Heroes { get; set; }
        public DbSet<Monsters> Monsters{ get; set; }
        public DbSet<Battle> Battles{ get; set; }

        public BattleContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
            SeedData();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HeroesVsMonsters;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False").UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }

        void SeedData()
        {
            string input = File.ReadAllText(@"..\..\..\..\heroes.json");
            List<Heroes> heroes = JsonConvert.DeserializeObject<List<Heroes>>(input);


            input = File.ReadAllText(@"..\..\..\..\monsters.json");
            List<Monsters> monsters = JsonConvert.DeserializeObject<List<Monsters>>(input);


            input = File.ReadAllText(@"..\..\..\..\battles.json");
            List<Battle> battles = JsonConvert.DeserializeObject<List<Battle>>(input);
            this.Heroes.AddRange(heroes);
            this.SaveChanges();
            this.Monsters.AddRange(monsters);
            this.SaveChanges();
            this.Battles.AddRange(battles);
            this.SaveChanges();
        }


    }
}
