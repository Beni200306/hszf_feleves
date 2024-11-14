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
            //BattleContext bcontext = new BattleContext();

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

                    //TODO
                }
            ).Build();

            host.Start();



            using IServiceScope serviceScope = host.Services.CreateScope();
            IServiceProvider serviceProvider = serviceScope.ServiceProvider;


            var heroService = serviceProvider.GetService<IHeroService>();

            //var hero = heroService.GetHeroById(1);

            var monsterService = serviceProvider.GetService<IMonsterService>();
            //var monster = monsterService.GetMonsters();

            var battleService = serviceProvider.GetService<IBattleService>();
            //var battle = battleService.GetBattles();

            ;


            string[] mainMenuOptions = {
                "Új hős felvétele",
                "Új szörny felvétele",
                "Meglévő hős módosítása",
                "Meglévő szörny módosítása",
                "Keresés hős név szerint",
                "Keresés hős kategória szerint" ,
                "Keresés szörny szint szerint"
            };
            
            Menu(mainMenuOptions, 0);

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

        static object ConvertValue(string input, Type targetType)
        {
            if (targetType == typeof(string))
            {
                return input; 
            }
            else if (targetType == typeof(int))
            {
                int.TryParse(input, out int result);
                return result;
            }
            else if (targetType == typeof(double))
            {
                double.TryParse(input, out double result);
                return result;
            }
            else if (targetType == typeof(bool))
            {
                bool.TryParse(input, out bool result);
                return result;
            }
            else if (targetType == typeof(DateTime))
            {
                DateTime.TryParse(input, out DateTime result);
                return result;
            }
            else
            {
                throw new InvalidOperationException($"Unsupported property type: {targetType.Name}");
            }
        }
    }
}
