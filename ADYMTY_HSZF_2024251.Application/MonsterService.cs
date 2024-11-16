using ADYMTY_HSZF_2024251.Model;
using ADYMTY_HSZF_2024251.Persistence.MsSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ADYMTY_HSZF_2024251.Application
{
    public interface IMonsterService
    {
        Monsters GetMonsterById(int id);
        Monsters[] GetMonstersByLevel(string level);
        Monsters[] GetMonsters();
        void AddMonster();
        void UpdateMonster();
        Monsters[] GetFastestMonsters();
        string[] GetMonstersName();
        void ToXml();
    }
    public class MonsterService : IMonsterService
    {

        IMonsterDataProvider monsterDataProvider;
        public void ToXml()
        {
            XDocument xdoc = new XDocument();
            XElement root = new XElement("Monsters");
            xdoc.Add(root);
            root.Add(GetMonsters().Select(t => {

                return new XElement("Monster",
                    new XElement("MonsterID", t.MonsterID),
                    new XElement("Name", t.Name),
                    new XElement("Speed", t.Speed),
                    new XElement("Strength", t.Strength),
                    new XElement("Level", t.Level)
                    );
            }
            ));
            xdoc.Save(@"..\..\..\..\monsters.xml");
        }

        public MonsterService(IMonsterDataProvider monsterDataProvider)
        {
            this.monsterDataProvider = monsterDataProvider;
        }

        public void AddMonster()
        {
            monsterDataProvider.AddMonster(CreateInstance.createInstance<Monsters>());
        }

        public Monsters[] GetFastestMonsters()
        {
            Monsters[] monsters = monsterDataProvider.GetMonsters();
            return monsters.Where(t => t.Speed==monsters.Max(a=>a.Speed)).ToArray();
        }

        public Monsters GetMonsterById(int id)
        {
            return monsterDataProvider.GetMonsterById(id);
        }

        public Monsters[] GetMonsters()
        {
            return monsterDataProvider.GetMonsters();
        }

        public Monsters[] GetMonstersByLevel(string level)
        {
            return monsterDataProvider.GetMonstersByLevel(level);
        }

        public string[] GetMonstersName()
        {
            return monsterDataProvider.GetMonsters().Select(a => a.Name).ToArray();
        }

        public void UpdateMonster()
        {
            Console.WriteLine("Give the ID of the monster you want to update: ");
            monsterDataProvider.UpdateMonster(CreateInstance.createInstance<Monsters>());
        }
    }
}
