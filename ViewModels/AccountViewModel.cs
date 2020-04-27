using System.Collections;
using System.Collections.Generic;

namespace MyrlandAAC.ViewModels
{
    public class AccountViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Premdays { get; set; }
        public IEnumerable<PlayerViewModel> Players { get; set; }
    }
}