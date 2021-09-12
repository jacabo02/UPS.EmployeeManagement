using GalaSoft.MvvmLight.Messaging;
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
using UPS.EmployeeManagement.Presentation.Messages;

namespace UPS.EmployeeManagement.Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Messenger.Default.Register<EmployeeEditMessage>(this, EditEmployee);
        }

        private void EditEmployee(EmployeeEditMessage obj)
        {
            var employeeEditDialog = new EmployeeDialog()
            {
                DataContext = obj.Employee
            };

            employeeEditDialog.Owner = this;
            employeeEditDialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            employeeEditDialog.ShowDialog();
        }
    }
}
