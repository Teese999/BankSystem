using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace BankEF
{
    public class BankViewModel : BaseViewModel
    {
        public static TestDBContext Context { get; set; } = new();
        private static Dictionary<Guid, Type> ClientDictionary { get; set; } = new();
        private static Client SelectedClient { get; set; }
        #region DataGridsSources
        private static ObservableCollection<Individual> indlist;
        private static ObservableCollection<Entity> entList;
        private static ObservableCollection<Vip> vipList;
        private static ObservableCollection<Bill> showedBills = new();
        public ObservableCollection<Individual> IndList

        {
            get { return indlist; }
            set
            {
                indlist = value; OnPropertyChanged("IndList");
            }
        }

        public ObservableCollection<Entity> EntList

        {
            get { return entList; }
            set
            {
                entList = value; OnPropertyChanged("EntList");
            }
        }

        public ObservableCollection<Vip> VipList

        {
            get { return vipList; }
            set
            {
                vipList = value; OnPropertyChanged("VipList");
            }
        }

        public ObservableCollection<Bill> ShowedBills

        {
            get { return showedBills; }
            set
            {
                showedBills = value; OnPropertyChanged("ShowedBills");
            }
        }
        #endregion
        #region Commands
        public RelayCommand ChangingClientCommand { get; set; }
        public RelayCommand NewClientWindowCommand { get; set; }
        public RelayCommand NewBillWindowCommand { get; set; }
        public RelayCommand DeleteClientCommand { get; set; }
        public RelayCommand DeleteBillCommand { get; set; }
        #endregion
        public BankViewModel()
        {
            DataGridsFill();
            CommandsActivate();
            CreateClientsDictionary();
        }
        #region Методы инкапсуляции
        public static Guid GetSelectedClientid()
        {
            return SelectedClient.Id;
        }
        public static Type GetSelectedClientType()
        {
            return SelectedClient.GetType();
        }
        public static void AddBankItemToView(BankItem item)
        {
            switch (item.GetType().Name)
            {
                case "Individual":
                    indlist.Add(item as Individual);
                    ClientDictionary.Add(item.Id, item.GetType());
                    break;
                case "Vip":
                    vipList.Add(item as Vip);
                    ClientDictionary.Add(item.Id, item.GetType());
                    break;
                case "Entity":
                    entList.Add(item as Entity);
                    ClientDictionary.Add(item.Id, item.GetType());
                    break;
                case "Bill":
                    showedBills.Add(item as Bill);
                    Context.Bills.Add(item as Bill);
                    break;
                default:
                    break;
            }
            Context.SaveChanges();
        }
        public static void DeleteBankItem(BankItem item)
        {
            switch (item.GetType().Name)
            {
                case "Individual":
                    indlist.Remove(item as Individual);
                    ClientDictionary.Remove(item.Id);
                    break;
                case "Vip":
                    vipList.Remove(item as Vip);
                    ClientDictionary.Remove(item.Id);
                    break;
                case "Entity":
                    entList.Remove(item as Entity);
                    ClientDictionary.Remove(item.Id);
                    break;
                case "Bill":
                    showedBills.Remove(item as Bill);
                    Context.Bills.Remove(item as Bill);
                    break;
                default:
                    break;
            }
            Context.SaveChanges();
        }

        #endregion
        #region Методы
        private void CreateClientsDictionary()
        {
            foreach (var item in IndList) { ClientDictionary.Add(item.Id, item.GetType()); }
            foreach (var item in EntList) { ClientDictionary.Add(item.Id, item.GetType()); }
            foreach (var item in VipList) { ClientDictionary.Add(item.Id, item.GetType()); }
        }
        private void DataGridsFill()
        {
            IndList = Context.Individuals.Local.ToObservableCollection();
            EntList = Context.Entities.Local.ToObservableCollection();
            VipList = Context.Vips.Local.ToObservableCollection();
        }
        private void CommandsActivate()
        {
            //Изменение выбранного клиента в DataGrid
            ChangingClientCommand = new(
            SelectedClientInWindow =>
            {
                //очищаю DataGrid счетов
                showedBills.Clear();
                //создаю временную коллекцию с счетами где ClientId счета равен Id выбранного клиента
                List<Bill> tempBillCollection = Context.Bills.Local.ToObservableCollection().Where(bill => bill.ClientId == (SelectedClientInWindow as Client).Id).ToList();
                //Добавляю в в DataGrid счетов все счета из tempBillCollection
                tempBillCollection.ForEach(b => showedBills.Add(b));
                //Изменяю значение SelectedClient
                SelectedClient = (SelectedClientInWindow as Client);
            },
            o =>
            {
                return o != null;
            });

            //Открытие нового окна создание клиента
            NewClientWindowCommand = new(
            OpenWindowAction =>
            {
                //Выбираю какой тип клиента нужно создать
                switch (SelectedClient.GetType().Name)
                {
                    case "Individual":
                    case "Vip":
                        {
                            //Создаю viewmodel
                            NewIndividualOrVipWindowViewModel viewmodel = new();
                            //Создаю экземпляр окна
                            NewClientWindow newClientWindow = new()
                            {
                                Owner = Application.Current.MainWindow,
                                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                                DataContext = viewmodel
                            };
                            //Передаю во viewmodel экземпляр его окна
                            viewmodel.ThisWindow = newClientWindow;
                            newClientWindow.ShowDialog();
                            break;
                        }
                    case "Entity":
                        {
                            NewEntityWindowViewModel viewmodel = new();
                            NewEntityClientWindow newEntityClientWindow = new()
                            {
                                Owner = Application.Current.MainWindow,
                                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                                DataContext = viewmodel
                            };
                            viewmodel.ThisWindow = newEntityClientWindow;
                            newEntityClientWindow.ShowDialog();
                            break;
                        }
                    default:
                        break;
                }
            },
            OpenWindowActionCheck =>
            {
                return SelectedClient != null;
            });
            //Удаление клиента
            DeleteClientCommand = new(o => { DeleteBankItem(o as BankItem); }, o => { return o != null && o is BankItem; });

            //Добавление нового счета
            NewBillWindowCommand = new(
                o =>
                {
                    NewBillWindowViewModel viewmodel = new();
                    NewBillWindow newBillWindow = new()
                    {
                        Owner = Application.Current.MainWindow,
                        WindowStartupLocation = WindowStartupLocation.CenterOwner,
                        DataContext = viewmodel
                    };
                    viewmodel.ThisWindow = newBillWindow;
                    newBillWindow.ShowDialog();
                },
                o =>
                {
                    return SelectedClient != null;
                });

            //Удаление счета
            DeleteBillCommand = new(
                o=>
                {
                    DeleteBankItem(o as Bill);
                },
                o=>
                {
                    return o != null && o is Bill;
                }
                );
        }
        #endregion
    }
}
