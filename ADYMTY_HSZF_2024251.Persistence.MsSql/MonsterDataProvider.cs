using ADYMTY_HSZF_2024251.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADYMTY_HSZF_2024251.Persistence.MsSql
{
    public interface IMonsterDataProvider
    {
        Monsters GetMonsterById(int id);
        List<Monsters> GetMonsters();
    }
    public class MonsterDataProvider : IMonsterDataProvider
    {
        BattleContext ctx;

        public MonsterDataProvider(BattleContext ctx)
        {
            this.ctx = ctx;
        }

        public Monsters GetMonsterById(int id)
        {
            return ctx.Monsters.First(a=>a.MonsterID==id);
        }

        public List<Monsters> GetMonsters()
        {
            return ctx.Monsters.ToList();
        }
    }
}
