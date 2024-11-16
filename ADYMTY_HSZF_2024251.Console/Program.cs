using ADYMTY_HSZF_2024251.Application;
using ADYMTY_HSZF_2024251.Model;
using ADYMTY_HSZF_2024251.Persistence.MsSql;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Net.WebSockets;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using System.Transactions;

namespace ADYMTY_HSZF_2024251.console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder().ConfigureServices(
                (hostContext, services) =>
                {
                    services.AddScoped<BattleContext>();


                    services.AddSingleton<IHeroDataProvider, HeroDataProvider>();
                    services.AddSingleton<IHeroService, HeroService>();

                    services.AddSingleton<IMonsterDataProvider, MonsterDataProvider>();
                    services.AddSingleton<IMonsterService, MonsterService>();

                    services.AddSingleton<IBattleDataProvider, BattleDataProvider>();
                    services.AddSingleton<IBattleService, BattleService>();
                }
            ).Build();

            host.Start();

            using IServiceScope serviceScope = host.Services.CreateScope();
            IServiceProvider serviceProvider = serviceScope.ServiceProvider;

            IHeroService heroService = serviceProvider.GetService<IHeroService>();
            IMonsterService monsterService = serviceProvider.GetService<IMonsterService>();
            IBattleService battleService = serviceProvider.GetService<IBattleService>();


            heroService.ToXml();
            battleService.ToXml();
            monsterService.ToXml();
            battleService.FromXml();
            //loop(heroService,monsterService,battleService);
        }
        static void loop(IHeroService hs, IMonsterService ms, IBattleService bs)
        {
            string[] mainMenuOptions = {
                "Új hős felvétele",
                "Új szörny felvétele",
                "Meglévő hős módosítása",
                "Meglévő szörny módosítása",
                "Keresés hős név szerint",
                "Keresés hős kategória szerint" ,
                "Keresés szörny szint szerint",
                "A legerősebb hősök listázása",
                "A leggyorsabb szörnyek listázása",
                "Csata szimulálása",
                "Hősök győzelmi aránya",
                "Legyőzött szörnyek listája"
            };

            int choice = Menu(mainMenuOptions, 0);
            while (choice!=-1)
            {
                Console.Clear();
                switch (choice)
                {
                    case 0:
                        Console.WriteLine("Add new hero");
                        Console.WriteLine();
                        hs.AddHero();
                        break;
                    case 1:
                        Console.WriteLine("Add new monster");
                        Console.WriteLine();
                        ms.AddMonster();
                        break;
                    case 2:
                        Console.WriteLine("Update hero");
                        Console.WriteLine();
                        hs.UpdateHero();
                        break;
                    case 3:
                        Console.WriteLine("Update monster");
                        Console.WriteLine();
                        ms.UpdateMonster();
                        break;
                    case 4:
                        Console.WriteLine("Search hero by name:");
                        Console.Write("Name:");
                        Console.WriteLine();
                        hs.GetHeroByName(Console.ReadLine()).ToConsole();
                        break;
                    case 5:
                        Console.WriteLine("Search hero by category:");
                        Console.Write("Category:");
                        Console.WriteLine();
                        foreach (var item in hs.GetHeroesByCategory(Console.ReadLine()))
                        {
                            item.ToConsole();
                        }
                        break;
                    case 6:
                        Console.WriteLine("Search monster by level: ");
                        Console.WriteLine();
                        Console.Write("Level: ");
                        foreach (var item in ms.GetMonstersByLevel(Console.ReadLine()))
                        {
                            item.ToConsole();
                        }
                        break;
                    case 7:
                        Console.WriteLine("Strongest heroes: ");
                        foreach (var item in hs.GetStrongestHeroes())
                        {
                            Console.WriteLine($"\t{item.Name}");
                        };
                        break;
                    case 8:
                        Console.WriteLine("Fastest monsters:");
                        Console.WriteLine();
                        foreach (var item in ms.GetFastestMonsters())
                        {
                            Console.WriteLine($"\t{item.Name}");
                        };
                        break;
                    case 9:
                        Console.WriteLine("Simulate battle");
                        Console.WriteLine();
                        SimulateBattle(hs, ms, bs);
                        break;

                    case 10:
                        Console.WriteLine("Win rate of heroes");
                        bs.HeroesWinRate();
                        break;
                    case 11:
                        Console.WriteLine("Monsters defeated by heroes");
                        bs.DefeatedMonsters();
                        break;

                    default:
                        break;
                }

                Console.WriteLine();
                Console.WriteLine("Press any button to return to the menu");
                Console.ReadKey();
                choice = Menu(mainMenuOptions, 0);
            }
        }
        static int Menu(string[] options,int pointer)
        {
            Console.Clear();
            ///Returns the value of the chosen menu option
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
            Console.WriteLine("Up/Down arrow to navigate");
            Console.WriteLine("Enter to select");
            Console.WriteLine("Press Esc to exit");

            ConsoleKey choice = Console.ReadKey().Key;
            switch (choice)
            {
                case ConsoleKey.DownArrow:
                    if (++pointer < options.Length)
                    {
                        return Menu(options, pointer);
                    }
                    else
                    {
                        return Menu(options, --pointer);
                    }

                case ConsoleKey.UpArrow:
                    if (--pointer>-1)
                    {
                        return Menu(options, pointer);
                    }
                    else
                    {
                        return Menu(options, ++pointer);
                    }
                    
                    break;

                case ConsoleKey.Enter:
                    return pointer;
                case ConsoleKey.Escape:
                    return -1;

                default:
                    return Menu(options,pointer);
            }


            return -1;
        }
        
        static void SimulateBattle(IHeroService hs, IMonsterService ms, IBattleService bs)
        {
            string[] heroes = hs.GetHeroesName();
            string[] monsters= ms.GetMonstersName();

            Heroes hero = hs.GetHeroById(Menu(heroes,0)+1);
            Monsters monster = ms.GetMonsterById(Menu(monsters,0)+1);

            bs.SimulateBattle(hero,monster);
        }
       
    }
}
