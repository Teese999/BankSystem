using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BankEF
{
    class NewEntityWindowViewModel : BaseViewModel, INewItemWindowViewModel
    {
        #region Поля
        private List<ComboBoxItem> _EntityTypesItemsList;
        public List<ComboBoxItem> EntityTypesItemsList
        {
            get { return _EntityTypesItemsList; }
            set { _EntityTypesItemsList = value; OnPropertyChanged("EntityTypesItemsList"); }
        }
        public Window ThisWindow { get; set; }
        public RelayCommand EntitySaveCommand { get; set; }
        #endregion
        public NewEntityWindowViewModel()
        {
            EntityTypesItemsList = CreateEntityTypesList();
            EntitySaveCommand = new(o => SaveResult(o as Entity), o=> SaveBtnCheck(o));
        }

        /// <summary>
        /// Создание списка типов фирм для Entity клиента
        /// </summary>
        /// <returns>Массив ComoboBoItem</returns>
        private static List<ComboBoxItem> CreateEntityTypesList()
        {
           
            List<ComboBoxItem> tempComboBoxListItems = new()
            {
                new ComboBoxItem() { Content = "ООО" },
                new ComboBoxItem() { Content = "ОАО" },
                new ComboBoxItem() { Content = "ЗАО" },
                new ComboBoxItem() { Content = "ИП" },
                new ComboBoxItem() { Content = "АО" },
                new ComboBoxItem() { Content = "ПАО" },
                new ComboBoxItem() { Content = "НКО" },
                new ComboBoxItem() { Content = "ОП" },
                new ComboBoxItem() { Content = "ТСЖ" }
            };

            //Фикс ошибки WPF
            tempComboBoxListItems.ForEach(x =>
            {
                x.HorizontalContentAlignment = HorizontalAlignment.Left;
                x.VerticalContentAlignment = VerticalAlignment.Center;
            });

            return tempComboBoxListItems;
        }
        public void SaveResult(BankItem client)
        {
            BankViewModel.AddBankItemToView(client);
            ThisWindow.Close();
        }
        private static bool SaveBtnCheck(object parametr)
        {
            return parametr != null && parametr is Entity;
        }
    }
}
