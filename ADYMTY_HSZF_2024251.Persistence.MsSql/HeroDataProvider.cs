using ADYMTY_HSZF_2024251.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADYMTY_HSZF_2024251.Persistence.MsSql
{
    public interface IHeroDataProvider
    {
        Heroes GetHeroById(int id);
        Heroes GetHeroByName(string name);
        Heroes[] GetHeroesByCategory(string category);
        Heroes[] GetHeroes();
        void AddHero(Heroes hero);
        void UpdateHero(Heroes hero);
        //todo
    }
    public class HeroDataProvider : IHeroDataProvider
    {
        BattleContext context;

        public HeroDataProvider(BattleContext context)
        {
            this.context = context;
        }

        public void AddHero(Heroes hero)
        {
            hero.HeroID = default(int);
            int a = hero.HeroID;
            ;
            context.Heroes.Add(hero);
            context.SaveChanges();
        }

        public Heroes GetHeroById(int id)
        {
            return context.Heroes.First(a => a.HeroID == id);
        }

        public Heroes GetHeroByName(string name)
        {
            return context.Heroes.First(a => a.Name == name); ;
        }

        public Heroes[] GetHeroes()
        {
            return context.Heroes.ToArray();
        }

        public Heroes[] GetHeroesByCategory(string category)
        {
            return context.Heroes.Where(t=>t.Category==category).ToArray();
        }

        public void UpdateHero(Heroes hero)
        {
            Heroes toUpdate = GetHeroById(hero.HeroID);
            foreach (var prop in typeof(Heroes).GetProperties())
            {
                prop.SetValue(toUpdate,prop.GetValue(hero));
            }
            context.SaveChanges();
        }
    }
}
