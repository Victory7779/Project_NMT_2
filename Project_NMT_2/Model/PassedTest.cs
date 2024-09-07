using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_NMT_2.Model
{
    public class PassedTest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public int CorrectQ { get; set; }
        [Required]
        public int CountQ { get; set; }
        [Required]
        public int Time { get; set; }
        [Required]
        public int id_test { get; set; }
        [Required]
        public int id_user { get; set; }
        public virtual UserPersonalInfomation User { get; set; }
        public virtual ALLTest ALLTests { get; set; }
        public PassedTest() { }
        public PassedTest(string title, int correctQ, int countQ, int time, int _id_test, int _id_user)
        {
            Title = title;
            CorrectQ = correctQ;
            CountQ = countQ;
            Time = time;
            id_test = _id_test;
            id_user = _id_user;
        }
        public override string ToString()
        {
            return$"id: {id} Назва: {Title} к-ть правтльних: {CorrectQ} к-ть питань: {CountQ} час: {Time}";
        }
    }
}
