using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BankEF
{
    class NewIndividualOrVipWindowViewModel : BaseViewModel, INewItemWindowViewModel
    {
        #region Поля
        /// <summary>
        /// Команда добавления клиента при нажатии Save
        /// </summary>
        public RelayCommand IndividualAndVipAddCommand { get; set; }
        public RelayCommand CloseWindowCommand { get; set; }
        public Window ThisWindow { get; set; }
        #endregion
        public NewIndividualOrVipWindowViewModel()
        {
            IndividualAndVipAddCommand = new RelayCommand(o => SaveResult(o as BankItem), o => SaveBtnCheck(o));
        }

        /// <summary>
        /// Сохранение нового клиента
        /// </summary>
        public void SaveResult(BankItem client)
        {
            BankViewModel.AddBankItemToView(client);
            //ThisWindow.Close();
        }
        /// <summary>
        /// Проверка что клиент подходящего типа
        /// </summary>
        /// <param name="parametr"></param>
        /// <returns></returns>
        private static bool SaveBtnCheck(object parametr)
        {
            return parametr != null;
        }
    }
}
