using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MyToDo.Common.Converters
{
    public class EnumStatusToBoolConverter : IValueConverter
    {
        /// <summary>
        /// 后端到前端的转换
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
           if(value!=null && value is StatusEnum status)
            {
                return status == StatusEnum.已完成;
            }
           return false;
        }
        /// <summary>
        /// 前端到后端的转换
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value!=null && value is Boolean isCheck)
            {
                if (isCheck)
                {
                    return StatusEnum.已完成;
                }
            }
             return StatusEnum.未完成;
        }
    }
}
