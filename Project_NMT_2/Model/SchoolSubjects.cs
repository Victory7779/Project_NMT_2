using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_NMT_2.Model
{
    public class SchoolSubjects
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        public string subject { get; set; }
        public virtual ICollection<ALLTest> ALLTests { get; set; }
        public SchoolSubjects() { }
        public SchoolSubjects( string _subject)
        {
            subject = _subject;
        }
    }
}
