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

namespace NBMFilteringService
{
    /// <summary>
    /// Interaction logic for NewMessage.xaml
    /// </summary>
    public partial class NewMessage : Window
    {
        public NewMessage()
        {
            InitializeComponent();
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            string id = idBox.Text;
            string send = senderBox.Text;
            string subject = subjectBox.Text;
            string body = messageBox.Text;

            MessageProcess.CategoriseMessage(id);
            MessageProcess.SanitiseMessage(id, send, subject, body);
            this.Close();
        }
    }
}
