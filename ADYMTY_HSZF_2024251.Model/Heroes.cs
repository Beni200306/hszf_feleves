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
    public class Heroes
    {
        [ToConsole]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HeroID { get; set; }
        [ToConsole]
        [Required]
        [StringLength(30)]
        [Convert]
        public string Name { get; set; }
        [ToConsole]
        [Required]
        [StringLength(30)]
        [Convert]
        public string Category { get; set; }
        [ToConsole]
        [Required]
        [Convert]
        public int Strength { get; set; }
        [ToConsole]
        [Required]
        [Convert]
        public int Speed { get; set; }
        [ToConsole]
        [Required]
        [StringLength(100)]
        [Convert]
        public string Abilities { get; set; }
        public virtual ICollection<Battle> Battles { get; set; }

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
        public Heroes()
        {
            this.Battles = new HashSet<Battle>();
        }

    }
}
