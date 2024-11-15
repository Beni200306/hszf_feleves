using ADYMTY_HSZF_2024251.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ADYMTY_HSZF_2024251.Application
{
    public static class CreateInstance
    {
        public static T createInstance<T>()
        {
            T re = (T)Activator.CreateInstance(typeof(T));
            foreach (var prop in re.GetType().GetProperties())
            {
                if (prop.GetCustomAttribute<ConvertAttribute>() != null)
                {
                    Console.WriteLine($"Enter value for {prop.Name} ({prop.PropertyType.Name}):");

                    string input = Console.ReadLine();
                    if (prop.PropertyType != typeof(string))
                    {
                        MethodInfo parse = prop.PropertyType.GetMethods().First(t => t.Name == "Parse");
                        object value = parse.Invoke(null, new object[] { input });
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
    }
}
