using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_NMT_2.Model
{
    public class RatingUsers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        public int Ukrainian { get; set; } = 0;
        [Required]
        public int Mathematics { get; set; } = 0;
        [Required]
        public int History { get; set; } = 0;
        [Required]
        public int id_subject { get; set; }
        public virtual SubjectUsers SubjectUsers { get; set; }
        public RatingUsers(int urk, int math, int history, int _id_subject)
        {
            Ukrainian = urk;
            Mathematics = math;
            History = history;
            id_subject = _id_subject;
        }

    }
}
