using System;
using System.Collections.Generic;

#nullable disable

namespace BankEF
{
    public partial class Entity : Client
    {
        #region Приватные поля
        private string _Type;
        #endregion
        #region Поля реализующие интерфейс
        public string Type { get { return _Type; } set { _Type = value; OnPropertyChanged("Type"); } }
        #endregion
    }
}
