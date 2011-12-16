using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections;
using System.IO;

namespace EjemploDTEFacil
{
    public delegate void CreateHandler(FacturaElectronica facturaElectronica);

    /// <summary>
    /// Interaction logic for DialogoCrearFactura.xaml
    /// </summary>
    public partial class DialogoCrearFactura : Window
    {

        public CreateHandler CreateHandler { get; set; }

        private FacturaElectronica facturaElectronica;
        
        public static Dictionary<int, string> codigoANombreActEco;

        public DialogoCrearFactura(Contribuyente c)
        {
            InitializeComponent();

            facturaElectronica = new FacturaElectronica()
            {
                Detalles = new List<Detalle>(),
                Receptor = new Receptor()
                {
                    Ubicacion = new Ubicacion()
                }
            };
            

            mainPanel.DataContext = facturaElectronica;

            if (codigoANombreActEco == null)
            {
                LeerActividadesEconomicas();
            }

            foreach(ActividadEconomica a in c.ActividadesEconomicas){
                var cbi = new ComboBoxItem();
                if (codigoANombreActEco.ContainsKey(a.Value))
                {
                    cbi.Content = codigoANombreActEco[a.Value];
                }
                else
                {
                    cbi.Content = a.Value;
                }
                cbi.Tag = a.Value;
                actEcoComboBox.Items.Add(cbi);
            }
        }

        private void LeerActividadesEconomicas()
        {
            codigoANombreActEco = new Dictionary<int, string>();
            foreach (string line in File.ReadAllLines("Resources\\cod_act_eco.properties"))
            {
                if ((!string.IsNullOrEmpty(line)) &&
                    (!line.StartsWith(";")) &&
                    (!line.StartsWith("#")) &&
                    (!line.StartsWith("'")) &&
                    (line.Contains('=')))
                {
                    int index = line.IndexOf('=');
                    string key = line.Substring(0, index).Trim();
                    string value = line.Substring(index + 1).Trim();

                    if ((value.StartsWith("\"") && value.EndsWith("\"")) ||
                        (value.StartsWith("'") && value.EndsWith("'")))
                    {
                        value = value.Substring(1, value.Length - 2);
                    }
                    codigoANombreActEco.Add(int.Parse(key), value);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CreateHandler(facturaElectronica);
            Close();
        }
    }


}
