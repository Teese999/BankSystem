using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BankEF
{
    class NewBillWindowViewModel : BaseViewModel, INewItemWindowViewModel
    {
        public NewBillWindow ThisWindow { get; set; }
        public RelayCommand BillSaveCommand { get; set; }
        public NewBillWindowViewModel()
        {
            BillSaveCommand = new(o => { SaveResult(o as Bill); }, o => { return o != null && o is Bill; });

        }
        public void SaveResult(BankItem bill)
        {
            BankViewModel.AddBankItemToView(bill);
            ThisWindow.Close();
        }
    }
}
