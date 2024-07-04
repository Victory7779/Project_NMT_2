using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_NMT_2.Model
{
    public class OpenAnswer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string textAnswerOne { get; set; }
        public string textAnswerTwo { get; set; }
        public int id_question { get; set; }
        public virtual QuestionsForTest QuestionsForTest { get; set; }
        public OpenAnswer(string _textAnswerOne, string _textAnswerTwo, int _id_question)
        {
            textAnswerOne = _textAnswerOne;
            textAnswerTwo = _textAnswerTwo;
            id_question = _id_question;
        }
    }
}
