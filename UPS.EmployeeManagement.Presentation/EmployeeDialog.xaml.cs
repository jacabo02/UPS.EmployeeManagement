using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UPS.EmployeeManagement.Presentation
{
    /// <summary>
    /// Interaction logic for EmployeeDialog.xaml
    /// </summary>
    public partial class EmployeeDialog : Window
    {
        public EmployeeDialog()
        {
            InitializeComponent();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = null;
            this.Close();
        }
    }
}
