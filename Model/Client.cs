using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankEF
{
    public class Client : BankItem
    {
        private string _Name;
        public string Name { get { return _Name; } set { _Name = value; OnPropertyChanged("Name"); } }
    }
}
