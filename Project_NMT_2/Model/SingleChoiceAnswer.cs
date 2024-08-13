using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_NMT_2.Model
{
    public class SingleChoiceAnswer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string textAnswer { get; set; }
        public bool answerTrueOrFalse { get; set; }
        public int id_question { get; set; }
        public virtual QuestionsForTest QuestionsForTest { get; set; }
        public SingleChoiceAnswer(string _textAnswer, bool _answerTorF, int _id_question)
        {
            textAnswer = _textAnswer;
            answerTrueOrFalse = _answerTorF;
            id_question = _id_question;
        }
        public SingleChoiceAnswer() { }
    }
}
