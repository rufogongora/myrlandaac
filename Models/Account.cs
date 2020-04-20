using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyrlandAAC.Models
{
    [Table("accounts")]
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public virtual ICollection<Player> Players { get; set; }
    }
}
