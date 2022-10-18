using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Eje3Agenda.Views
{
    /// <summary>
    /// Lógica de interacción para AgendaView.xaml
    /// </summary>
    public partial class AgendaView : Window
    {
        public AgendaView()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Deseas eliminar a este amigo de tu agenda?", "Confirme",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                avm.EliminarCommand.Execute(lstAgenda.SelectedItem);
            }
        }
    }
}
