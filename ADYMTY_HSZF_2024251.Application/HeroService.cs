using ADYMTY_HSZF_2024251.Model;
using ADYMTY_HSZF_2024251.Persistence.MsSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ADYMTY_HSZF_2024251.Application
{
    public interface IHeroService
    {
        Heroes GetHeroById(int id);
        Heroes GetHeroByName(string name);
        Heroes[] GetHeroesByCategory(string category);
        Heroes[] GetHeroes();
        void AddHero();
        void UpdateHero();
        Heroes[] GetStrongestHeroes();
        string[] GetHeroesName();
        void ToXml();
    }
    public class HeroService : IHeroService
    {
        IHeroDataProvider dataProvider;

        public HeroService(IHeroDataProvider dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        public void AddHero()
        {
            dataProvider.AddHero(CreateInstance.createInstance<Heroes>());
        }

        public void ToXml()
        {
            XDocument xdoc = new XDocument();
            XElement root = new XElement("Heroes");
            xdoc.Add(root);
            root.Add(GetHeroes().Select(t=>{

                return new XElement("Hero",
                    new XElement("HeroID", t.HeroID),
                    new XElement("Name", t.Name),
                    new XElement("Speed", t.Speed),
                    new XElement("Strength", t.Strength),
                    new XElement("Abilities", t.Abilities),
                    new XElement("Category", t.Category)                    
                    );
                }
            ) );
            xdoc.Save(@"..\..\..\..\heroes.xml");
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

        public void UpdateHero()
        {
            dataProvider.UpdateHero(CreateInstance.createInstance<Heroes>());
        }
        
    }
}
