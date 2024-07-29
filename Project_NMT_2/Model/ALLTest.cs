using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Project_NMT_2.Model
{
    public class ALLTest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public int CountQ { get; set; }
        [Required]
        public int Time { get; set; }
        [Required]
        public int id_subject { get; set; }
        public virtual SchoolSubjects SchoolSubjects { get; set; }
        public virtual ICollection<PassedTest> PassedTests { get; set; }
        public virtual ICollection<QuestionsForTest> QuestionsForTests { get; set; }
        public ALLTest( string title, int time, int countQ, int _id_subject)
        {
            Title = title;
            CountQ = countQ;
            Time = time;
            id_subject = _id_subject;
        }
        public override string ToString()
        {
            return $"id: {id} Назва: {Title} к-ть питань: {CountQ} час: {Time}";
        }

    }
}
