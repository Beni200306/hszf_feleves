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
    }
    public class HeroService : IHeroService
    {
        IHeroDataProvider dataProvider;

        public HeroService(IHeroDataProvider dataProvider)
        {
            this.dataProvider = dataProvider;
        }
        Heroes IHeroService.GetHeroById(int id)
        {
            return dataProvider.GetHeroById(id);
        }

        List<Heroes> IHeroService.GetHeroes()
        {
            return dataProvider.GetHeroes();
        }
    }
}
