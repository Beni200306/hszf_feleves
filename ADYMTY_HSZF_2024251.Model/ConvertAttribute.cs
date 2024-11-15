using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADYMTY_HSZF_2024251.Model
{
    public class ConvertAttribute : Attribute
    {
        //CreateInstance függvényhez, csak az "egyszerű" adatokat kéri be
        public ConvertAttribute()
        {
        }
    }
    public class ToConsoleAttribute : Attribute
    {
        //Konzolra kiíárashoz, csak az alap adatokat jeleníti meg
        public ToConsoleAttribute()
        {
            
        }
    }
}
