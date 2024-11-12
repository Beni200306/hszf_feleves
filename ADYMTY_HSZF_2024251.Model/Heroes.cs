using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADYMTY_HSZF_2024251.Model
{
    public class Heroes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HeroID { get; set; }
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        [Required]
        [StringLength(30)]
        public string Category { get; set; }
        [Required]
        public int Strength { get; set; }
        [Required]
        public int Speed { get; set; }
        [Required]
        [StringLength(100)]
        public string Abilities { get; set; }
        public virtual ICollection<Battle> Battles { get; set; }

        public Heroes()
        {
            this.Battles = new HashSet<Battle>();
        }

    }
}
