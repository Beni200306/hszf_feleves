using ADYMTY_HSZF_2024251.Model;
using ADYMTY_HSZF_2024251.Persistence.MsSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ADYMTY_HSZF_2024251.Application
{
    public interface IBattleService
    {
        Battle BattleById(int id);
        List<Battle> GetBattles();
        void SimulateBattle(Heroes hero,Monsters monsters);
        void HeroesWinRate();
        void DefeatedMonsters();
        void ToXml();
        void FromXml();
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
            var groupByHero = battleDataProvider.GetBattles().GroupBy(t => t.Hero);
            var statistic = groupByHero.Select(a=>new
                {
                    hero = a.Key,
                    winRate = (((double)a.Count(b=>b.Result=="Victory"))/((double)a.Count()))*100
                }
            );
            Console.Clear();
            Console.WriteLine("Heroes win rate");
            foreach (var item in statistic)
            {
                Console.WriteLine($"{item.hero.Name} winrate: {item.winRate}%");
            }

        }

        public void DefeatedMonsters()
        {
            Console.Clear();
            var defeatedMonstersGroupByHeroes = battleDataProvider.GetBattles().Where(t=>t.Result=="Victory").GroupBy(x=>x.Hero);
            
            Console.WriteLine("-------------------------");
            foreach (var item in defeatedMonstersGroupByHeroes)
            {
                Console.WriteLine($"{item.Key.Name} defeated: ");
                foreach (var battle in item)
                {
                    Console.WriteLine($"\t{battle.Monster.Name}");
                }
                Console.WriteLine("-------------------------");
            }
        }

        public void ToXml()
        {
            XDocument xdoc = new XDocument();

            XElement root = new XElement("Battles");

            xdoc.Add(root);

            Battle[] battles = GetBattles().ToArray();

            root.Add(battles.Select(t =>
            {
                return new XElement("battles",

                    new XElement("BattleID", t.BattleID),
                    new XElement("HeroID", t.HeroID),
                    new XElement("MonsterID", t.MonsterID),
                    new XElement("BattleDate", t.BattleDate),
                    new XElement("Result", t.Result));

            }));

            xdoc.Save(@"..\..\..\..\battles.xml");
        }

        public void FromXml()
        {
            Battle[] battles=XDocument.Load(@"..\..\..\..\battles.xml").Root.Elements("battles").Select(
                t => {
                    return new Battle() { 
                        BattleDate=DateTime.Parse(t.Element("BattleDate").Value),
                        BattleID =int.Parse(t.Element("BattleID").Value),
                        HeroID =int.Parse(t.Element("HeroID").Value), 
                        MonsterID =int.Parse(t.Element("MonsterID").Value), 
                        Result=t.Element("Result").Value};
                }).ToArray();
            
        }
    }
}
