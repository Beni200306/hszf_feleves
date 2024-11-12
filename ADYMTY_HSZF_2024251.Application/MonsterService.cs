using ADYMTY_HSZF_2024251.Model;
using ADYMTY_HSZF_2024251.Persistence.MsSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADYMTY_HSZF_2024251.Application
{
    public interface IMonsterService
    {
        Monsters GetMonsterById(int id);
        List<Monsters> GetMonsters();
    }
    public class MonsterService:IMonsterService
    {

        IMonsterDataProvider monsterDataProvider;

        public MonsterService(IMonsterDataProvider monsterDataProvider)
        {
            this.monsterDataProvider = monsterDataProvider;
        }

        public Monsters GetMonsterById(int id)
        {
            return monsterDataProvider.GetMonsterById(id);
        }

        public List<Monsters> GetMonsters()
        {
            return monsterDataProvider.GetMonsters();
        }
        
    }
}
