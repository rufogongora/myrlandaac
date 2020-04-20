using System.ComponentModel.DataAnnotations.Schema;

namespace MyrlandAAC.Models
{
    [Table("players")]
    public class Player
    {
        [Column("account_id")]
        public int AccountId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Vocation { get; set; }
        [Column("group_id")]
        public int GroupId { get; set; }
        public int Level { get; set; }
        public byte[] Conditions { get; set; } = new byte[0];
    }
}