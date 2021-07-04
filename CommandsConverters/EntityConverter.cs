using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace BankEF.CommandsConverters
{
    class EntityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            // Проверка поля Type
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
            // Проверка поле Name
            if (values[1] != null && values[1] != DependencyProperty.UnsetValue && values[1].ToString().Length > 0)
            {
                try
                {
                    values[1] = values[1].ToString();
                }
                catch (Exception)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
            //Если все условия соблюдены то возвращаем готового Entity клиента
            return new Entity() { Name = values[0] as string, Type = values[1] as string, Id = Guid.NewGuid() };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
