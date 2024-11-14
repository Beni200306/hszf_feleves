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
        List<Heroes> GetHeroes();
        void AddHero(Heroes hero);
        void UpdateHero(Heroes hero);
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

        public List<Heroes> GetHeroes()
        {
            return dataProvider.GetHeroes();
        }

        public void UpdateHero(Heroes hero)
        {
            dataProvider.UpdateHero(hero);
        }
    }
}
