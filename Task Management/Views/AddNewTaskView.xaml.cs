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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Task_Management.Views
{
    /// <summary>
    /// Interaction logic for AddNewTaskView.xaml
    /// </summary>
    public partial class AddNewTaskView : UserControl
    {
        public AddNewTaskView()
        {
            InitializeComponent();
        }

        private void NumericOnly(object sender, TextCompositionEventArgs e)
        {
            // Check if the input is numeric
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.Text, "^[0-9\\s?]+$"))
                e.Handled = true; // Reject the input
        }

        private void SfTextBoxExt_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
