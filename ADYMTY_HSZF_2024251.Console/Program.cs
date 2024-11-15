using ADYMTY_HSZF_2024251.Application;
using ADYMTY_HSZF_2024251.Model;
using ADYMTY_HSZF_2024251.Persistence.MsSql;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using System.ComponentModel.Design;
using System.Drawing;
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

            battleService.DefeatedMonsters();
            ;
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
                "Csata szimulálása"
            };
            
            int a=Menu(mainMenuOptions, 0);
            ;
            //SimulateBattle(heroService,monsterService,battleService);
        }
        static int Menu(string[] options,int pointer)
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
                    break;

                default:
                    return Menu(options,pointer);
            }


            return -1;
        }
        static T CreateInstance<T>()
        {
            T re = (T)Activator.CreateInstance(typeof(T));
            foreach (var prop in re.GetType().GetProperties())
            {
                if (prop.GetCustomAttribute<ConvertAttribute>()!=null)
                {
                    Console.WriteLine($"Enter value for {prop.Name} ({prop.PropertyType.Name}):");

                    string input = Console.ReadLine();
                    if (prop.PropertyType != typeof(string))
                    {
                        MethodInfo parse = prop.PropertyType.GetMethods().First(t=>t.Name=="Parse");
                        object value = parse.Invoke(null,new object[] {input });
                        //object value = ConvertValue(input, prop.PropertyType);
                        prop.SetValue(re, value);
                    }
                    else
                    {
                        prop.SetValue(re, input);
                    }
                }
                
            }
            return re;
        }
        static void SimulateBattle(IHeroService hs, IMonsterService ms, IBattleService bs)
        {
            string[] heroes = hs.GetHeroes().Select(t => t.Name).ToArray();
            string[] monsters= ms.GetMonsters().Select(a => a.Name).ToArray();

            Heroes hero= hs.GetHeroById(Menu(heroes,0)+1);
            Monsters monster= ms.GetMonsterById(Menu(monsters,0)+1);

            bs.SimulateBattle(hero,monster);
        }
       
    }
}
