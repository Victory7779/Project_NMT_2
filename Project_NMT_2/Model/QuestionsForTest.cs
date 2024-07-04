using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_NMT_2.Model
{
    public class QuestionsForTest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        public string question { get; set; }
        [Required]
        public int id_test { get; set; }
        public virtual ALLTest ALLTest { get; set; }
        public virtual ICollection<SingleChoiceAnswer> SingleChoiceAnswers { get; set; }
        public virtual ICollection<MultipleChoiceAnswer> MultipleChoiceAnswers { get; set; }
        public virtual ICollection<MachingAnswer> MachingAnswers { get; set; }
        public virtual ICollection<OpenAnswer> OpenAnswers { get; set; }
        public QuestionsForTest(string _question, int _id_test)
        {
            question = _question;
            id_test = _id_test;
        }


    }
}
