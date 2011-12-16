using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Controls;

namespace EjemploDTEFacil
{
    /// <summary>
    /// Convierte de codigo a nombre de actividad economica
    /// </summary>
    [ValueConversion(typeof(int), typeof(ComboBoxItem))]
    public class ComboBoxItemToCodeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int code;
            if (value == null){
                code = -1;
            } else {
                code = (int)value;
            }
            
            var cbi = new ComboBoxItem();
            if (DialogoCrearFactura.codigoANombreActEco.ContainsKey(code))
            {
                cbi.Content = DialogoCrearFactura.codigoANombreActEco[code];
            }
            else
            {
                cbi.Content = code;
            }
            cbi.Tag = code;
            return cbi;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return -1;
            var cbi = (ComboBoxItem)value;
            return (int)cbi.Tag;
        }
    }
}
