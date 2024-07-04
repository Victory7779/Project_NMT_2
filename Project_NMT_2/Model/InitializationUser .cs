using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Project_NMT_2.Model
{
    public class InitializationUser
    {
        [Key]
        [EmailAddress]
        public string Email { get; set;  }
        [Required]
        public string Password { get; set; }
        [Required]
        public int id_user { get; set; }
        public virtual UserPersonalInfomation User { get; set; }

        public InitializationUser( string _Email, string _Password, int _id_user)
        {
            Email = _Email;
            Password = _Password;
            id_user = _id_user;
        }

        public override string ToString()
        {
            return $"Email: {Email}  Password: {Password} id користувача: {id_user}";
        }
    }
}
