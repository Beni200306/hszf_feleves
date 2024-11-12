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
    }
}
