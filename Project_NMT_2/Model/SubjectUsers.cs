using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_NMT_2.Model
{
    public class SubjectUsers
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        public bool Ukrainian { get; set; } = false;
        [Required]
        public bool Mathematics { get; set; } = false;
        [Required]
        public bool History { get; set; } = false;
        [Required]
        public int id_user { get; set; }
        public virtual UserPersonalInfomation User { get; set; }
        public virtual ICollection<UkrainianSchoolPerformance> UkrainianSchoolPerformances { get; set; }
        public virtual ICollection<HistorySchoolPerformance> HistorySchoolPerformances { get; set; }
        public virtual ICollection<MathematicsSchoolPerformance> MathematicsSchoolPerformances { get; set; }
        public SubjectUsers( bool ukr, bool math, bool history, int _id_user)
        {
            Ukrainian = ukr;
            Mathematics = math;
            History = history;
            id_user = _id_user;

        }
        public override string ToString()
        {
            string str = $"id: {id} Навчальний предмет: ";
            if (Ukrainian == true) str += "Українська мова ";
            if (Mathematics == true) str += "Математика ";
            if (History == true) str += "Історія ";
            return str;
        }
    }
}
