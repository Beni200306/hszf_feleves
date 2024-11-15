using ADYMTY_HSZF_2024251.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADYMTY_HSZF_2024251.Persistence.MsSql
{
    public interface IBattleDataProvider
    {
        Battle BattleById(int id);
        List<Battle> GetBattles();
        void AddBattle(Battle battle);
    }
    public class BattleDataProvider:IBattleDataProvider
    {
        BattleContext ctx;

        public BattleDataProvider(BattleContext ctx)
        {
            this.ctx = ctx;
        }

        public void AddBattle(Battle battle)
        {
            ctx.Battles.Add(battle);
            ctx.SaveChanges();
        }

        public Battle BattleById(int id)
        {
            return ctx.Battles.First(a=>a.BattleID==id);
        }

        public List<Battle> GetBattles()
        {
            var battles = ctx.Battles;
            return ctx.Battles.ToList();
        }
    }
}
