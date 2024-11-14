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
        List<Heroes> GetHeroes();
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
            context.Heroes.Add(hero);
            context.SaveChanges();
        }

        public Heroes GetHeroById(int id)
        {
            return context.Heroes.First(a => a.HeroID == id);
        }

        public List<Heroes> GetHeroes()
        {
            return context.Heroes.ToList();
        }

        public void UpdateHero(Heroes hero)
        {
            Heroes toUpdate = context.Heroes.First(t=>t.HeroID==hero.HeroID);
            foreach (var prop in typeof(Heroes).GetProperties())
            {
                prop.SetValue(toUpdate,prop.GetValue(hero));
            }
            context.SaveChanges();
        }
    }
}
