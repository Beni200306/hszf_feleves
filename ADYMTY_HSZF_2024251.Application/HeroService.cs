using ADYMTY_HSZF_2024251.Model;
using ADYMTY_HSZF_2024251.Persistence.MsSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADYMTY_HSZF_2024251.Application
{
    public interface IHeroService
    {
        Heroes GetHeroById(int id);
        Heroes GetHeroByName(string name);
        Heroes[] GetHeroesByCategory(string category);
        Heroes[] GetHeroes();
        void AddHero(Heroes hero);
        void UpdateHero(Heroes hero);
        Heroes[] GetStrongestHeroes();
        string[] GetHeroesName();
        //Heroes[] GetStrongestHeroes();
    }
    public class HeroService : IHeroService
    {
        IHeroDataProvider dataProvider;

        public HeroService(IHeroDataProvider dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        public void AddHero(Heroes hero)
        {
            dataProvider.AddHero(hero);
        }

        public Heroes GetHeroById(int id)
        {
            return dataProvider.GetHeroById(id);
        }

        public Heroes GetHeroByName(string name)
        {
            return this.dataProvider.GetHeroByName(name);
        }

        public Heroes[] GetHeroes()
        {
            return dataProvider.GetHeroes();
        }

        public Heroes[] GetHeroesByCategory(string category)
        {
            return dataProvider.GetHeroesByCategory(category);
        }

        public string[] GetHeroesName()
        {
            return dataProvider.GetHeroes().Select(t => t.Name).ToArray();
        }

        public Heroes[] GetStrongestHeroes()
        {
            var heroes = dataProvider.GetHeroes();
            return heroes.Where(t=>t.Strength==heroes.Max(a=>a.Strength)).ToArray();
        }

        public void UpdateHero(Heroes hero)
        {
            dataProvider.UpdateHero(hero);
        }
        
    }
}
