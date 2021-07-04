using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace BankEF.CommandsConverters
{
    class BillConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] != null && values[0] != DependencyProperty.UnsetValue && values[0].ToString().Length > 0)
            {
                try
                {

                    if (System.Text.RegularExpressions.Regex.IsMatch(values[0].ToString(), "^[,]+[.]+[^0-9]"))
                    {
                        MessageBox.Show("Введите цифры!!");
                        //удаление буквы
                        (values[5] as NewBillWindow).Balance.Text = (values[5] as NewBillWindow).Balance.Text.Remove(values[0].ToString().Length - 1);
                    }
                }
                catch (Exception)
                {

                    return null;
                }
            }
            //Если поле пустое то не даем сохранить
            else
            {
                return null;
            }
            if (values[2] != null && values[2] != DependencyProperty.UnsetValue && values[2].ToString().Length > 0)
            {
                try
                {
                    //Попытка сконвертировать значение в сторку
                    values[2] = DateTime.Parse(values[2].ToString());
                }
                catch (Exception)
                {
                    //если ошибка возвращаем null и деактивируем кнопку "Сохранить
                    return null;
                }
            }
            //Если поле пустое то не даем сохранить
            else
            {
                return null;
            }
            if (values[3] != null && values[3] != DependencyProperty.UnsetValue && values[3].ToString().Length > 0)
            {
                try
                {
                    //Попытка сконвертировать значение в сторку
                    values[3] = DateTime.Parse(values[3].ToString());
                }
                catch (Exception)
                {
                    //если ошибка возвращаем null и деактивируем кнопку "Сохранить
                    return null;
                }
            }
            //Если поле пустое то не даем сохранить
            else
            {
                return null;
            }
            return new Bill() {
                Id = Guid.NewGuid(),
                ClientId = BankViewModel.GetSelectedClientid(),
                Balance = decimal.Parse(values[0].ToString()),
                IsCapital = Boolean.Parse(values[1].ToString()),
                StartDate = DateTime.Parse(values[2].ToString()),
                EndDate = DateTime.Parse(values[3].ToString()),
                IsActive = Boolean.Parse(values[4].ToString()),
                InitialBalance = decimal.Parse(values[0].ToString())
            };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
