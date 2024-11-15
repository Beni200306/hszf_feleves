using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ADYMTY_HSZF_2024251.Model
{
    public class Monsters
    {
        [Key]
        [ToConsole]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Convert]
        public int MonsterID { get; set; }
        [Required]
        [ToConsole]
        [StringLength(30)]
        [Convert]
        public string Name { get; set; }
        [Required]
        [StringLength(30)]
        [ToConsole]
        [Convert]
        public string Level { get; set; }
        [Required]
        [Convert]
        [ToConsole]
        public int Strength { get; set; }
        [Required]
        [Convert]
        [ToConsole]
        public int Speed { get; set; }
        public virtual ICollection<Battle> Battles { get; set; }
        public Monsters()
        {
            this.Battles = new HashSet<Battle>();
        }
        public void ToConsole()
        {
            Console.WriteLine();
            foreach (var item in this.GetType().GetProperties())
            {
                if (item.GetCustomAttribute<ToConsoleAttribute>() is not null)
                {
                    Console.WriteLine($"{item.Name}: {item.GetValue(this)}");
                }
            }
        }
    }
}
