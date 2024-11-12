using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADYMTY_HSZF_2024251.Model
{
    public class Monsters
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MonsterID { get; set; }
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        [Required]
        [StringLength(30)]
        public string Level { get; set; }
        [Required]
        public int Strength { get; set; }
        [Required]
        public int Speed { get; set; }
        public virtual ICollection<Battle> Battles { get; set; }
        public Monsters()
        {
            this.Battles = new HashSet<Battle>();
        }
    }
}
