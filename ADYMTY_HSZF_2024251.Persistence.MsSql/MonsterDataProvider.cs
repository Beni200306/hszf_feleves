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
        Monsters[] GetMonstersByLevel(string level);
        Monsters[] GetMonsters();
        void AddMonster(Monsters monster);
        void UpdateMonster(Monsters monster);
    }
    public class MonsterDataProvider : IMonsterDataProvider
    {
        BattleContext ctx;

        public MonsterDataProvider(BattleContext ctx)
        {
            this.ctx = ctx;
        }

        public void AddMonster(Monsters monster)
        {
            monster.MonsterID = default(int);
            ctx.Monsters.Add(monster);
            ctx.SaveChanges();
        }

        public Monsters GetMonsterById(int id)
        {
            return ctx.Monsters.First(a=>a.MonsterID==id);
        }

        public Monsters[] GetMonsters()
        {
            return ctx.Monsters.ToArray();
        }

        public Monsters[] GetMonstersByLevel(string level)
        {
            return ctx.Monsters.Where(t=>t.Level==level).ToArray();
        }

        public void UpdateMonster(Monsters monster)
        {
            Monsters toUpdate = GetMonsterById(monster.MonsterID);
            foreach (var prop in typeof(Monsters).GetProperties())
            {
                prop.SetValue(toUpdate, prop.GetValue(monster));
            }
            ctx.SaveChanges();
        }
    }
}
