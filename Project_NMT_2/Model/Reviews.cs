using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_NMT_2.Model
{
    public class Reviews
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        public string review { get; set; }
        [Required]
        public int id_user { get; set; }
        [Required]
        public int blocking { get; set; }
        public virtual UserPersonalInfomation User { get; set; }
        public Reviews() { }
        public Reviews( string _review, int _id_user)
        {
            review = _review;
            id_user = _id_user;
        }
        public override string ToString()
        {
            return $"id: {id} Відгук: {review}";
        }
    }
}
