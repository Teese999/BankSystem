using System;
using System.Collections.Generic;

#nullable disable

namespace BankEF
{
    public partial class Bill : BankItem
    {
        #region Приватные поля
        private Guid _ClientId;
        private decimal _Balance;
        private bool _IsCapital;
        private DateTime _StartDate;
        private DateTime _EndDate;
        private bool _IsActive;
        private decimal _InitialBalance;
        #endregion
        #region Поля реализующие интерфейс
        public Guid ClientId { get { return _ClientId; } set { _ClientId = value; OnPropertyChanged("ClientId"); } }
        public decimal Balance { get { return _Balance; } set { _Balance = value; OnPropertyChanged("Balance"); } }
        public bool IsCapital { get { return _IsCapital; } set { _IsCapital = value; OnPropertyChanged("IsCapital"); } }
        public DateTime StartDate { get { return _StartDate; } set { _StartDate = value; OnPropertyChanged("StartDate"); } }
        public DateTime EndDate { get { return _EndDate; } set { _EndDate = value; OnPropertyChanged("EndDate"); } }
        public bool IsActive { get { return _IsActive; } set { _IsActive = value; OnPropertyChanged("IsActive"); } }
        public decimal InitialBalance { get { return _InitialBalance; } set { _InitialBalance = value; OnPropertyChanged("InitialBalance"); } }
        #endregion
    }
}
