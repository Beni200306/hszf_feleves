using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ADYMTY_HSZF_2024251.Model
{
    public class Battle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BattleID { get; set; }
        [Required]
        public int HeroID { get; set; }
        [Required]
        public virtual Heroes Hero { get; set; }
        [Required]
        public int MonsterID { get; set; }
        [Required]
        public virtual Monsters Monster { get; set; }
        [Required]
        public DateTime BattleDate { get; set; }
        [Required]
        [StringLength(10)]
        public string Result { get; set; }

    }
}
