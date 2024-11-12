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
    }
    public class BattleDataProvider:IBattleDataProvider
    {
        BattleContext ctx;

        public BattleDataProvider(BattleContext ctx)
        {
            this.ctx = ctx;
        }

        public Battle BattleById(int id)
        {
            return ctx.Battles.First(a=>a.BattleID==id);
        }

        public List<Battle> GetBattles()
        {
            return ctx.Battles.ToList();
        }
    }
}
