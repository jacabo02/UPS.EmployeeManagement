using GalaSoft.MvvmLight.Messaging;
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
using UPS.EmployeeManagement.Presentation.Messages;

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
            Messenger.Default.Register<CloseDialogMessage>(this, Close);
        }

        private void Close(CloseDialogMessage obj)
        {
            DialogResult = obj.IsSuccess;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = null;
            this.Close();
        }
    }
}
