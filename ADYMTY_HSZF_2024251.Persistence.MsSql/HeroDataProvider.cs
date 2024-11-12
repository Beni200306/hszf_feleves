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
        //todo
    }
    public class HeroDataProvider : IHeroDataProvider
    {
        BattleContext context;

        public HeroDataProvider(BattleContext context)
        {
            this.context = context;
        }

        public Heroes GetHeroById(int id)
        {
            return context.Heroes.First(a => a.HeroID == id);
        }

        public List<Heroes> GetHeroes()
        {
            return context.Heroes.ToList();
        }
    }
}
