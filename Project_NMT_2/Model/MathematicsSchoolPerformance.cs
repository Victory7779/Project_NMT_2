using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_NMT_2.Model
{
    public class MathematicsSchoolPerformance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        public int Result { get; set; }
        [Required]
        public int id_subject { get; set; }
        public virtual SubjectUsers SubjectUsers { get; set; }
        public MathematicsSchoolPerformance(int result, int _id_subject)
        {
            Result = result;
            id_subject = _id_subject;
        }
    }
}
