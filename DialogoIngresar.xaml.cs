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

namespace EjemploDTEFacil
{
    public delegate void LogInHandler(String username, String password);

    /// <summary>
    /// Interaction logic for LoginDialog.xaml
    /// </summary>
    public partial class DialogoIngresar : Window
    {
        public LogInHandler LogInHandler{ get; set;}

        public DialogoIngresar()
        {
            InitializeComponent();

        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LogInHandler(username.Text, password.Password);
                Close();
            }
            catch
            {
                username.Clear();
                password.Clear();
                MessageBox.Show("Revise que el servidor esté funcionando y que está utilizando\n" +
                    "las credenciales correctas de un contribuyente tipo DTE Fácil.",
                    "Error al conectarse con el servidor");
            }
        }
    }
}
