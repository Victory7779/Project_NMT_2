using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;

namespace Project_NMT_2.Model
{
    public class UserPersonalInfomation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public byte[] Photo { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Age { get; set; }

        public virtual ICollection<Reviews> Reviewss { get; set; }
        public virtual ICollection<PassedTest> Tests { get; set; }

        public UserPersonalInfomation(string _Name, int _Age)
        {
            Name = _Name;
            Age = _Age;
        }
        public UserPersonalInfomation() { }
        public override string ToString()
        {
            return $"id: {id} Ім'я: {Name} Вік: {Age}";
        }


    }
}
