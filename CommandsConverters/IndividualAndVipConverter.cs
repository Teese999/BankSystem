using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using BankEF;

namespace BankEF.CommandsConverters 
{
    class IndividualAndVipConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] != null && values[0] != DependencyProperty.UnsetValue && values[0].ToString().Length > 0)
            {
                try
                {
                    //Попытка сконвертировать значение в сторку
                    values[0] = values[0].ToString();
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
            if (values[1] != null && values[1] != DependencyProperty.UnsetValue && values[1].ToString().Length > 0)
            {
                try
                {
                    //Попытка сконвертировать значение в сторку
                    values[1] = values[1].ToString();
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
                    values[3] = values[3].ToString();
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
            return BankViewModel.GetSelectedClientType().Name switch
            {
                "Individual" => new Individual { Name = values[0] as string, Surname = values[1] as string, Birthday = (DateTime)values[2], Workplace = values[3] as string, Id = Guid.NewGuid() },
                "Vip" => new Vip { Name = values[0] as string, Surname = values[1] as string, Birthday = (DateTime)values[2], Workplace = values[3] as string, Id = Guid.NewGuid() },
                _ => null,
            };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
