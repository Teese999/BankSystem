using System;
using System.Collections.Generic;

#nullable disable

namespace BankEF
{
    public partial class Vip : Client
    {
        #region Приватные поля
        private string _Surname;
        private DateTime _Birthday;
        private string _Workplace;
        #endregion
        #region Поля реализующие интерфейс
        public string Surname { get { return _Surname; } set { _Surname = value; OnPropertyChanged("Surname"); } }
        public DateTime Birthday { get { return _Birthday; } set { _Birthday = value; OnPropertyChanged("Birthday"); } }
        public string Workplace { get { return _Workplace; } set { _Workplace = value; OnPropertyChanged("Workplace"); } }
        #endregion
    }
}
