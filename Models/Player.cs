using System.ComponentModel.DataAnnotations.Schema;

namespace MyrlandAAC.Models
{
    [Table("players")]
    public class Player
    {
        [Column("account_id")]
        public int AccountId {get;set;}
        public int Id {get;set;}
        public string Name {get;set;}
    }
}