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
using UPS.EmployeeManagement.Presentation.ViewModel;

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
            Messenger.Default.Register<ConfirmationMessage>(this, Confirm);
            Messenger.Default.Register<DialogMessage>(this, Dialog);

        }

        private void Dialog(DialogMessage obj)
        {
            MessageBox.Show(obj.Message, obj.Title, MessageBoxButton.OK, obj.IsError ? MessageBoxImage.Error : MessageBoxImage.None);
        }

        private void Confirm(ConfirmationMessage obj)
        {
            var result = MessageBox.Show(obj.Message, obj.Title, MessageBoxButton.YesNo);
            obj.Result = result == MessageBoxResult.Yes;
        }

        private void EditEmployee(EmployeeEditMessage obj)
        {
            var employeeEditDialog = new EmployeeDialog()
            {
                DataContext = obj.Employee
            };

            employeeEditDialog.Owner = this;
            employeeEditDialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            obj.DialogResult = employeeEditDialog.ShowDialog();
            
        }
    }
}
