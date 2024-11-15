using ADYMTY_HSZF_2024251.Model;
using ADYMTY_HSZF_2024251.Persistence.MsSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADYMTY_HSZF_2024251.Application
{
    public interface IBattleService
    {
        Battle BattleById(int id);
        List<Battle> GetBattles();
        void SimulateBattle(Heroes hero,Monsters monsters);
        void HeroesWinRate();
        Monsters[] DefeatedMonsters();
    }
    public class BattleService : IBattleService
    {
        IBattleDataProvider battleDataProvider;

        public BattleService(IBattleDataProvider battleDataProvider)
        {
            this.battleDataProvider = battleDataProvider;
        }

        public Battle BattleById(int id)
        {
            return battleDataProvider.BattleById(id);
        }

        public List<Battle> GetBattles()
        {
            return battleDataProvider.GetBattles();
        }

        public void SimulateBattle(Heroes hero, Monsters monster)
        {
            Battle newBattle = new Battle() {Hero=hero,Monster=monster,BattleDate=DateTime.Now, HeroID=hero.HeroID,MonsterID=monster.MonsterID, Result=(hero.Strength+hero.Speed)>(monster.Speed+monster.Strength)?"Victory":"Defeat" };
            battleDataProvider.AddBattle(newBattle);
        }

        public void HeroesWinRate()
        {
            var battles = battleDataProvider.GetBattles();
            var groupByHero = battleDataProvider.GetBattles().GroupBy(t => t.Hero);
            var statistic = groupByHero.Select(a=>new
                {
                    hero = a.Key,
                    winRate = (((double)a.Count(b=>b.Result=="Victory"))/((double)a.Count()))*100
                }
            );
            Console.WriteLine("Heroes win rate");
            foreach (var item in statistic)
            {
                Console.WriteLine($"{item.hero.Name} winrate: {item.winRate}");
            }

        }

        public Monsters[] DefeatedMonsters()
        {
            Monsters[] defeatedMonsters = battleDataProvider.GetBattles().Where(t=>t.Result=="Victory").Select(x=>x.Monster).Distinct().ToArray();
            Console.WriteLine("Defeated monsters: ");
            foreach (var item in defeatedMonsters)
            {
                Console.WriteLine($"\t{item.Name}");
            }
            return defeatedMonsters;
        }
    }
}
