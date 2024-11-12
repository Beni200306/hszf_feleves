using ADYMTY_HSZF_2024251.Application;
using ADYMTY_HSZF_2024251.Persistence.MsSql;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.Design;
using System.Drawing;
using System.Transactions;

namespace ADYMTY_HSZF_2024251.console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //BattleContext bcontext = new BattleContext();

            var host = Host.CreateDefaultBuilder().ConfigureServices(
                (hostContext, services) => 
                {
                    services.AddScoped<BattleContext>();
                    services.AddSingleton<IHeroDataProvider,HeroDataProvider>();
                    services.AddSingleton<IHeroService,HeroService>();

                    services.AddSingleton<IMonsterDataProvider,MonsterDataProvider>();
                    services.AddSingleton<IMonsterService,MonsterService>();

                    //TODO
                }
            ).Build();

            host.Start();



            using IServiceScope serviceScope = host.Services.CreateScope();
            IServiceProvider serviceProvider = serviceScope.ServiceProvider;


            var heroService = serviceProvider.GetService<IHeroService>();
            var hero = heroService.GetHeroById(1);

            var monsterService = serviceProvider.GetService<IMonsterService>();
            var monster = monsterService.GetMonsters();

            var battleService = serviceProvider.GetService<IBattleService>();
            var battle = battleService.GetBattles();
            ;
            
            Menu(new string[] {"listáz","átír","beolvas"}, 0);

        }
        static void Menu(string[] options,int pointer)
        {
            Console.Clear();
            ;
            for (int i = 0; i < options.Length; i++)
            {
                if (pointer==i)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.WriteLine(options[i]);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.WriteLine("-----------------");
            Console.WriteLine("Press Esc to exit");

            ConsoleKey choice = Console.ReadKey().Key;
            ;
            switch (choice)
            {
                case ConsoleKey.DownArrow:
                    if (++pointer < options.Length)
                    {
                        Menu(options, pointer);
                    }
                    else
                    {
                        Menu(options, --pointer);
                    }
                    break;

                case ConsoleKey.UpArrow:
                    if (--pointer>-1)
                    {
                        Menu(options, pointer);
                    }
                    else
                    {
                        Menu(options, ++pointer);
                    }
                    
                    break;

                case ConsoleKey.Enter:
                    ;
                    break;
                case ConsoleKey.Escape:
                    return;

                default:
                    Menu(options,pointer);
                    break;
            }

        }
    }
}
