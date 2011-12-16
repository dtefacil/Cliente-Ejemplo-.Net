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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;
using System.IO;


namespace EjemploDTEFacil
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DteFacilApi api;
        private PaginaDocumentos pag;

        public MainWindow()
        {
            InitializeComponent();
            api = new DteFacilApi();
            EstablecerNombreUsuario(null);
            ActivacionBotonesDtes(false);
        }

        private void detallesButton_Click(object sender, RoutedEventArgs e)
        {
            Documento d = ((FrameworkElement)sender).DataContext as Documento;
            api.DescargarDocumento(d);
        }

        private void sessionButton_Click(object sender, RoutedEventArgs e)
        {
            DialogoIngresar loginDialog = new DialogoIngresar()
            {
                LogInHandler = Ingresar
            };
            loginDialog.Owner = this;
            loginDialog.ShowDialog();
        }

        

        private void Ingresar(String username, String password)
        {
            api.EstablecerUsuario(username, password);
            EstablecerNombreUsuario(username);
            ActivacionBotonesDtes(true);
        }

        private void ActivacionBotonesDtes(bool estado)
        {
            documentsToolBar.IsEnabled = estado;
        }

        private void EstablecerNombreUsuario(String name)
        {
            if (name == null)
            {
                username.Content = "No ha definido un contribuyente";
            }
            else
            {
                username.Content = "Conectado como " + name;
            }
        }

        private void EstablecerPagina(int? npag)
        {
            try
            {
                pag = api.Documentos(npag);
                EstablecerDataGrid();
                EstablecerNavegacion();
            }
            catch (UnauthorizedException ue)
            {
                MessageBox.Show("Debe ingresar con un contribuyente valido");
            }
        }

        private void EstablecerDataGrid()
        {
            dataGrid.ItemsSource = pag.Documentos;
        }

        private void EstablecerNavegacion()
        {
            ant.IsEnabled = pag.Pag > 0;
            sig.IsEnabled = pag.Pag < pag.Pags;
            npags.Content = "/ " + pag.Pags;
            actual.Text = pag.Pag+"";
            actual.IsEnabled = true;
            ir.IsEnabled = true;
            total.Content = pag.Total+" elemento(s) encontrado(s)";
        }

        private void documentsButton_Click(object sender, RoutedEventArgs e)
        {
            EstablecerPagina(null);
        }

        private void ant_Click(object sender, RoutedEventArgs e)
        {
            EstablecerPagina(pag.Pag - 1);
        }

        private void sig_Click(object sender, RoutedEventArgs e)
        {
            EstablecerPagina(pag.Pag + 1);
        }

        private void ir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EstablecerPagina(int.Parse(actual.Text));
            } catch(Exception ex) 
            {
                MessageBox.Show("Error al transformar numero");
            }
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Esta operacion no ha sido implementada");
        }

        private void createButton_Click(object sender, RoutedEventArgs e)
        {
            DialogoCrearFactura dialogoCrear = new DialogoCrearFactura(api.Contribuyente)
            {
                CreateHandler = crearFactura
            };
            dialogoCrear.Owner = this;
            dialogoCrear.ShowDialog();
        }

        private void crearFactura(FacturaElectronica fe)
        {
            try
            {
                api.CrearFactura(fe);

            }
            catch (ApiException ae)
            {
                MessageBox.Show(ae.Message, "Errores");
            }
        }

    }
}
